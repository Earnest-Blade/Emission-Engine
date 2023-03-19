using System.Diagnostics.CodeAnalysis;

using Emission.IO;
using Emission.Window;
using Emission.Graphics;
using Emission.Graphics.RenderConfig;
using Emission.Natives;
using JetBrains.Annotations;

namespace Emission
{
    public class GameController
    {
        public static void Initiate()
        {
            if (!GameInstance.HasInstance()) return;
            Event.Invoke(Event.INITIALIZE);            
        }
        
        public static void Start()
        {
            if (!GameInstance.HasInstance()) return;
            Event.Invoke(Event.START);
        }

        [DoesNotReturn]
        public static void Stop(int status)
        {
            if (!GameInstance.HasInstance()) return;
            Event.Invoke(Event.STOP, status);
        }

        public static Game Create() => Create(EngineSettings.GetDefault());
        public static Game Create(string directory) => Create(0, directory);
        public static Game Create(short version, string directory) => Create(version, directory, 60);
        public static Game Create(short version, string directory, int framerate) => Create(version, framerate, false, directory);
        public static Game Create(short version, int framerate, bool debug, string directory) => Create(new EngineSettings(version, framerate, debug, directory));
        public static Game Create(EngineSettings settings)
        {
            Debug.Log("[INFO] A new game instance have been created!");
            GameInstance instance = !GameInstance.HasInstance() ? new GameInstance(settings) : GameInstance.Instance;
            return instance.Current;
        }

        public static void CreateWindow(string title) => CreateWindow(WindowConfig.Default(title));
        public static void CreateWindow(string title, int width, int height) => CreateWindow(WindowConfig.Default(title, width, height));
        public static void CreateWindow(WindowConfig config)
        {
            if (!GameInstance.HasInstance()) return;
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
            if (!GameInstance.HasInstance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application debugger instance have been created!");
                instance.Current.Debugger = debugger;
            }
        }

        public static void CreateRenderer(RenderConfig config)
        {
            if (!GameInstance.HasInstance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new game renderer instance have been created!");
                instance.Current.Renderer = new Renderer(config);
            }
        }

        public static void CreateIo()
        {
            if (!GameInstance.HasInstance()) return;
            using (var instance = GameInstance.Instance)
            {
                Debug.Log("[INFO] A new application I/O instance have been created!");
                instance.Current.Data = new Data();
            }
        }

        public static bool IsRunning() => !GameInstance.HasInstance() && GameInstance.Game.IsRunning;
    }
}