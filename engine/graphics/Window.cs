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
    public unsafe class Window : IEngineBehaviour
    {
        /// <summary>
        /// Pointer to GLFW Window object. Represent window. Public get and can be only set class constructor.
        /// Pointer to <see cref="OpenTK.Windowing.GraphicsLibraryFramework.Window"/>
        /// </summary>
        public OpenTK.Windowing.GraphicsLibraryFramework.Window* WindowPointer { get; }
        
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
            set => GLFW.SetWindowPos(WindowPointer, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Use a Vector2 with int values to get Window current size in pixels.
        /// Can be set or get.
        /// Call <see cref="GLFW.SetWindowSize"/> to define size.
        /// </summary>
        public Vector2 WindowSize
        {
            get => _lastWindowSize;
            set => GLFW.SetWindowSize(WindowPointer, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Return a boolean when the window is maximized on the current monitor.
        /// Can be set or get.
        /// When is set, it maximized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Maximized
        {
            get => GLFW.GetWindowAttrib(WindowPointer, WindowAttributeGetBool.Maximized);
            set
            {
                if(value) GLFW.MaximizeWindow(WindowPointer);
                else GLFW.RestoreWindow(WindowPointer);
            }
        }

        /// <summary>
        /// Return a boolean when the window is minimized on the current monitor.
        /// Can be set or get.
        /// When is set, it minimized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Minimized
        {
            get => GLFW.GetWindowAttrib(WindowPointer, WindowAttributeGetBool.AutoIconify);
            set
            {
                if(value) GLFW.IconifyWindow(WindowPointer);
                else GLFW.RestoreWindow(WindowPointer);
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
                GLFW.SetWindowTitle(WindowPointer, value ?? String.Empty);
            }
        }

        /// <summary>
        /// Return if the window is visible or not. Use attribute to get the value.
        /// If visibility need the be set, if it's true, the window will be show, otherwise the window will be hide.
        /// </summary>
        public bool Visible
        {
            get => GLFW.GetWindowAttrib(WindowPointer, WindowAttributeGetBool.Visible);
            set
            {
                if(value) GLFW.ShowWindow(WindowPointer);
                else GLFW.HideWindow(WindowPointer);
            }
        }

        /// <summary>
        /// Warper for <see cref="GLFW.GetWindowAttrib"/> with <see cref="WindowAttributeGetBool.Focused"/> while
        /// returning value and <see cref="GLFW.FocusWindow"/> when value is set.
        /// </summary>
        public bool Focus
        {
            get => GLFW.GetWindowAttrib(WindowPointer, WindowAttributeGetBool.Focused);
            set => GLFW.FocusWindow(WindowPointer);
        }

        /// <summary>
        /// Warper for <see cref="GLFW.WindowShouldClose"/> while returning value
        /// and <see cref="GLFW.SetWindowShouldClose"/> when value is set.
        /// </summary>
        public bool ShouldClose
        {
            get => GLFW.WindowShouldClose(WindowPointer);
            set => GLFW.SetWindowShouldClose(WindowPointer, value);
        }

        public Vector4 ClearColor
        {
            get => _clearColor;
            set => _clearColor = new Color(255, value.X, value.Y, value.Z);
        }
        
        public IEngineBehaviour Behaviour {get => this;}

        // Events
        public event GLFWCallbacks.WindowCloseCallback CloseCallbackEvent;
        public event GLFWCallbacks.WindowSizeCallback ResizeCallbackEvent;
        public event GLFWCallbacks.WindowPosCallback PositionCallbackEvent;
        public event GLFWCallbacks.WindowFocusCallback FocusCallbackEvent;
        public event GLFWCallbacks.KeyCallback KeyCallbackEvent;
        public event GLFWCallbacks.MouseButtonCallback MouseButtonCallbackEvent;
        public event GLFWCallbacks.CursorPosCallback CursorPositionCallbackEvent;
        public event GLFWCallbacks.ScrollCallback MouseScrollCallbackEvent;
        
        // private variables
        private Vector2i _lastWindowSize;
        private Vector2i _lastWindowPosition;
        private Color _clearColor;

        private string _title;

        // constructor
        public Window(WindowSettings settings)
        {
            Settings = settings;
            _title = settings.Title;
            ClearColor = new Color(255, 0, 0, 0);

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
            WindowPointer = GLFW.CreateWindow((int)(Settings.Width * Settings.Scale), (int)(Settings.Height * Settings.Scale), Settings.Title, null, null);
            Context = new GLFWGraphicsContext(WindowPointer);
            Context.MakeCurrent();

            GLFW.SetWindowPos(WindowPointer, Settings.XPos, Settings.YPos);
            
            // Load OpenGL Bindings
            int status = LoadBindings();
            if(status == -1)
            {
                Debug.LogError("[ERROR] Cannot load bindings.");
                Application.Singleton.Stop(-1);
            }

            // Update title with window settings
            if (Settings.ShowOpenGLVersion)
            {
                Title = Settings.Title + " - OpenGL V" + GL.GetString(StringName.Version);
            }

            // Define all callbacks
            BindCallbacks(); // GLFW and OpenGL Callbacks
            Behaviour.BindCallbacks(); // Engine Callbacks

            // OpenGL window parameters
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            
            GL.DepthFunc(DepthFunction.Less);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            // Update Viewport
            GLFW.GetWindowSize(WindowPointer, out var windowW, out var windowH);
            OnWindowResize(windowW, windowH);
        }

        void IEngineBehaviour.Initialize()
        {
            Visible = true;
        }

        void IEngineBehaviour.Start()
        {
            
        }

        void IEngineBehaviour.Update()
        {
            GLFW.PollEvents();
        }

        void IEngineBehaviour.PreRender()
        {
            GL.ClearColor(_clearColor.R, _clearColor.G, _clearColor.B, _clearColor.A);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        void IEngineBehaviour.Render()
        {
            
        }

        void IEngineBehaviour.PostRender()
        {
            GLFW.SwapBuffers(WindowPointer);
        }

        void IEngineBehaviour.Dispose()
        {
            GLFW.DestroyWindow(WindowPointer);
            GLFW.Terminate();
        }

        private void OnWindowClose()
        {
            Application.Singleton.Stop(0);
        }

        private void OnWindowResize(int width, int height)
        {
            Vector2i size = new Vector2i((int)(width * Settings.Scale), (int)(height * Settings.Scale));
            
            // Define Viewport
            _lastWindowSize = size;
            GL.Viewport(0, 0, size.X, size.Y);
            
            // Redefine Camera Viewport
            if (Camera.Main != null) Camera.Main.Viewport = new Vector2(width, height);
        }

        private void OnWindowMove(int x, int y)
        {
            GLFW.GetWindowPos(WindowPointer, out int windowX, out int windowY);
            _lastWindowPosition = new Vector2i(windowX, windowY);
        }

        private void OnWindowFocus(bool isFocus)
        {
            Focus = isFocus;
        }

        /// <summary>
        /// Use refraction to initialize Gl bindings.
        /// Code from <see href="https://github.com/opentk/opentk/blob/master/src/OpenTK.Windowing.Desktop/NativeWindow.cs">Native Window</see>
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
            Debug.Log("**** OpenGL Strings ****");
            Debug.Log("[GL INFO] Renderer                 : " + GL.GetString(StringName.Renderer));
            Debug.Log("[GL INFO] Shading Language Version : " + GL.GetString(StringName.ShadingLanguageVersion));
            Debug.Log("[GL INFO] Graphic Card             : " + GL.GetString(StringName.Vendor));
            Debug.Log("[GL INFO] OpenGL Version           : " + GL.GetString(StringName.Version));
            Debug.Log("************************");

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
            FocusCallbackEvent += (_, v) => OnWindowFocus(v);
            
            Input input = Input.Current;
            KeyCallbackEvent += (_, key, code, state, mods) => input.KeyCallback(key, code, state, mods);
            MouseButtonCallbackEvent += (_, button, state, mod) => input.MouseCallback(button, state, mod);
            CursorPositionCallbackEvent += (_, x, y) => input.CursorPosition(x, y);
            MouseScrollCallbackEvent += (_, x, y) => input.ScrollCallback(y);
            FocusCallbackEvent += (_, v) => input.FocusCallback(v);
            
            // Window Callbacks
            GLFW.SetWindowCloseCallback(WindowPointer, CloseCallbackEvent);
            GLFW.SetWindowSizeCallback(WindowPointer, ResizeCallbackEvent);
            GLFW.SetWindowPosCallback(WindowPointer, PositionCallbackEvent);
            GLFW.SetWindowFocusCallback(WindowPointer, FocusCallbackEvent);
            
            // Input callbacks
            GLFW.SetKeyCallback(WindowPointer, KeyCallbackEvent);
            GLFW.SetScrollCallback(WindowPointer, MouseScrollCallbackEvent);
            GLFW.SetMouseButtonCallback(WindowPointer, MouseButtonCallbackEvent);
            GLFW.SetCursorPosCallback(WindowPointer, CursorPositionCallbackEvent);
        }

        // Static getter for the Window, call from current Application.
        public static Window Singleton { get => Application.Singleton.Window; }
    }
}
