using System;
using System.Diagnostics.CodeAnalysis;

using Emission.GLFW;
using Emission.IO;
using Emission.Window.GLFW;
using static Emission.Graphics.GL;
using Emission.Mathematics.Numerics;

namespace Emission.Window
{
    public class Window : IDisposable, IEngineBehaviour
    {
        /// <summary>
        /// Pointer to Glfw Window object. Represent window. Public get and can be only set class constructor.
        /// Pointer to <see cref="GLFW.Window"/>
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Structure that contains all information to generate window.
        /// Information cannot be change, so it can be used to get starting value for the width or the title for example.
        /// Also contains Projection parameters for projections matrices for current <see cref="PerspectiveCamera"/>.
        /// </summary>
        public WindowParameters Parameters { get; }

        /// <summary>
        /// Return, as float value, the relationship between the width and the height of the screen.
        /// Calculate with <see cref="_lastWindowPosition"/>.
        /// Cannot be set, readonly.
        /// </summary>
        public float WindowAspect
        {
            get => _lastWinSize.X / _lastWinSize.Y;
        }

        /// <summary>
        /// Use a Vector2 with int values to get window's position in computer screen space.
        /// Can be set or get.
        /// Call <see cref="Glfw.SetWindowPos"/> to define position.
        /// </summary>
        public Vector2 WindowPosition
        {
            get => _lastWinPos;
            set => Glfw.SetWindowPosition(Handle, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Use a Vector2 with int values to get Window current size in pixels.
        /// Can be set or get.
        /// Call <see cref="Glfw.SetWindowSize"/> to define size.
        /// </summary>
        public Vector2 WindowSize
        {
            get => _lastWinSize;
            set => Glfw.SetWindowSize(Handle, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Return a boolean when the window is maximized on the current monitor.
        /// Can be set or get.
        /// When is set, it maximized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Maximized
        {
            get => Glfw.GetWindowAttribute(Handle, WindowAttribute.Maximized);
            set
            {
                if(value) Glfw.MaximizeWindow(Handle);
                else Glfw.RestoreWindow(Handle);
            }
        }

        /// <summary>
        /// Return a boolean when the window is minimized on the current monitor.
        /// Can be set or get.
        /// When is set, it minimized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Minimized
        {
            get => Glfw.GetWindowAttribute(Handle, WindowAttribute.AutoIconify);
            set
            {
                if(value) Glfw.IconifyWindow(Handle);
                else Glfw.RestoreWindow(Handle);
            }
        }

        /// <summary>
        /// Return or set window's title. Use private variable <see cref="_title"/> to save current title.
        /// When the title need to change, it define the private variable and then change title using
        /// <see cref="Glfw.SetWindowTitle"/>.
        /// </summary>
        [MaybeNull]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Glfw.SetWindowTitle(Handle, value ?? String.Empty);
            }
        }
        
        /// <summary>
        /// Return if the window is visible or not. Use attribute to get the value.
        /// If visibility need the be set, if it's true, the window will be show, otherwise the window will be hide.
        /// </summary>
        public bool Visible
        {
            get => Glfw.GetWindowAttribute(Handle, WindowAttribute.Visible);
            set
            {
                if(value) Glfw.ShowWindow(Handle);
                else Glfw.HideWindow(Handle);
            }
        }

        /// <summary>
        /// Warper for <see cref="Glfw.GetWindowAttrib"/> with <see cref="WindowAttributeGetBool.Focused"/> while
        /// returning value and <see cref="Glfw.FocusWindow"/> when value is set.
        /// </summary>
        public bool Focus
        {
            get => Glfw.GetWindowAttribute(Handle, WindowAttribute.Focused);
            set => Glfw.FocusWindow(Handle);
        }

        /// <summary>
        /// Warper for <see cref="Glfw.WindowShouldClose"/> while returning value
        /// and <see cref="Glfw.SetWindowShouldClose"/> when value is set.
        /// </summary>
        public bool ShouldClose
        {
            get => Glfw.WindowShouldClose(Handle);
            set => Glfw.SetWindowShouldClose(Handle, value);
        }
        
        /// <summary>
        /// Gets or sets the opacity of the window in the range of 0.0f and 1.0f.
        /// </summary>
        public float Opacity
        {
            get => Glfw.GetWindowOpacity(Handle);
            set => Glfw.SetWindowOpacity(Handle, Math.Min(1.0f, Math.Max(0.0f, value)));
        }

        public Viewport Viewport => new Viewport(0, 0, WindowSize.X, WindowSize.Y);

        public IEngineBehaviour Behaviour => this;

        private string _title;
        private Vector2 _lastWinSize;
        private Vector2 _lastWinPos;

        public Window(WindowParameters parameters)
        {
            Parameters = parameters;
            _title = parameters.Title;
            _lastWinSize = (Parameters.Width, Parameters.Height);
            _lastWinPos = Vector2.Zero;

            if (!Glfw.Init())
            {
                Glfw.GetError(out string error);
                throw new EmissionException(EmissionException.EmissionGlfwException, error);
            }
            
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.ContextVersionMinor, Parameters.MinorVersion);
            Glfw.WindowHint(Hint.ContextVersionMajor, Parameters.MajorVersion);
            Glfw.WindowHint(Hint.Focused, Parameters.IsFocused);
            Glfw.WindowHint(Hint.Floating, Parameters.IsFloating);
            Glfw.WindowHint(Hint.Maximized, Parameters.IsMaximized);
            Glfw.WindowHint(Hint.Decorated, Parameters.IsDecorated);
            Glfw.WindowHint(Hint.Resizable, Parameters.IsResizable);
            Glfw.WindowHint(Hint.Visible, Parameters.IsVisible);
            Glfw.WindowHint(Hint.CenterCursor, Parameters.IsCursorCentered);
            Glfw.WindowHint(Hint.FocusOnShow, Parameters.IsFocusedOnShow);
            
            Handle = Glfw.CreateWindow(Parameters.Width, Parameters.Height, Parameters.Title, Monitor.None, IntPtr.Zero);
            if (Handle == IntPtr.Zero) throw new EmissionException(EmissionException.EmissionGlfwException, "Failed to create GLFW Window!");

            Glfw.MakeContextCurrent(Handle);
            Import(Glfw.GetProcAddress);
        }

        public void Initialize()
        {
            glEnable(GL_BLEND);
            glEnable(GL_DEPTH_CLAMP);
            glEnable(GL_TEXTURE_2D);
            
            glDepthFunc(GL_LESS);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
            
            glViewport(0, 0, Parameters.Width, Parameters.Height);

            Event.AddDelegate(Event.WindowClose, OnClose);
            Event.AddDelegate<Vector2>(Event.WindowResize, OnResize);
            Event.AddDelegate<Vector2>(Event.WindowMove, OnMove);
            Event.AddDelegate<bool>(Event.WindowIconify, OnIconify);
            Event.AddDelegate<bool>(Event.WindowMaximize, OnMaximize);
            Event.AddDelegate<bool>(Event.WindowFocus, OnFocus);
            
            Input ptr = Instances.Input;
            Event.AddDelegate<(Keys, InputState)>(Event.Key, ptr.KeyCallback);
            Event.AddDelegate<(MouseButton, InputState)>(Event.Button, ptr.MouseCallback);
            Event.AddDelegate<double>(Event.MouseScroll, ptr.ScrollCallback);
            Event.AddDelegate<Vector2>(Event.MouseMove, ptr.CursorCallback);
            
            Glfw.SetCloseCallback(Handle, _ => Event.Invoke(Event.WindowClose));
            Glfw.SetWindowSizeCallback(Handle, (_, width, height) => Event.Invoke<Vector2>(Event.WindowResize, (width, height)));
            Glfw.SetWindowPositionCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.WindowMove, ((float)x, (float)y)));
            Glfw.SetWindowFocusCallback(Handle, (_, focused) => Event.Invoke<bool>(Event.WindowFocus, focused));
            Glfw.SetWindowIconifyCallback(Handle, (_, iconified) => Event.Invoke<bool>(Event.WindowIconify, iconified));
            Glfw.SetWindowMaximizeCallback(Handle, (_, maximized) => Event.Invoke<bool>(Event.WindowMaximize, maximized));

            Glfw.SetKeyCallback(Handle, (_, key, code, action, mods) => Event.Invoke<(Keys, InputState)>(Event.Key, (key, action)));
            Glfw.SetMouseButtonCallback(Handle, (_, button, action, mods) => Event.Invoke<(MouseButton, InputState)>(Event.Button, (button, action)));
            Glfw.SetScrollCallback(Handle, (_, x, y) => Event.Invoke<double>(Event.MouseScroll, y));
            Glfw.SetCursorPositionCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.MouseMove, ((float)x, (float)y)));
        }

        public void Start()
        {
            Visible = true;
        }

        public void Update()
        {
            Glfw.PollEvents();
        }

        public void Render()
        {
            glClearColor(Parameters.ClearColorArgb.R, Parameters.ClearColorArgb.G, Parameters.ClearColorArgb.B, 0);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        }

        public void Flush()
        {
            if (Parameters.VSync)
            {
                Glfw.SwapBuffers(Handle);
            }
        }

        public void LoadIcon(string path) => LoadIcon(new Icon(path));
        public void LoadIcon(Icon icon)
        {
            Glfw.SetWindowIcon(Handle, 1, new []{icon});
        }

        public void Stop()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            Glfw.DestroyWindow(Handle);
            Glfw.Terminate();
        }
        
        public virtual void OnClose()
        {
            GameController.Stop(0);
        }
        
        public virtual void OnResize(Vector2 size)
        {
            glViewport(0, 0, (int)size.X, (int)size.Y);
            _lastWinSize = size;
        }

        public virtual void OnMove(Vector2 pos)
        {
            _lastWinPos = pos;
        }

        public virtual void OnFocus(bool focused)
        {
            
        }

        public virtual void OnIconify(bool iconified)
        {
            
        }
        
        public virtual void OnMaximize(bool maximized)
        {
            
        }
    }

    public struct WindowParameters
    {
        public int Width; 
        public int Height;
        public string Title;

        public bool IsFocused;
        public bool IsFloating;
        public bool IsMaximized;
        public bool IsDecorated;
        public bool IsResizable;
        public bool IsVisible;
        public bool IsCursorCentered;
        public bool IsFocusedOnShow;

        public string Version;
        public int MinorVersion;
        public int MajorVersion;

        public int DepthBits;
        public int StencilBits;
        public bool VSync;

        public ColorRgb ClearColorArgb;

        public static WindowParameters Default(string name) => Default(name, 960 * 2, 540 * 2);
        public static WindowParameters Default(string name, int width, int height)
        {
            return new WindowParameters()
            {
                Width = width,
                Height = height,
                Title = name,
                
                IsFloating = true,
                IsFocused = false,
                IsMaximized = false,
                IsDecorated = true,
                IsResizable = true,
                IsVisible = false,
                IsCursorCentered = false,
                IsFocusedOnShow = true,
                
                Version = "0.0.1",
                MinorVersion = 3,
                MajorVersion = 4,
                
                DepthBits = 24,
                StencilBits = 8,
                VSync = true,
                
                ClearColorArgb = ColorRgb.Black
            };
        }
    }
}
