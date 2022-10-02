using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Emission
{
    public unsafe class Window : IDisposable, IEngineBehaviour
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
        public WindowParams Params { get; }

        /// <summary>
        /// Return, as float value, the relationship between the width and the height of the screen.
        /// Calculate with <see cref="_lastWindowPosition"/>.
        /// Cannot be set, readonly.
        /// </summary>
        public float WindowAspect
        {
            get => (float)_lastWinSize.X / (float)_lastWinSize.Y;
        }

        /// <summary>
        /// Use a Vector2 with int values to get window's position in computer screen space.
        /// Can be set or get.
        /// Call <see cref="GLFW.SetWindowPos"/> to define position.
        /// </summary>
        public Vector2i WindowPosition
        {
            get => _lastWinPos;
            set => GLFW.SetWindowPos(WindowPointer, (int)value.X, (int)value.Y);
        }

        /// <summary>
        /// Use a Vector2 with int values to get Window current size in pixels.
        /// Can be set or get.
        /// Call <see cref="GLFW.SetWindowSize"/> to define size.
        /// </summary>
        public Vector2 WindowSize
        {
            get => _lastWinSize;
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

        public string Icon
        {
            set
            {
                
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

        public IEngineBehaviour Behaviour => this;

        private string _title;
        private Vector2i _lastWinSize;
        private Vector2i _lastWinPos;

        public Window(WindowParams @params)
        {
            Params = @params;
            _title = @params.Title;

            if (!GLFW.Init())
            {
                GLFW.GetError(out string error);
                throw new EmissionException(EmissionException.EmissionGlfwException, error);
            }
            
            GLFW.WindowHint(WindowHintInt.ContextVersionMinor, Params.MinorVersion);
            GLFW.WindowHint(WindowHintInt.ContextVersionMajor, Params.MajorVersion);
            GLFW.WindowHint(WindowHintBool.Focused, Params.IsFocused);
            GLFW.WindowHint(WindowHintBool.Floating, Params.IsFloating);
            GLFW.WindowHint(WindowHintBool.Maximized, Params.IsMaximized);
            GLFW.WindowHint(WindowHintBool.Decorated, Params.IsDecorated);
            GLFW.WindowHint(WindowHintBool.Resizable, Params.IsResizable);
            GLFW.WindowHint(WindowHintBool.Visible, Params.IsVisible);
            GLFW.WindowHint(WindowHintBool.CenterCursor, Params.IsCursorCentered);
            GLFW.WindowHint(WindowHintBool.FocusOnShow, Params.IsFocusedOnShow);

            WindowPointer = GLFW.CreateWindow(Params.Width, Params.Height, Params.Title, null, null);
            Context = new GLFWGraphicsContext(WindowPointer);
            Context.MakeCurrent();
            
            if (EmissionAssembly.Load() != 0)
            {
                throw new EmissionException(EmissionException.EmissionAssemblyException, "While Loading OpenTk's Assembly");
            }
        }

        public void Initialize()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            
            GL.DepthFunc(DepthFunction.Less);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            GL.Viewport(0, 0, Params.Width, Params.Height);
        }

        public void Start()
        {
            Visible = true;
        }

        public void Update()
        {
            GLFW.PollEvents();
        }

        public void Render()
        {
            GL.ClearColor(Params.ClearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Flush()
        {
            if (Params.VSync)
            {
                GLFW.SwapBuffers(WindowPointer);
            }
        }

        public void Stop()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            GLFW.DestroyWindow(WindowPointer);
            GLFW.Terminate();
        }
    }

    public struct WindowParams
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

        public Color ClearColor;

        public static WindowParams Default(string name) => Default(name, 960 * 2, 540 * 2);
        public static WindowParams Default(string name, int width, int height)
        {
            return new WindowParams()
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
                
                ClearColor = Color.Black
            };
        }
    }
}
