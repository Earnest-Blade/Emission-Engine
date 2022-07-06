using System;
using Emission.Math;

namespace Emission
{
    public class Application
    {
        // Singleton
        private static readonly Application _current = new Application();

        public Window Window { get; private set; }
        
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

        // private constructor
        private Application() {}

        /// <summary>
        /// Initialize application context and start application.
        /// Use an <see cref="IConstructable"/> object to call a method as constructor.
        /// Create window and console objects and call initialize callback.
        /// </summary>
        /// <param name="obj">Construct object</param>
        public void Initialize(IConstructable obj)
        {
            Window = new Window(WindowSettings.GetDefault());
            //Console = new Debug();
            
            obj.Construct();
            
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
        /// Print and save logs using <see cref="Debug.Singleton.WriteLogs"/>.
        /// Use status to define program's state when it end (-1 : Catch an error,
        /// 0 : On application's loop end)
        /// </summary>
        /// <param name="status">Current application status when end</param>
        public void Stop(int status)
        {
            StopEvent?.Invoke(this, EventArgs.Empty);
            
            Window.Behaviour.Dispose();
            GraphicAllocator.ClearAll();

            Debug.Log("[INFO] Quitting with status " + status);
            Debug.Singleton.WriteLogs();
            
            Environment.Exit(status);
        }

    }
}
