using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using Emission.IO;
using Emission.Graphics;
using Emission.Window.GLFW;
using static Emission.Graphics.GL.GL;
using Emission.Mathematics;
using System.Reflection;

namespace Emission.Window
{
    public sealed class Window : IDisposable, IEngineBehaviour
    {
        /// <summary>
        /// Pointer to Glfw Window object. Represent window. Public get and can be only set class constructor.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Structure that contains all information to generate window.
        /// Information cannot be change, so it can be used to get starting value for the width or the title for example.
        /// </summary>
        public WindowParameters Parameters { get; }

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
        /// Gets the clear color of the window.
        /// </summary>
        public ColorRgb ClearColor
        {
            get => Parameters.ClearColorRgb;
        }

        /// <summary>
        /// Viewport of the window.
        /// Countains WindowSize and Window Position
        /// </summary>
        public Viewport Viewport => new Viewport(0, 0, WindowSize.X, WindowSize.Y);

        /// <summary>
        /// Engine Behaviour of the Window.
        /// </summary>
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
                throw new EmissionException(Errors.EmissionGlfwException, error);
            }

            Glfw.WindowHint(WindowHint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(WindowHint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(WindowHint.ContextVersionMinor, Parameters.MinorVersion);
            Glfw.WindowHint(WindowHint.ContextVersionMajor, Parameters.MajorVersion);
            Glfw.WindowHint(WindowHint.Focused, Parameters.IsFocused);
            Glfw.WindowHint(WindowHint.Floating, Parameters.IsFloating);
            Glfw.WindowHint(WindowHint.Maximized, Parameters.IsMaximized);
            Glfw.WindowHint(WindowHint.Decorated, Parameters.IsDecorated);
            Glfw.WindowHint(WindowHint.Resizable, Parameters.IsResizable);
            Glfw.WindowHint(WindowHint.Visible, Parameters.IsVisible);
            Glfw.WindowHint(WindowHint.CenterCursor, Parameters.IsCursorCentered);
            Glfw.WindowHint(WindowHint.FocusOnShow, Parameters.IsFocusedOnShow);
            
            Handle = Glfw.CreateWindow(Parameters.Width, Parameters.Height, Parameters.Title, Monitor.None, IntPtr.Zero);
            if (Handle == IntPtr.Zero) throw new EmissionException(Errors.EmissionGlfwException, "Failed to create GLFW Window!");

            Debug.Log("[INFO] New window has been created!");
            
            Glfw.MakeContextCurrent(Handle);
            Import(Glfw.GetProcAddress);
            
            Debug.Log($"[INFO] Using OpenGL {glGetString(GL_VERSION)}");
            Debug.Log($"[INFO] Using GLSL {glGetString(GL_SHADING_LANGUAGE_VERSION)}");
            Debug.Log($"[INFO] Running with OpenGL Vendor {glGetString(GL_VENDOR)}");
            Debug.Log($"[INFO] Running with OpenGL Renderer {glGetString(GL_RENDERER)}");

            if (!string.IsNullOrEmpty(parameters.Icon))
                SetIcon(parameters.Icon);
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
            Glfw.SetWindowFocusCallback(Handle, (_, focused) => Event.Invoke(Event.WindowFocus, focused));
            Glfw.SetWindowIconifyCallback(Handle, (_, minimized) => Event.Invoke(Event.WindowIconify, minimized));
            Glfw.SetWindowMaximizeCallback(Handle, (_, maximized) => Event.Invoke(Event.WindowMaximize, maximized));

            Glfw.SetKeyCallback(Handle, (_, key, _, action, _) => Event.Invoke<(Keys, InputState)>(Event.Key, (key, action)));
            Glfw.SetMouseButtonCallback(Handle, (_, button, action, _) => Event.Invoke<(MouseButton, InputState)>(Event.Button, (button, action)));
            Glfw.SetScrollCallback(Handle, (_, _, y) => Event.Invoke(Event.MouseScroll, y));
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
            glClearColor(Parameters.ClearColorRgb.R, Parameters.ClearColorRgb.G, Parameters.ClearColorRgb.B, 0);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        }

        public void Swap()
        {
            Glfw.SwapBuffers(Handle);
        }

        public void SetIcon(string path)
        {
            Glfw.SetWindowIcon(Handle, 1, new []{new Icon(path)});
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
        
        public void OnClose()
        {
            GameController.Stop(0);
        }
        
        public void OnResize(Vector2 size)
        {
            glViewport(0, 0, (int)size.X, (int)size.Y);
            _lastWinSize = size;
        }

        public void OnMove(Vector2 pos)
        {
            _lastWinPos = pos;
        }

        public void OnFocus(bool focused)
        {
            
        }

        public void OnIconify(bool iconified)
        {
            
        }
        
        public void OnMaximize(bool maximized)
        {
            
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowParameters
    {
        public int Width { get; set; } 
        public int Height { get; set; } 
        public string Title { get; set; } 
        public string Icon { get; set; }

        public bool IsFocused { get; set; } 
        public bool IsFloating { get; set; } 
        public bool IsMaximized { get; set; } 
        public bool IsDecorated { get; set; } 
        public bool IsResizable { get; set; } 
        public bool IsVisible { get; set; } 
        public bool IsCursorCentered { get; set; } 
        public bool IsFocusedOnShow { get; set; } 

        public string Version { get; set; } 
        public int MinorVersion { get; set; } 
        public int MajorVersion { get; set; } 

        public int DepthBits { get; set; } 
        public int StencilBits { get; set; } 

        [JsonIgnore]
        public ColorRgb ClearColorRgb;

        public static WindowParameters Default(string name) => Default(name, 960 * 2, 540 * 2);
        public static WindowParameters Default(string name, int width, int height)
        {
            return new WindowParameters()
            {
                Width = width,
                Height = height,
                Title = name,
                Icon = null,
                
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
                
                ClearColorRgb = ColorRgb.Black
            };
        }

        public static WindowParameters FromJson(string path)
        {
            return Json.Deserialize<WindowParameters>(path);
        }
    }
}
