using System;
using System.Reflection;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Emission.Math;

namespace Emission
{
    unsafe class Window
    {
        public OpenTK.Windowing.GraphicsLibraryFramework.Window* CurrentWindow { get; }

        public IGLFWGraphicsContext Context { get; private set; }

        public WindowSettings Settings { get; private set; }

        public Vector2 WindowPosition
        {
            get => _lastWindowPosition;
            set
            {
                GLFW.SetWindowPos(CurrentWindow, (int)value.X, (int)value.Y);
            }
        }

        public Vector2 WindowSize
        {
            get => _lastWindowSize;
            set
            {
                GLFW.SetWindowSize(CurrentWindow, (int)value.X, (int)value.Y);
            }
        }

        public Matrix4 WindowProjection
        {
            get
            {
                switch (Settings.Projection)
                {
                    case WindowSettings.WindowProjection.Orthographic:
                        return MatrixHelper.CreateOrtho(_lastWindowSize.X, _lastWindowSize.Y, Settings.NearPlane, Settings.FarPlane);

                    case WindowSettings.WindowProjection.Perspective:
                        return MatrixHelper.CreatePerspective(Settings.FieldOfView, _lastWindowSize.X, _lastWindowSize.Y, Settings.NearPlane, Settings.FieldOfView);
                }

                return Matrix4.Zero;
            }
        }

        private Vector2i _lastWindowSize;
        private Vector2 _lastWindowPosition;
        
        private bool _isVisible;
        private bool _isFocus;

        public Window(WindowSettings Settings)
        {
            this.Settings = Settings;

            GLFW.Init();

            GLFW.WindowHint(WindowHintInt.ContextVersionMinor, Settings.APIversion);
            GLFW.WindowHint(WindowHintInt.ContextVersionMajor, Settings.APIversion);
            GLFW.WindowHint(WindowHintBool.Decorated, true);
            GLFW.WindowHint(WindowHintBool.Resizable, true);
            GLFW.WindowHint(WindowHintBool.Focused, true);
            GLFW.WindowHint(WindowHintBool.Visible, false);

            if (Settings.IsOpenES)
            {
                // Define parameters when OpenES is use.
                GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlEsApi);
            }
            else
            {
                // Define parameters when OpenGL is use.
                GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlApi);
            }

            CurrentWindow = GLFW.CreateWindow(Settings.Width * Settings.Scale, Settings.Height * Settings.Scale, Settings.Title, null, null);
            Context = new GLFWGraphicsContext(CurrentWindow);
            Context.MakeCurrent();

            int status = LoadBindings();
            if(status == -1)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot load bindings.");
                Application.Current.Stop(-1);
            }

            if (Settings.ShowOpenGLVersion)
            {
                GLFW.SetWindowTitle(CurrentWindow, Settings.Title + " - OpenGL V" + GL.GetString(StringName.Version));
            }

            GLFW.SetWindowCloseCallback(CurrentWindow, WindowCloseCallback);
            GLFW.SetWindowSizeCallback(CurrentWindow, WindowResizeCallback);
            GLFW.SetWindowPosCallback(CurrentWindow, WindowMoveCallback);
            GLFW.SetKeyCallback(CurrentWindow, Input.KeyCallback);
            GLFW.SetMouseButtonCallback(CurrentWindow, Input.MouseCallback);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            WindowResizeCallback(CurrentWindow, Settings.Width, Settings.Height);
        }

        public void Update()
        {
            _isFocus = GLFW.GetWindowAttrib(CurrentWindow, WindowAttributeGetBool.Focused);
        }

        public void PreRender()
        {
            GL.ClearColor(0.13f, 0.22f, 0.4f, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Flush();
        }

        public void PostRender()
        {
            GLFW.SwapBuffers(CurrentWindow);
            GLFW.WaitEvents();
        }

        public void Show()
        {
            GLFW.ShowWindow(CurrentWindow);
            _isVisible = true;
        }

        public void Hide()
        {
            GLFW.HideWindow(CurrentWindow);
            _isVisible = false;
        }

        public void Destroy()
        {
            GLFW.DestroyWindow(CurrentWindow);
            GLFW.Terminate();
        }

        private void WindowCloseCallback(OpenTK.Windowing.GraphicsLibraryFramework.Window* window)
        {
            Application.Current.Stop(0);
        }

        private void WindowResizeCallback(OpenTK.Windowing.GraphicsLibraryFramework.Window* window, int width, int height)
        {
            GLFW.GetWindowSize(CurrentWindow, out var windowW, out var windowH);

            _lastWindowSize = new Vector2i((int)windowW, (int)windowH);
            GL.Viewport(0, 0, (int)windowW, (int)windowH);
        }

        private void WindowMoveCallback(OpenTK.Windowing.GraphicsLibraryFramework.Window* window, int x, int y)
        {
            GLFW.GetWindowPos(CurrentWindow, out var windowX, out var windowY);
            _lastWindowPosition = new Vector2((float)windowX, (float)windowY);
        }

        #region State

        public bool ShouldClose()
        {
            return GLFW.WindowShouldClose(CurrentWindow);
        }

        public bool IsVisible()
        {
            return _isVisible;
        }

        public bool IsFocus()
        {
            return _isFocus;
        }

        public static Window Current
        {
            get => Application.Current.Window;
        }

        #endregion

        #region Private Methods

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

            ApplicationConsole.Print("**** OpenGL Strings ****");
            ApplicationConsole.Print("[GL INFO] Renderer                 : " + GL.GetString(StringName.Renderer));
            ApplicationConsole.Print("[GL INFO] Shading Language Version : " + GL.GetString(StringName.ShadingLanguageVersion));
            ApplicationConsole.Print("[GL INFO] Graphic Card             : " + GL.GetString(StringName.Vendor));
            ApplicationConsole.Print("[GL INFO] OpenGL Version           : " + GL.GetString(StringName.Version));
            ApplicationConsole.Print("************************");

            return 0;
        }
        #endregion

    }
}
