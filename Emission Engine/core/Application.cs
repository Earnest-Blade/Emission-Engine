using System;
using System.Diagnostics.CodeAnalysis;
using Emission.IO;
using Emission.Shading;

namespace Emission
{
    public class Application
    {
        private static Application _instance;
        
        public Window Window;
        public Renderer Renderer;
        public Debug Debugger;
        public Data Data;

        private Model _model;

        private Application()
        {
            EngineBehaviour.CreateDispatcher();
            Event.CreateEventDispatcher();

            Event.RegisterEvent<EventArgs>(Event.Initialize, new ActionEvent(Initialize));
            Event.RegisterEvent<EventArgs>(Event.Start, new ActionEvent(Start));
            Event.RegisterEvent<EventArgs>(Event.Update, new ActionEvent(Update));
            Event.RegisterEvent<EventArgs>(Event.Render, new ActionEvent(Draw));
            Event.RegisterEvent<EventArgs>(Event.Stop, new FloatEvent(0));
        }

        private void Initialize()
        {
            Window ??= CreateWindow("Window");
            Renderer ??= CreateRenderer();
            Debugger ??= CreateDebugger("Emission Console");

            Data = new Data();
            
            Window.Initialize();
            EngineBehaviour.Call(EngineBehaviour.Initialize);

            var mat = new Material("material", "Assets/demo.shader");
            _model = new Model(mat, new[]
            {
                -1, 1, 0, 1.0f, 1.0f, 0, 0, 0,
                -1, -1, 0, 1.0f, 0.0f, 0, 0, 0,
                1, -1, 0, 0.0f, 0.0f, 0, 0, 0,
                1, 1, 0, 0.0f, 1.0f, 0, 0, 0
            }, new[] { 0, 1, 2, 2, 3, 0 });
        }
        
        private void Start()
        {
            Window.Start();
            EngineBehaviour.Call(EngineBehaviour.Start);

            Loop();
        }
        
        private void Loop()
        {
            while (!Window.ShouldClose)
            {
                Window.Update();
                Event.HandleEvent(Event.Update);
                EngineBehaviour.Call(EngineBehaviour.Update);
                
                Window.Render();
                Event.HandleEvent(Event.Render);
                EngineBehaviour.Call(EngineBehaviour.Render);
                
                Window.Flush();
                
                Event.ResetEventDispatcher();
            }
            
            Stop(0);
        }

        private void Update()
        {
            _model.Update();
        }

        private void Draw()
        {
            _model.Draw();
        }
        
        private void Stop(int status)
        {
            // Call Stop event
            Event.HandleEvent<EventArgs>(Event.Stop, new FloatEvent(status));
            EngineBehaviour.Call(EngineBehaviour.Stop);
            Window.Stop();
            
            // Clear events
            Event.RemoveEvent(Event.Initialize);
            Event.RemoveEvent(Event.Start);
            Event.RemoveEvent(Event.Update);
            Event.RemoveEvent(Event.Render);
            Event.RemoveEvent(Event.Stop);
            
            // Dispose Observators
            Event.DisposeEventDispatcher();
            EngineBehaviour.RemoveDispatcher();
            
            // Exit Application
            Environment.Exit(status);
        }

        /* Fabricator Methods */
        public static Window CreateWindow([NotNull]WindowParams @params)
        {
            _instance.Window = new Window(@params);
            return _instance.Window;
        }
        public static Window CreateWindow([NotNull]string name) => CreateWindow(WindowParams.Default(name));
        public static Window CreateWindow([NotNull] string name, int width, int height) =>
            CreateWindow(WindowParams.Default(name, width, height));

        public static Renderer CreateRenderer()
        {
            _instance.Renderer = new Renderer();
            return _instance.Renderer;
        }

        public static Debug CreateDebugger()
        {
            _instance.Debugger = new Debug();
            return _instance.Debugger;
        }
        
        public static Debug CreateDebugger(string title)
        {
            _instance.Debugger = new Debug(title);
            return _instance.Debugger;
        }
        
        public static Debug CreateDebugger(string title, int width, int height)
        {
            _instance.Debugger = new Debug(title, width, height);
            return _instance.Debugger;
        }

        /* Static methods */
        public static Application CreateApplication()
        {
            if (!HasCurrentInstance())
            {
                _instance = new Application();
            }
            
            return _instance;
        }

        public static bool HasCurrentInstance()
        {
            return _instance != null;
        }
        
        public static Application GetCurrentApplication()
        {
            return HasCurrentInstance() ? _instance : CreateApplication();
        }

        public static void StartApplication()
        {
            Event.HandleEvent(Event.Initialize);
            Event.HandleEvent(Event.Start);
        }

        public static void InitializeApplication()
        {
            Event.HandleEvent(Event.Initialize);
        }
        
        public static void StopApplication(int status)
        {
            _instance.Stop(status);
        }
    }
}
