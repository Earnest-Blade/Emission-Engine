using System;
using Emission.Page;
using Emission.IO;
using Emission.Window;
using Emission.Graphics;
using System.Threading;
using Emission.Graphics.RenderConfig;

namespace Emission
{
    // TODO: Multi-threading
    public class Game
    {
        public readonly EngineSettings Settings;

        public Window.Window? Window { get; internal set; }
        public Renderer? Renderer { get; internal set; }
        public Debug? Debugger { get; internal set; }
        public Data? Data { get; internal set; }

        public PageManager? PageManager { get; internal set; }

        public bool IsRunning { get; private set; }

        internal Game(EngineSettings settings)
        {
            Settings = settings;
            IsRunning = false;
            
            if(Settings.Directory != EngineSettings.DEFAULT_DIRECTORY)
                GameDirectory.SetCurrentDirectory(Settings.Directory);

            EngineBehaviour.CreateDispatcher();
            Event.CreateEventDispatcher();
            
            Event.AddDelegate(Event.INITIALIZE, Initialize);
            Event.AddDelegate(Event.START, Start);
            Event.AddDelegate(Event.UPDATE, Update);
            Event.AddDelegate(Event.RENDER, Render);
            Event.AddDelegate<int>(Event.STOP, Exit);
            
            Debug.Log("[INFO] Creating game event handlers");
            Debug.Log($"[INFO] Running with Emission Engine '{Settings.VersionAsStr}' ({Settings.Version})");
            Debug.Log($"[INFO] Current directory : '{GameDirectory.GetCurrentDirectory()}'");
            Debug.Log($"[INFO] Running with FPS limit {Settings.Framerate}");
        }

        private void Initialize()
        {
            Debugger ??= new Debug("Emission Console");
            Debugger.Write($"{DateTime.Now:F}\n{Environment.OSVersion.Platform} - {Environment.OSVersion.Version}\n{GameInstance.EngineSettings.ToString()}\n\n");
            
            Data ??= new Data();
            Window ??= new Window.Window(WindowConfig.Default("Window"));
            Renderer ??= new Renderer(new GlConfig().GetDefault());
            PageManager ??= new PageManager();
            
            ModelBuilder.InitializeContext();
            
            Window.Initialize();
            Renderer.Initialize();
            EngineBehaviour.Call(Event.INITIALIZE);
        }
        
        private void Start()
        {
            Window.Start();
            EngineBehaviour.Call(Event.START);

            if (!IsRunning) 
                Loop();
        }

        private void Loop()
        {
            IsRunning = true;
            double totalElapsedTime = 0, previousTime = Time.GlfwTime();
            int frameCount = 0;

            while (!Window.ShouldClose)
            {
                IsRunning = true;
                Time.SetFrameTime(Time.GlfwTime());
                Time.SetDeltaTime(Time.GlfwTime() - totalElapsedTime);
                totalElapsedTime = Time.GlfwTime();

                frameCount++;
                if (Time.GlfwTime() - previousTime >= 1.0 / Settings.Framerate)
                {
                    Time.SetFps(frameCount);
                    frameCount = 0;
                    previousTime = Time.GlfwTime();
                }

                GameInstance.Input.Update();
                
                Window.Update();
                Event.Invoke(Event.UPDATE);
                
                Window.Render();
                Event.Invoke(Event.RENDER);

                Window.Swap();
            }

            // Close program.
            IsRunning = false;
            Event.Invoke(Event.STOP, 0);
        }

        private void Update()
        {
            EngineBehaviour.Call(Event.UPDATE);
        }

        private void Render()
        {
            EngineBehaviour.Call(Event.RENDER);
        }
        
        private void Exit(int status)
        {
            // Call Stop Event
            EngineBehaviour.Call(Event.STOP);
            Window.Stop();

            ModelBuilder.ReleaseContext();
            
            // Dispose Observators
            Event.DisposeEventDispatcher();
            EngineBehaviour.RemoveDispatcher();
            
            // Dispose Debugger
            Debug.Log($"[INFO] Application stopped with exit code {status}");
            Debugger.Dispose();
            
            // Exit Application
            Environment.Exit(status);
        }
    }
}
