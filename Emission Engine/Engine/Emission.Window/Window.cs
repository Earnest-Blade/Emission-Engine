using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Emission.IO;
using Emission.Graphics;
using Emission.Natives.GL;
using Emission.Natives.GLFW;
using Emission.Mathematics;
using static Emission.Natives.GL.Gl;
using static Emission.Natives.GLFW.Glfw;
using Icon = Emission.IO.Icon;

namespace Emission.Window
{
    public sealed unsafe class Window : IDisposable, IEngineBehaviour
    {
        /// <summary>
        /// Pointer to Glfw Window object. Represent window. Public get and can be only set class constructor.
        /// </summary>
        public GlfwWindow* Handle { get; }

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
            set => glfwSetWindowPos(Handle, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Use a Vector2 with int values to get Window current size in pixels.
        /// Can be set or get.
        /// Call <see cref="Glfw.SetWindowSize"/> to define size.
        /// </summary>
        public Vector2 WindowSize
        {
            get => _lastWinSize;
            set => glfwSetWindowSize(Handle, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Return a boolean when the window is maximized on the current monitor.
        /// Can be set or get.
        /// When is set, it maximized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Maximized
        {
            get => glfwGetWindowAttrib(Handle, GLFW_MAXIMIZED) == GLFW_TRUE;
            set
            {
                if(value) glfwMaximizeWindow(Handle);
                else glfwRestoreWindow(Handle);
            }
        }

        /// <summary>
        /// Return a boolean when the window is minimized on the current monitor.
        /// Can be set or get.
        /// When is set, it minimized the window when value is true and restore his size when it's false.
        /// </summary>
        public bool Minimized
        {
            get => glfwGetWindowAttrib(Handle, GLFW_AUTO_ICONIFY) == GLFW_TRUE;
            set
            {
                if(value) glfwIconifyWindow(Handle);
                else glfwRestoreWindow(Handle);
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
                _title = value ?? String.Empty;
                glfwSetWindowTitle(Handle, GlUtils.StrToBytePtr(_title));
            }
        }
        
        /// <summary>
        /// Return if the window is visible or not. Use attribute to get the value.
        /// If visibility need the be set, if it's true, the window will be show, otherwise the window will be hide.
        /// </summary>
        public bool Visible
        {
            get => glfwGetWindowAttrib(Handle, GLFW_VISIBLE) == GLFW_TRUE;
            set
            {
                if(value) glfwShowWindow(Handle);
                else glfwHideWindow(Handle);
            }
        }

        /// <summary>
        /// Warper for <see cref="Glfw.GetWindowAttribute(System.IntPtr,int)"/> with <see cref="WindowAttribute.Focused"/> while
        /// returning value and <see cref="Glfw.FocusWindow"/> when value is set.
        /// </summary>
        public bool Focus
        {
            get => glfwGetWindowAttrib(Handle, GLFW_FOCUSED) == GLFW_TRUE;
            set => glfwFocusWindow(Handle);
        }

        /// <summary>
        /// Warper for <see cref="Glfw.WindowShouldClose"/> while returning value
        /// and <see cref="Glfw.SetWindowShouldClose"/> when value is set.
        /// </summary>
        public bool ShouldClose
        {
            get => glfwWindowShouldClose(Handle) == GL_TRUE;
            set => glfwSetWindowShouldClose(Handle, value ? GLFW_TRUE : GLFW_FALSE);
        }

        /// <summary>
        /// Gets or sets the opacity of the window in the range of 0.0f and 1.0f.
        /// </summary>
        public float Opacity
        {
            get => glfwGetWindowOpacity(Handle);
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value));
                glfwSetWindowOpacity(Handle, value);
            }
        }

        /// <summary>
        /// Get the color use to clear the window.
        /// </summary>
        public ColorRgb ClearColor { get => _clearColor; set => _clearColor = value; }

        /// <summary>
        /// Get Window's Icon.
        /// </summary>
        public Icon WindowIcon
        {
            get => _windowIcon;
            set
            {
                _windowIcon = value;
                glfwSetWindowIcon(Handle, 1, value);
            }
        }

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

            // If cannot init glfw an error
            if (glfwInit() == 0)
            {
                byte* info;
                glfwGetError(&info);
                string error = GlUtils.PtrToStringUTF8(info);
                
                throw new EmissionException(EmissionErrors.EmissionGlfwException, error);
            }

            glfwSetErrorCallback(GlfwError.ErrorCallback);
            
            glfwWindowHint(GLFW_CLIENT_API, GLFW_OPENGL_API);
            glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
            glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, Config.MajorVersion);
            glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, Config.MinorVersion);
            glfwWindowHint(GLFW_FOCUSED, Config.IsFocused ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_FLOATING, Config.IsFloating ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_MAXIMIZED, Config.IsMaximized ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_DECORATED, Config.IsDecorated ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_RESIZABLE, Config.IsResizable ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_VISIBLE, Config.IsVisible ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_CENTER_CURSOR, Config.IsCursorCentered ? GL_TRUE : GL_FALSE);
            glfwWindowHint(GLFW_FOCUS_ON_SHOW, Config.IsFocusedOnShow ? GL_TRUE : GL_FALSE);
            //glfwWindowHint(GL_DEPTH_BITS, Config.DepthBits);
            //glfwWindowHint(GL_STENCIL_BITS, Config.StencilBits);

            glfwWindowHint(GLFW_OPENGL_DEBUG_CONTEXT, GameInstance.EngineSettings.Debug ? GL_TRUE : GL_FALSE);

            Handle = glfwCreateWindow(Config.Width, Config.Height, GlUtils.StrToBytePtr(Config.Title), null, null);
            if (Handle == (void*)0) 
                throw new EmissionException(EmissionErrors.EmissionGlfwException, "Failed to create GLFW Window!");

            int glfwMajor, glfwMinor, glfwRevision;
            glfwGetVersion(&glfwMajor, &glfwMinor, &glfwRevision);
            
            AssignContext();
            Debug.Log("[INFO] New window has been created!");
            Debug.Log($"[INFO] Running with GLFW {glfwMajor}");

            if (!string.IsNullOrEmpty(config.Icon))
                WindowIcon = new Icon(config.Icon);
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
            
            Event.AddDelegate<(Keys, InputState)>(Event.Key, GameInstance.Input.KeyCallback);
            Event.AddDelegate<(MouseButton, InputState)>(Event.Button, GameInstance.Input.MouseCallback);
            Event.AddDelegate<double>(Event.MouseScroll, GameInstance.Input.ScrollCallback);
            Event.AddDelegate<Vector2>(Event.MouseMove, GameInstance.Input.CursorCallback);
            
            glfwSetWindowCloseCallback(Handle, _ => Event.Invoke(Event.WindowClose));
            glfwSetWindowSizeCallback(Handle, (_, width, height) => Event.Invoke<Vector2>(Event.WindowResize, (width, height)));
            glfwSetWindowPosCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.WindowMove, ((float)x, (float)y)));
            glfwSetWindowFocusCallback(Handle, (_, focused) => Event.Invoke(Event.WindowFocus, focused));
            glfwSetWindowIconifyCallback(Handle, (_, minimized) => Event.Invoke(Event.WindowIconify, minimized));
            glfwSetWindowMaximizeCallback(Handle, (_, maximized) => Event.Invoke(Event.WindowMaximize, maximized));

            glfwSetKeyCallback(Handle, (_, key, _, action, _) => Event.Invoke<(Keys, InputState)>(Event.Key, (key, action)));
            glfwSetMouseButtonCallback(Handle, (_, button, action, _) => Event.Invoke<(MouseButton, InputState)>(Event.Button, (button, action)));
            glfwSetScrollCallback(Handle, (_, _, y) => Event.Invoke(Event.MouseScroll, y));
            glfwSetCursorPosCallback(Handle, (_, x, y) => Event.Invoke<Vector2>(Event.MouseMove, ((float)x, (float)y)));
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
            glfwPollEvents();
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
            glfwSwapBuffers(Handle);
        }

        /// <summary>
        /// Make current thread as Glfw Context. Load OpenGl Context.
        /// </summary>
        public void AssignContext()
        {
            glfwMakeContextCurrent(Handle);
            GlLoader.Initialize(glfwGetProcAddress);
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
            glfwDestroyWindow(Handle);
            glfwTerminate();
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
