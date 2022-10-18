
using Emission.IO;
using Emission.Window;

namespace Emission
{
    public class GameController
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
            GameInstance instance = !HasInstance ? new GameInstance() : GameInstance.Instance;
            return instance.Game;
        }
        
        public static void CreateWindow(WindowParameters parameters)
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                instance.Game.Window = new Window.Window(parameters);
            }
        }
        public static void CreateWindow(string title) => CreateWindow(WindowParameters.Default(title));
        public static void CreateWindow(string title, int width, int height) => CreateWindow(WindowParameters.Default(title, width, height));
        
        public static void CreateDebugger(string name)
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                instance.Game.Debugger = new Debug(name);
            }
        }

        public static void CreateRenderer()
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                instance.Game.Renderer = new Renderer();
            }
        }

        public static void CreateIo()
        {
            if (!HasInstance) return;
            using (var instance = GameInstance.Instance)
            {
                instance.Game.Data = new Data();
            }
        }

        public static bool IsGameRunning
        {
            get
            {
                // TODO:
                if (!HasInstance) return false;
                return false;
            }
        }
        
        public static bool HasInstance => GameInstance.Instance != null;
        public static Game Instance => HasInstance ? GameInstance.Instance.Game : Create();
    }
}