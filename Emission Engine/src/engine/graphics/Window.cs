using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Emission.Math;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Emission
{
    public unsafe class Window
    {
        /// <summary>
        /// Pointer to GLFW Window object. Represent window. Public get and can be only set class constructor.
        /// Pointer to <see cref="OpenTK.Windowing.GraphicsLibraryFramework.Window"/>
        /// </summary>
        public OpenTK.Windowing.GraphicsLibraryFramework.Window* CurrentWindow { get; }
        
        /// <summary>
        /// Defines the interface for OpenGL context management.
        /// Extends from <see cref="OpenTK.Windowing.Common.IGraphicsContext"/>
        /// </summary>
        public IGLFWGraphicsContext Context { get; }

        /// <summary>
        /// Structure that contains all information to generate window.
        /// Information cannot be change, so it can be used to get starting value for the width or the title for example.
        /// Also contains Projection parameters for projections matrices for current <see cref="Camera"/>.
        /// </summary>
        public WindowSettings Settings { get; }

        /// <summary>
        /// Return, as float value, the relationship between the width and the height of the screen.
        /// Calculate with <see cref="_lastWindowPosition"/>.
        /// Cannot be set, readonly.
        /// </summary>
        public float WindowAspect
        {
            get => (float)_lastWindowSize.X / (float)_lastWindowSize.Y;
        }

        /// <summary>
        /// Use a Vector2 with int values to get window's position in computer screen space.
        /// Can be set or get.
        /// Call <see cref="GLFW.SetWindowPos"/> to define position.
        /// </summary>
        public Vector2i WindowPosition
        {
            get => _lastWindowPosition;
            set => GLFW.SetWindowPos(CurrentWindow, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Use a Vector2 with int values to get Window current size in pixels.
        /// Can be set or get.
        /// Call <see cref="GLFW.SetWindowSize"/> to define size.
        /// </summary>
        public Vector2 WindowSize
        {
            get => _lastWindowSize;
            set => GLFW.SetWindowSize(CurrentWindow, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Return a boolean when the window is maximized on the current monitor.
        /// Can be set or get.
        /// When is set, it maximized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Maximized
        {
            get => GLFW.GetWindowAttrib(CurrentWindow, WindowAttributeGetBool.Maximized);
            set
            {
                if(value) GLFW.MaximizeWindow(CurrentWindow);
                else GLFW.RestoreWindow(CurrentWindow);
            }
        }

        /// <summary>
        /// Return a boolean when the window is minimized on the current monitor.
        /// Can be set or get.
        /// When is set, it minimized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Minimized
        {
            get => GLFW.GetWindowAttrib(CurrentWindow, WindowAttributeGetBool.AutoIconify);
            set
            {
                if(value) GLFW.IconifyWindow(CurrentWindow);
                else GLFW.RestoreWindow(CurrentWindow);
            }
        }

        /// <summary>
        /// Return or set window's title. Use private variable <see cref="_title"/> to save current title.
        /// When the title need to change, it define the private variable and then change title using
        /// <see cref="GLFW.SetWindowTitle"/>.
        /// </summary>
        [MaybeNull]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                GLFW.SetWindowTitle(CurrentWindow, value ?? String.Empty);
            }
        }

        /// <summary>
        /// Return if the window is visible or not. Use attribute to get the value.
        /// If visibility need the be set, if it's true, the window will be show, otherwise the window will be hide.
        /// </summary>
        public bool Visible
        {
            get => GLFW.GetWindowAttrib(CurrentWindow, WindowAttributeGetBool.Visible);
            set
            {
                if(value) GLFW.ShowWindow(CurrentWindow);
                else GLFW.HideWindow(CurrentWindow);
            }
        }

        /// <summary>
        /// Warper for <see cref="GLFW.GetWindowAttrib"/> with <see cref="WindowAttributeGetBool.Focused"/> while
        /// returning value and <see cref="GLFW.FocusWindow"/> when value is set.
        /// </summary>
        public bool Focus
        {
            get => GLFW.GetWindowAttrib(CurrentWindow, WindowAttributeGetBool.Focused);
            set => GLFW.FocusWindow(CurrentWindow);
        }

        /// <summary>
        /// Warper for <see cref="GLFW.WindowShouldClose"/> while returning value
        /// and <see cref="GLFW.SetWindowShouldClose"/> when value is set.
        /// </summary>
        public bool ShouldClose
        {
            get => GLFW.WindowShouldClose(CurrentWindow);
            set => GLFW.SetWindowShouldClose(CurrentWindow, value);
        }

        // Events
        public event GLFWCallbacks.WindowCloseCallback CloseCallbackEvent;
        public event GLFWCallbacks.WindowSizeCallback ResizeCallbackEvent;
        public event GLFWCallbacks.WindowPosCallback PositionCallbackEvent;
        public event GLFWCallbacks.KeyCallback KeyCallbackEvent;
        public event GLFWCallbacks.MouseButtonCallback MouseButtonCallbackEvent;
        public event GLFWCallbacks.CursorPosCallback CursorPositionCallbackEvent;
        public event GLFWCallbacks.ScrollCallback MouseScrollCallbackEvent;
        
        // private variables
        private Vector2i _lastWindowSize;
        private Vector2i _lastWindowPosition;

        private string _title;

        // constructor
        public Window(WindowSettings settings)
        {
            this.Settings = settings;
            this._title = settings.Title;

            // Load GLFW. VERY IMPORTANT
            GLFW.Init();

            // Define Window Hints
            GLFW.WindowHint(WindowHintInt.ContextVersionMinor, Settings.APIversion);
            GLFW.WindowHint(WindowHintInt.ContextVersionMajor, Settings.APIversion);
            GLFW.WindowHint(WindowHintBool.Decorated, true);
            GLFW.WindowHint(WindowHintBool.Resizable, true);
            GLFW.WindowHint(WindowHintBool.Focused, true);
            GLFW.WindowHint(WindowHintBool.Visible, false);
            GLFW.WindowHint(WindowHintClientApi.ClientApi,
                Settings.IsOpenES ? ClientApi.OpenGlEsApi : ClientApi.OpenGlApi);

            // Setup Context
            CurrentWindow = GLFW.CreateWindow(Settings.Width * Settings.Scale, Settings.Height * Settings.Scale, Settings.Title, null, null);
            Context = new GLFWGraphicsContext(CurrentWindow);
            Context.MakeCurrent();

            // Load OpenGL Bindings
            int status = LoadBindings();
            if(status == -1)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot load bindings.");
                Application.Current.Stop(-1);
            }

            // Update title with window settings
            if (Settings.ShowOpenGLVersion)
            {
                Title = Settings.Title + " - OpenGL V" + GL.GetString(StringName.Version);
            }

            // Define all callbacks
            BindCallbacks();

            // OpenGL window parameters
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthClamp);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // Update Viewport
            OnWindowResize(0, 0);
        }

        public void Update()
        {
            // Update Events
            GLFW.PollEvents();
        }

        public void PreRender()
        {
            GL.ClearColor(0.13f, 0.22f, 0.4f, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Flush();
        }

        public void PostRender()
        {
            GLFW.SwapBuffers(CurrentWindow);
        }

        public void Destroy()
        {
            GLFW.DestroyWindow(CurrentWindow);
            GLFW.Terminate();
        }

        private void OnWindowClose()
        {
            Application.Current.Stop(0);
        }

        private void OnWindowResize(int width, int height)
        {
            GLFW.GetWindowSize(CurrentWindow, out var windowW, out var windowH);

            _lastWindowSize = new Vector2i((int)windowW, (int)windowH);
            GL.Viewport(0, 0, (int)windowW, (int)windowH);
            if(Camera.Main != null) Camera.Main.Update(); 
        }

        private void OnWindowMove(int x, int y)
        {
            GLFW.GetWindowPos(CurrentWindow, out int windowX, out int windowY);
            _lastWindowPosition = new Vector2i(windowX, windowY);
        }

        /// <summary>
        /// Use refraction to initialize Gl bindings.
        /// Code from <see href="https://github.com/opentk/opentk/blob/master/src/OpenTK.Windowing.Desktop/NativeWindow.cs">Native Window</see> in
        /// OpenTk source code.
        /// Return loading status, zero when is done and -1 when there is an error or it cannot be loaded.
        /// </summary>
        /// <returns>Loading status</returns>
        private int LoadBindings()
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load("OpenTK.Graphics");
            }
            catch
            {
                return -1;
            }

            var provider = new GLFWBindingsContext();

            void LoadBindings(string typeNamespace)
            {
                var type = assembly.GetType($"OpenTK.Graphics.{typeNamespace}.GL");
                if (type == null)
                {
                    return;
                }

                var load = type.GetMethod("LoadBindings");
                load.Invoke(null, new object[] { provider });
            }

            if (Settings.IsOpenES)
            {
                LoadBindings("ES11");
                LoadBindings("ES20");
                LoadBindings("ES30");
            }
            LoadBindings("OpenGL");
            LoadBindings("OpenGL4");

            // Display OpenGL Strings into Console so that you have information for debugging without have to search in engine.
            ApplicationConsole.Print("**** OpenGL Strings ****");
            ApplicationConsole.Print("[GL INFO] Renderer                 : " + GL.GetString(StringName.Renderer));
            ApplicationConsole.Print("[GL INFO] Shading Language Version : " + GL.GetString(StringName.ShadingLanguageVersion));
            ApplicationConsole.Print("[GL INFO] Graphic Card             : " + GL.GetString(StringName.Vendor));
            ApplicationConsole.Print("[GL INFO] OpenGL Version           : " + GL.GetString(StringName.Version));
            ApplicationConsole.Print("************************");

            return 0;
        }

        /// <summary>
        /// Load all events and callbacks
        /// </summary>
        private void BindCallbacks()
        {
            CloseCallbackEvent += (_) => OnWindowClose();
            ResizeCallbackEvent += (_, x, y) => OnWindowResize(x, y);
            PositionCallbackEvent += (_, x, y) => OnWindowMove(x, y);
            
            Input input = Input.Current;
            KeyCallbackEvent += (_, key, code, state, mods) => input.KeyCallback(key, code, state, mods);
            MouseButtonCallbackEvent += (_, button, state, mod) => input.MouseCallback(button, state, mod);
            CursorPositionCallbackEvent += (_, x, y) => input.CursorPosition(x, y);
            MouseScrollCallbackEvent += (_, x, y) => input.ScrollCallback(y);
            
            // Window Callbacks
            GLFW.SetWindowCloseCallback(CurrentWindow, CloseCallbackEvent);
            GLFW.SetWindowSizeCallback(CurrentWindow, ResizeCallbackEvent);
            GLFW.SetWindowPosCallback(CurrentWindow, PositionCallbackEvent);
            
            // Input callbacks
            GLFW.SetKeyCallback(CurrentWindow, KeyCallbackEvent);
            GLFW.SetScrollCallback(CurrentWindow, MouseScrollCallbackEvent);
            GLFW.SetMouseButtonCallback(CurrentWindow, MouseButtonCallbackEvent);
            GLFW.SetCursorPosCallback(CurrentWindow, CursorPositionCallbackEvent);
        }

        // Static getter for the Window, call from current Application.
        public static Window Current { get => Application.Current.Window; }
    }
}
