using System;

using Emission.IO;
using Emission.Window;

namespace Emission
{
    public class Game
    {
        public Window.Window Window;
        public Renderer Renderer;
        public Debug Debugger;
        public Data Data;

        private bool _isRunning;
        private float _startDuration;
        
        internal Game()
        {
            _isRunning = false;
            _startDuration = (float)Time.GlfwTime();
            
            EngineBehaviour.CreateDispatcher();
            Event.CreateEventDispatcher();
            
            Input.CreateInput();

            Event.AddDelegate(Event.Initialize, Initialize);
            Event.AddDelegate(Event.Start, Start);
            Event.AddDelegate(Event.Update, Update);
            Event.AddDelegate(Event.Render, Render);
            Event.AddDelegate<int>(Event.Stop, Exit);
        }

        private void Initialize()
        {
            Window ??= new Window.Window(WindowParameters.Default("Window"));
            Renderer ??= new Renderer();
            Debugger ??= new Debug("Emission Console");
            Data ??= new Data();
            
            Window.Initialize();
            EngineBehaviour.Call(Event.Initialize);
        }
        
        private void Start()
        {
            Window.Start();
            EngineBehaviour.Call(Event.Start);
            
            Debug.Log($"[INFO] Starting duration: {Math.Round(Time.GlfwTime() - _startDuration, 2)}ms!");
            if (!_isRunning) Loop();
        }
        
        private void Loop()
        {
            _isRunning = true;
            double totalElapsedTime = 0, previousTime = Time.GlfwTime();
            int frameCount = 0;

            while (!Window.ShouldClose)
            {
                Time.SetDeltaTime(Time.GlfwTime() - totalElapsedTime);
                totalElapsedTime = Time.GlfwTime();

                frameCount++;
                if (Time.GlfwTime() - previousTime >= 1.0)
                {
                    Time.SetFps(frameCount);
                    frameCount = 0;
                    previousTime = Time.GlfwTime();
                }

                Window.Update();
                Event.Invoke(Event.Update);
                
                Window.Render();
                Event.Invoke(Event.Render);

                Window.Flush();
            }
            
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
            
            // Exit Application
            Environment.Exit(status);
        }
    }
}
