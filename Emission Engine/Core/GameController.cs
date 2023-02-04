using Emission.IO;
using Emission.Window;
using Emission.Graphics;
using Emission.Graphics.RenderConfig;
using JetBrains.Annotations;

namespace Emission
{
    public static class GameController
    {
        public static void Initiate()
        {
            if (!GameInstance.HasIntance()) return;
            Event.Invoke(Event.Initialize);            
        }
        
        public static void Start()
        {
            if (!GameInstance.HasIntance()) return;
            Event.Invoke(Event.Start);
        }

        public static void Stop(int status)
        {
            if (!GameInstance.HasIntance()) return;
            Event.Invoke(Event.Stop, status);
        }

        public static Game Create() => Create(EngineSettings.GetDefault());
        public static Game Create(EngineSettings settings)
        {
            Debug.Log("[INFO] A new game instance have been created!");
            GameInstance instance = !GameInstance.HasIntance() ? new GameInstance(settings) : GameInstance.Instance;
            return instance.Current;
        }

        public static void CreateWindow(string title) => CreateWindow(WindowConfig.Default(title));
        public static void CreateWindow(string title, int width, int height) => CreateWindow(WindowConfig.Default(title, width, height));
        public static void CreateWindow(WindowConfig config)
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game window instance have been created!");
                instance.Current.Window = new Window.Window(config);
            }
        }

        public static void CreateDebugger([CanBeNull] string name)
        {
            if (name == null) return;
            if (name.Length == 0) return;
            CreateDebugger(new Debug(name));
        }
        
        public static void CreateDebugger(Debug debugger)
        {
            if (debugger == null) return;
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application debugger instance have been created!");
                instance.Current.Debugger = debugger;
            }
        }

        public static void CreateRenderer(RenderConfig config)
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game renderer instance have been created!");
                instance.Current.Renderer = new Renderer(config);
            }
        }

        public static void CreateIo()
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application I/O instance have been created!");
                instance.Current.Data = new Data();
            }
        }

        public static bool IsGameRunning() => !GameInstance.HasIntance() && GameInstance.Game.IsRunning;
    }
}