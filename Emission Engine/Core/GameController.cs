using Emission.IO;
using Emission.Window;
using Emission.Graphics;

namespace Emission
{
    public static class GameController
    {
        public static void Initiate()
        {
            if (!HasInstance) return;
            Event.Invoke(Event.Initialize);            
        }
        
        public static void Start()
        {
            if (!HasInstance) return;
            Event.Invoke(Event.Start);
        }

        public static void Stop(int status)
        {
            if (!HasInstance) return;
            Event.Invoke(Event.Stop, status);
        }

        public static Game Create()
        {
            Debug.Log("[INFO] A new game instance have been created!");
            GameInstance instance = !HasInstance ? new GameInstance() : GameInstance.Instance;
            return instance.Game;
        }

        public static void CreateWindow(string title) => CreateWindow(WindowParameters.Default(title));
        public static void CreateWindow(string title, int width, int height) => CreateWindow(WindowParameters.Default(title, width, height));
        public static void CreateWindow(WindowParameters parameters)
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game window instance have been created!");
                instance.Game.Window = new Window.Window(parameters);
            }
        }
        
        public static void CreateDebugger(string name)
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application debugger instance have been created!");
                instance.Game.Debugger = new Debug(name);
            }
        }

        public static void CreateRenderer()
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game renderer instance have been created!");
                instance.Game.Renderer = new Renderer();
            }
        }

        public static void CreateIo()
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application I/O instance have been created!");
                instance.Game.Data = new Data();
            }
        }

        public static bool IsGameRunning => !HasInstance && Instances.Game.IsRunning;

        public static bool HasInstance => GameInstance.Instance != null;
        public static Game Instance => HasInstance ? GameInstance.Instance.Game : Create();
    }
}