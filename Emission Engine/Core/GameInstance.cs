using Emission.Graphics;
using Emission.IO;
using Emission.Page;
using System;

namespace Emission
{
    public class GameInstance : IDisposable
    {
        public static Game Game => _gameInstance;
        public static Window.Window Window => HasInstance() ? Game.Window : null;
        public static Renderer Renderer => HasInstance() ? Game.Renderer : null;
        public static Debug Debugger => HasInstance() ? Game.Debugger : null; 
        public static Data Data => HasInstance() ? Game.Data : null;
        public static EngineSettings EngineSettings => HasInstance() ? Game.Settings : EngineSettings.GetDefault();

        public static PageManager PageManager => HasInstance() ? Game.PageManager : null;

        public static Input Input => Input.Instance;

        internal static EventDispatcher EventDispatcher => EventDispatcher.Instance;
        internal static EngineBehaviourDispatcher EngineBehaviourDispatcher => EngineBehaviourDispatcher.Instance;

        public static GameInstance Instance
        {
            get
            {
                lock (_padlock) return _instance;
            }
        }

        public Game Current => _gameInstance;

        private static GameInstance _instance;
        private static Game _gameInstance;
        private static readonly object _padlock = new object();

        internal GameInstance(EngineSettings settings)
        {
            _gameInstance = new Game(settings);
            _instance = this;
        }

        public void Dispose() { }

        public static bool HasInstance() => _instance != null;
    }
}
