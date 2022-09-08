using System;

using Emission.Math;
using Emission.GFX;

namespace Emission
{
    public class Application
    {
        // Singleton
        private static Application _current = new();

        public Debug Debugger { get; private set; }
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; }
        
        /// <summary>
        /// Return current Application singleton.
        /// Use to get current window.
        /// </summary>
        public static Application Singleton
        {
            get => _current;
        }
        
        // Events
        public event EventHandler InitializeCallback;
        public event EventHandler StartCallback;
        public event EventHandler UpdateCallback;
        public event EventHandler PreRenderCallback;
        public event EventHandler RenderCallback;
        public event EventHandler PostRenderCallback;
        public event EventHandler StopEvent;

        private Action _initializeAction;

        // private constructor
        private Application() {}

        /// <summary>
        /// Initialize application context and start application.
        /// Create window and console objects and call initialize callback.
        /// </summary>
        /// <param name="obj">Construct object</param>
        public void Initialize()
        {
            Debugger ??= new Debug();
            Window ??= new Window(WindowSettings.GetDefault());
            Renderer ??= new Renderer();

            _initializeAction?.Invoke();

            UpdateCallback += (o, e) => Input.Current.Update();
            InitializeCallback?.Invoke(this, EventArgs.Empty);
            
            Start();
        }

        /// <summary>
        /// Start application method.
        /// Call the Start event and Start loop.
        /// Don't need to be call, call after application is initialize.
        /// </summary>
        public void Start()
        {
            StartCallback?.Invoke(this, EventArgs.Empty);

            Loop();
        }

        /// <summary>
        /// Main program method. Contains main loop.
        /// check while the window must be visible and stop program when it shouldn't.
        /// Define current framerate and current delta time.
        /// Call Update and Render events.
        /// Never re-call this method, only keep one loop alive.
        /// </summary>
        private void Loop()
        {
            double totalElapsedTime = 0, previousTime = Time.CurrentTime;
            int frameCount = 0;

            while (!Window.ShouldClose)
            {
                Time.SetDeltaTime(Time.CurrentTime - totalElapsedTime);
                totalElapsedTime = Time.CurrentTime;

                frameCount++;
                if (Time.CurrentTime - previousTime >= 1.0)
                {
                    Time.SetFps(frameCount);
                    frameCount = 0;
                    previousTime = Time.CurrentTime;
                }

                UpdateCallback?.Invoke(this, EventArgs.Empty);
                
                PreRenderCallback?.Invoke(this, EventArgs.Empty);
                RenderCallback?.Invoke(this, EventArgs.Empty);
                PostRenderCallback?.Invoke(this, EventArgs.Empty);
            }
            
            Stop(0);
        }

        /// <summary>
        /// Stop application.
        /// Unload and clear all components and call stop event.
        /// Print and save logs.
        /// Use status to define program's state when it end (-1 : Catch an error,
        /// 0 : On application's loop end)
        /// </summary>
        /// <param name="status">Current application status when end</param>
        public void Stop(int status)
        {
            StopEvent?.Invoke(this, EventArgs.Empty);
            
            Window.Behaviour.Dispose();
            Renderer.Clear();

            Debug.Log("[INFO] Quitting with status " + status);
            Debug.Singleton.Save();
            
            Environment.Exit(status);
        }

        /// <summary>
        /// Initialize a new Debugger object.
        /// </summary>
        /// <returns></returns>
        public Application CreateDebugger()
        {
            Debugger = new Debug();
            return this;
        }
        
        /// <summary>
        /// Create a new window using WindowSettings.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Application CreateWindow(WindowSettings settings)
        {
            Window = new Window(settings);
            return this;
        }

        /// <summary>
        /// Create a new renderer to the application.
        /// </summary>
        /// <returns></returns>
        public Application CreateRenderer()
        {
            Renderer = new Renderer();
            return this;
        }

        /// <summary>
        /// Register the function that will be called when the application start.
        /// </summary>
        /// <param name="action">Function to call.</param>
        public void RegisterInitMethod(Action action)
        {
            _initializeAction = action;
        }

        /// <summary>
        /// Create a new Application.
        /// </summary>
        /// <returns></returns>
        public static Application GenerateApplication()
        {
            Application application = new Application();
            _current = application;
            return application;
        }

        /// <summary>
        /// Stop application.
        /// Unload and clear all components and call stop event.
        /// Print and save logs.
        /// Use status to define program's state when it end (-1 : Catch an error,
        /// 0 : On application's loop end)
        /// </summary>
        /// <param name="status">Current application status when end</param>
        public static void Quit(int status = 0)
        {
            Singleton.Stop(status);
        }
    }
}
