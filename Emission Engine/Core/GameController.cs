using Emission.IO;
using Emission.Window;
using Emission.Graphics;

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

        public static Game Create()
        {
            Debug.Log("[INFO] A new game instance have been created!");
            GameInstance instance = !GameInstance.HasIntance() ? new GameInstance() : GameInstance.Instance;
            return instance.Current;
        }

        public static void CreateWindow(string title) => CreateWindow(WindowParameters.Default(title));
        public static void CreateWindow(string title, int width, int height) => CreateWindow(WindowParameters.Default(title, width, height));
        public static void CreateWindow(WindowParameters parameters)
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game window instance have been created!");
                instance.Current.Window = new Window.Window(parameters);
            }
        }
        
        public static void CreateDebugger(string name)
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application debugger instance have been created!");
                instance.Current.Debugger = new Debug(name);
            }
        }

        public static void CreateRenderer()
        {
            if (!GameInstance.HasIntance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game renderer instance have been created!");
                instance.Current.Renderer = new Renderer();
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