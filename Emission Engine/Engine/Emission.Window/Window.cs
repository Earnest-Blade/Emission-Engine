using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Emission.IO;
using Emission.Graphics;
using Emission.Natives.GL;
using Emission.Window.GLFW;
using Emission.Mathematics;
using static Emission.Natives.GL.Gl;

namespace Emission.Window
{
    public sealed unsafe class Window : IDisposable, IEngineBehaviour
    {
        /// <summary>
        /// Pointer to Glfw Window object. Represent window. Public get and can be only set class constructor.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Structure that contains all information to generate window.
        /// Information cannot be change, so it can be used to get starting value for the width or the title for example.
        /// </summary>
        public WindowConfig Config { get; }

        /// <summary>
        /// Return, as float value, the relationship between the width and the height of the screen.
        /// Calculate with <see cref="_lastWinSize"/>.
        /// Cannot be set, readonly.
        /// </summary>
        public float WindowAspect => _lastWinSize.X / _lastWinSize.Y;

        /// <summary>
        /// Use a Vector2 with int values to get window's position in computer screen space.
        /// Can be set or get.
        /// Call <see cref="Glfw.SetWindowPosition"/> to define position.
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
        /// <see cref="Glfw.SetWindowTitle(IntPtr, byte[])"/>.
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
        /// Warper for <see cref="Glfw.GetWindowAttribute(System.IntPtr,int)"/> with <see cref="WindowAttribute.Focused"/> while
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
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value));
                Glfw.SetWindowOpacity(Handle, value);
            }
        }

        /// <summary>
        /// Get the color use to clear the window.
        /// </summary>
        public ColorRgb ClearColor { get => _clearColor; set => _clearColor = value; }

        /// <summary>
        /// Get Window's Icon.
        /// </summary>
        public Icon WindowIcon => _windowIcon;

        /// <summary>
        /// Viewport of the window.
        /// Contains WindowSize and Window Position
        /// </summary>
        public Viewport Viewport => new Viewport(0, 0, WindowSize.X, WindowSize.Y, 0.1f, 400f);

        /// <summary>
        /// Engine Behaviour of the Window.
        /// </summary>
        public IEngineBehaviour Behaviour => this;
        public bool IsActive { get => true; set {} }

        private string _title;
        private Vector2 _lastWinSize;
        private Vector2 _lastWinPos;
        private ColorRgb _clearColor;
        private Icon _windowIcon;

        public Window(WindowConfig config)
        {
            Config = config;
            _title = config.Title;
            _lastWinSize = (Config.Width, Config.Height);
            _lastWinPos = Vector2.Zero;
            _clearColor = ColorRgb.Black;

            if (!Glfw.Init())
            {
                Glfw.GetError(out string error);
                throw new EmissionException(EmissionErrors.EmissionGlfwException, error);
            }

            Glfw.WindowHint(WindowHint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(WindowHint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(WindowHint.ContextVersionMinor, Config.MinorVersion);
            Glfw.WindowHint(WindowHint.ContextVersionMajor, Config.MajorVersion);
            Glfw.WindowHint(WindowHint.Focused, Config.IsFocused);
            Glfw.WindowHint(WindowHint.Floating, Config.IsFloating);
            Glfw.WindowHint(WindowHint.Maximized, Config.IsMaximized);
            Glfw.WindowHint(WindowHint.Decorated, Config.IsDecorated);
            Glfw.WindowHint(WindowHint.Resizable, Config.IsResizable);
            Glfw.WindowHint(WindowHint.Visible, Config.IsVisible);
            Glfw.WindowHint(WindowHint.CenterCursor, Config.IsCursorCentered);
            Glfw.WindowHint(WindowHint.FocusOnShow, Config.IsFocusedOnShow);
            Glfw.WindowHint(WindowHint.DepthBits, Config.DepthBits);
            Glfw.WindowHint(WindowHint.StencilBits, Config.StencilBits);

            Glfw.WindowHint(WindowHint.OpenglDebugContext, GameInstance.EngineSettings.Debug ? GL_TRUE : GL_FALSE);

            Handle = Glfw.CreateWindow(Config.Width, Config.Height, Config.Title, Monitor.None, IntPtr.Zero);
            if (Handle == IntPtr.Zero) throw new EmissionException(EmissionErrors.EmissionGlfwException, "Failed to create GLFW Window!");

            AssignContext();
            Debug.Log("[INFO] New window has been created!");
            Debug.Log($"[INFO] Running with GLFW {Glfw.Version}");

            if (!string.IsNullOrEmpty(config.Icon))
                SetIcon(config.Icon);
        }

        /// <summary>
        /// Initialize Window.
        /// Enable OpenGl, initialize Event Delegates and Glfw inputs.
        /// </summary>
        public void Initialize()
        {
            Event.AddDelegate(Event.WindowClose, OnClose);
            Event.AddDelegate<Vector2>(Event.WindowResize, OnResize);
            Event.AddDelegate<Vector2>(Event.WindowMove, OnMove);
            Event.AddDelegate<bool>(Event.WindowIconify, OnIconify);
            Event.AddDelegate<bool>(Event.WindowMaximize, OnMaximize);
            Event.AddDelegate<bool>(Event.WindowFocus, OnFocus);
            
            Input ptr = GameInstance.Input;
            Event.AddDelegate<(Keys, InputState)>(Event.Key, ptr.KeyCallback);
            Event.AddDelegate<(MouseButton, InputState)>(Event.Button, ptr.MouseCallback);
            Event.AddDelegate<double>(Event.MouseScroll, ptr.ScrollCallback);
            Event.AddDelegate<Vector2>(Event.MouseMove, ptr.CursorCallback);
            
            Glfw.SetCloseCallback(Handle, _ => Event.Invoke(Event.WindowClose));
            Glfw.SetWindowSizeCallback(Handle, (_, width, height) => Event.Invoke<Vector2>(Event.WindowResize, (width, height)));
            Glfw.SetWindowPositionCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.WindowMove, ((float)x, (float)y)));
            Glfw.SetWindowFocusCallback(Handle, (_, focused) => Event.Invoke(Event.WindowFocus, focused));
            Glfw.SetWindowIconifyCallback(Handle, (_, minimized) => Event.Invoke(Event.WindowIconify, minimized));
            Glfw.SetWindowMaximizeCallback(Handle, (_, maximized) => Event.Invoke(Event.WindowMaximize, maximized));

            Glfw.SetKeyCallback(Handle, (_, key, _, action, _) => Event.Invoke<(Keys, InputState)>(Event.Key, (key, action)));
            Glfw.SetMouseButtonCallback(Handle, (_, button, action, _) => Event.Invoke<(MouseButton, InputState)>(Event.Button, (button, action)));
            Glfw.SetScrollCallback(Handle, (_, _, y) => Event.Invoke(Event.MouseScroll, y));
            Glfw.SetCursorPositionCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.MouseMove, ((float)x, (float)y)));
        }

        /// <summary>
        /// Enable window.
        /// </summary>
        public void Start()
        {
            Visible = true;
        }

        /// <summary>
        /// Update window.
        /// </summary>
        public void Update()
        {
            Glfw.PollEvents();
        }

        /// <summary>
        /// Clear window color.
        /// </summary>
        public void Render()
        {
            glClearColor(_clearColor.R, _clearColor.G, _clearColor.B, 0);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        }

        /// <summary>
        /// Swap Glfw buffers.
        /// </summary>
        public void Swap()
        {
            Glfw.SwapBuffers(Handle);
        }

        /// <summary>
        /// Make current thread as Glfw Context. Load OpenGl Context.
        /// </summary>
        public void AssignContext()
        {
            Glfw.MakeContextCurrent(Handle);
            GlLoader.Initialize(Glfw.GetProcAddress);
        }

        /// <summary>
        /// Define Window's Icon using an image.
        /// </summary>
        /// <param name="path">Path to the image</param>
        public void SetIcon(string path)
        {
            _windowIcon = new Icon(path);
            Glfw.SetWindowIcon(Handle, 1, new []{_windowIcon});
        }
        
        /// <summary>
        /// Dispose window.
        /// </summary>
        public void Stop()
        {
            Dispose();
        }
        
        /// <summary>
        /// Destroy and terminate window.
        /// </summary>
        public void Dispose()
        {
            Glfw.DestroyWindow(Handle);
            Glfw.Terminate();
        }

        private void OnClose()
        {
            GameController.Stop(0);
        }

        private void OnResize(Vector2 size)
        {
            glViewport(0, 0, (int)size.X, (int)size.Y);
            _lastWinSize = size;
        }

        private void OnMove(Vector2 pos)
        {
            _lastWinPos = pos;
        }

        private void OnFocus(bool focused)
        {
            
        }

        private void OnIconify(bool iconified)
        {
            
        }

        private void OnMaximize(bool maximized)
        {
            
        }
    }

    
}
