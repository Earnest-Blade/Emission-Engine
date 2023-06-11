using Emission.Core;
using Emission.Core.IO;

using Emission.Engine;
using Emission.Engine.Page;
using Emission.Engine.Window;
using Emission.Graphics;
using Emission.Graphics.RenderConfig;
using Emission.Graphics.UI;
using Emission.Natives.GLFW;

using static nuklear.nuklear;

namespace Emission.Engine
{
    // TODO: Multi-threading when adding audio
    
    /// <summary>
    /// Represent an application template use to create game core.
    /// </summary>
    public abstract class Game : IApplication
    {
        /// <summary>
        /// Return application context information.
        /// Application Context is in readonly in this application type.
        /// </summary>
        public ApplicationContext Context
        {
            get => _context;
            set {}
        }

        private ApplicationContext _context;

        public Window.Window? Window;
        public Renderer? Renderer;

        internal EventDispatcher? EventDispatcher;
        internal PageManager? PageManager;
        internal UserInterfaceDispatcher? UserInterfaceDispatcher;

        public bool IsRunning { get; private set; }
        public bool IsDebug { get; private set; }

        protected Game(ApplicationContext context, string? workingDirectory)
        {
            _context = context;
            IsRunning = false;
            IsDebug = context.IsDebug;
            
            if(!string.IsNullOrWhiteSpace(workingDirectory))
                EDirectory.SetCurrentDirectory(workingDirectory);
            
            Model.InitializeContext();
        }

        public virtual void Initialize()
        {
            if (IsDebug)
            {
                _context.Debugger ??= new Debug("Emission Console");
                _context.Debugger.Write($"{DateTime.Now:F}\n{Environment.OSVersion.Platform} - {Environment.OSVersion.Version}\n{Application.Instance}\n\n");
            }
            else _context.DebugFlags = DebugFlags.ShowNothing;
            
            Event.CreateEventDispatcher();

            Event.AddDelegate(Event.INITIALIZE, Initialize);
            Event.AddDelegate(Event.START, Start);
            Event.AddDelegate<float>(Event.UPDATE, Update);
            Event.AddDelegate(Event.RENDER, Render);
            Event.AddDelegate<int>(Event.STOP, Exit);
            
            Debug.Log("[INFO] Creating game event handlers");
            Debug.Log($"[INFO] Running with Emission Engine '{Context.Name}' ({Context.VersionMajor})");
            Debug.Log($"[INFO] Current directory : '{EDirectory.GetCurrentDirectory()}'");
            Debug.Log($"[INFO] Current log file : '{Path.Combine(EDirectory.GetCurrentDirectory(), _context.Debugger?.Path!)}'");
            Debug.Log($"[INFO] Running with FPS limit {Context.Framerate}");
            
            Window ??= new Window.Window(WindowConfig.Default("Window"));
            Renderer ??= new Renderer(new OpenGlConfig().GetDefault());
            PageManager ??= new PageManager();
            UserInterfaceDispatcher ??= new UserInterfaceDispatcher();
            
            Renderer.SetRendererInstance(Renderer);

            Window.Initialize();
            unsafe
            {
                _context.Window = Window.Handle;
            }

            Renderer.Initialize();
        }
        
        public virtual void Start()
        {
            Window.Start();

            if (!IsRunning) 
                Loop();
        }

        public void Loop()
        {
            IsRunning = true;

            double previous = Glfw.glfwGetTime();
            double steps = 0.0f;
            
            while (!Window.ShouldClose)
            {
                double current = Glfw.glfwGetTime();
                double elapsed = current - previous;
                previous = current;

                steps += elapsed;
                
                Window.Update();
                IApplication.SetDeltaTime(elapsed);

                while (steps >= 1.0 / Context.Framerate)
                {
                    Event.Invoke(Event.UPDATE, (float)elapsed);
                    steps -= 1.0 / Context.Framerate;
                }
                
                Window.Render();
                Event.Invoke(Event.RENDER);
                
                Input.Instance.Update();
                
                Window.Swap();
            }

            IsRunning = false;
            Event.Invoke(Event.STOP, 0);
        }

        public virtual void Update(float delta) { }

        public virtual void Render()
        {
            NkNewFrame();
            
            UserInterfaceDispatcher.CallAll();
            
            NkRender(false);
        }
        
        public virtual void Exit(int status)
        {
            NkShutdown();

            Window!.Stop();
            
            Model.ReleaseContext();
            
            // Dispose Observators
            Event.DisposeEventDispatcher();
            
            // Dispose Debugger
            Debug.Log($"[INFO] Application stopped with exit code {status}");
            _context.Debugger!.Dispose();
            
            // Exit Application
            Environment.Exit(status);
        }

        public void AttachInitializeFunction(Action action) => EventDispatcher!.Add(Event.INITIALIZE, () => action());
        public void AttachStartFunction(Action action) => EventDispatcher!.Add(Event.START, () => action());
        public void AttachRenderFunction(Action action) => EventDispatcher!.Add(Event.RENDER, () => action());
        public void AttachUpdateFunction(Action<float> action) => EventDispatcher!.Add<float>(Event.UPDATE, (_) => action(_));
        public void AttachExitFunction(Action<int> action) => EventDispatcher!.Add<int>(Event.STOP, (_) => action(_));

        public override string ToString() => Context.ToString();
    }
}
