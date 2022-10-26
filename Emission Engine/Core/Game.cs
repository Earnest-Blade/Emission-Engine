using System;
using Emission.IO;
using Emission.Window;
using Emission.Graphics;

namespace Emission
{
    public class Game
    {
        public Window.Window Window;
        public Renderer Renderer;
        public Debug Debugger;
        public Data Data;

        public bool IsRunning { get; private set; }

        internal Game()
        {
            IsRunning = false;
            
            EngineBehaviour.CreateDispatcher();
            Event.CreateEventDispatcher();

            Event.AddDelegate(Event.Initialize, Initialize);
            Event.AddDelegate(Event.Start, Start);
            Event.AddDelegate(Event.Update, Update);
            Event.AddDelegate(Event.Render, Render);
            Event.AddDelegate<int>(Event.Stop, Exit);
        }

        private void Initialize()
        {
            Debugger ??= new Debug("Emission Console");
            Data ??= new Data();
            Window ??= new Window.Window(WindowParameters.Default("Window"));
            Renderer ??= new Renderer();
            
            Window.Initialize();
            EngineBehaviour.Call(Event.Initialize);
        }
        
        private void Start()
        {
            Window.Start();
            EngineBehaviour.Call(Event.Start);
            

            if (!IsRunning) Loop();
        }

        private void Loop()
        {
            IsRunning = true;
            double totalElapsedTime = 0, previousTime = Time.GlfwTime();
            int frameCount = 0;

            while (!Window.ShouldClose)
            {
                IsRunning = true;
                Time.SetDeltaTime(Time.GlfwTime() - totalElapsedTime);
                totalElapsedTime = Time.GlfwTime();

                frameCount++;
                if (Time.GlfwTime() - previousTime >= 1.0)
                {
                    Time.SetFps(frameCount);
                    frameCount = 0;
                    previousTime = Time.GlfwTime();
                }

                Instances.Input.Update();
                
                Window.Update();
                Event.Invoke(Event.Update);
                
                Window.Render();
                Event.Invoke(Event.Render);

                Window.Swap();
            }

            IsRunning = false;
            Event.Invoke(Event.Stop, 0);
        }

        private void Update()
        {
            EngineBehaviour.Call(Event.Update);
        }

        private void Render()
        {
            EngineBehaviour.Call(Event.Render);
        }
        
        private void Exit(int status)
        {
            // Call Stop Event
            EngineBehaviour.Call(Event.Stop);
            Window.Stop();

            // Dispose Observators
            Event.DisposeEventDispatcher();
            EngineBehaviour.RemoveDispatcher();
            
            Debug.Log($"[INFO] Application stopped with exit code: {status}!");
            Debugger.Dispose();
            
            // Exit Application
            Environment.Exit(status);
        }
    }
}
