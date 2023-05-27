using Emission.Core;
using Emission.Core.IO;

using Emission.Engine;
using Emission.Engine.Page;
using Emission.Engine.Window;
using Emission.Graphics;
using Emission.Graphics.RenderConfig;
using Emission.Natives.GLFW;

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

        internal EngineBehaviourDispatcher? BehaviourDispatcher;
        internal EventDispatcher? EventDispatcher;
        internal PageManager? PageManager;

        public bool IsRunning { get; private set; }
        public bool IsDebug { get; private set; }

        protected Game(ApplicationContext context, string? workingDirectory)
        {
            _context = context;
            IsRunning = false;
            IsDebug = context.IsDebug;
            
            if(!string.IsNullOrWhiteSpace(workingDirectory))
                EDirectory.SetCurrentDirectory(workingDirectory);
            
            ModelBuilder.InitializeContext();
        }

        public virtual void Initialize()
        {
            if (IsDebug)
            {
                _context.Debugger ??= new Debug("Emission Console");
                _context.Debugger.Write($"{DateTime.Now:F}\n{Environment.OSVersion.Platform} - {Environment.OSVersion.Version}\n{Application.Instance}\n\n");
            }
            else _context.DebugFlags = DebugFlags.ShowNothing;
            
            EngineBehaviour.CreateDispatcher();
            Event.CreateEventDispatcher();

            Event.AddDelegate(Event.INITIALIZE, Initialize);
            Event.AddDelegate(Event.START, Start);
            Event.AddDelegate(Event.UPDATE, Update);
            Event.AddDelegate(Event.RENDER, Render);
            Event.AddDelegate<int>(Event.STOP, Exit);
            
            Debug.Log("[INFO] Creating game event handlers");
            Debug.Log($"[INFO] Running with Emission Engine '{Context.Name}' ({Context.VersionMajor})");
            Debug.Log($"[INFO] Current directory : '{EDirectory.GetCurrentDirectory()}'");
            Debug.Log($"[INFO] Running with FPS limit {Context.Framerate}");
            
            Window ??= new Window.Window(WindowConfig.Default("Window"));
            Renderer ??= new Renderer(new GlConfig().GetDefault());
            PageManager ??= new PageManager();
            
            Graphics.Renderer.SetRendererInstance(Renderer);

            Window.Initialize();
            unsafe
            {
                _context.Window = Window.Handle;
            }
            
            Renderer.Initialize();

            EngineBehaviour.Call(Event.INITIALIZE);
        }
        
        public virtual void Start()
        {
            Window.Start();
            EngineBehaviour.Call(Event.START);

            if (!IsRunning) 
                Loop();
        }

        public void Loop()
        {
            IsRunning = true;

            float previous = (float)Glfw.glfwGetTime();
            float steps = 0.0f;
            
            while (!Window.ShouldClose)
            {
                float start = (float)Glfw.glfwGetTime();
                
                IApplication.SetDeltaTime(start - previous);
                
                previous = (float)Glfw.glfwGetTime();
                steps += Time.DeltaTime;
                
                while (steps >= 1.0f / Context.Framerate)
                {
                    Window.Update();
                    Event.Invoke(Event.UPDATE);
                    
                    steps -= 1.0f / Context.Framerate;
                }
                
                Window.Render();
                Event.Invoke(Event.RENDER);
                
                Input.Instance.Update();

                Window.Swap();
                
                steps += (float)Glfw.glfwGetTime() - start;
            }

            IsRunning = false;
            Event.Invoke(Event.STOP, 0);
        }

        public virtual void Update()
        {
            EngineBehaviour.Call(Event.UPDATE);
        }

        public virtual void Render()
        {
            EngineBehaviour.Call(Event.RENDER);
        }
        
        public virtual void Exit(int status)
        {
            // Call Stop Event
            EngineBehaviour.Call(Event.STOP);
            Window!.Stop();
            
            ModelBuilder.ReleaseContext();
            
            // Dispose Observators
            Event.DisposeEventDispatcher();
            EngineBehaviour.RemoveDispatcher();
            
            // Dispose Debugger
            Debug.Log($"[INFO] Application stopped with exit code {status}");
            _context.Debugger!.Dispose();
            
            // Exit Application
            Environment.Exit(status);
        }

        public void AttachInitializeFunction(Action action) => EventDispatcher!.Add(Event.INITIALIZE, () => action());
        public void AttachStartFunction(Action action) => EventDispatcher!.Add(Event.START, () => action());
        public void AttachRenderFunction(Action action) => EventDispatcher!.Add(Event.RENDER, () => action());
        public void AttachUpdateFunction(Action action) => EventDispatcher!.Add(Event.UPDATE, () => action());
        public void AttachExitFunction(Action<int> action) => EventDispatcher!.Add<int>(Event.STOP, (_) => action(_));

        public override string ToString() => Context.ToString();
    }
}
