using Emission.Graphics;
using Emission.IO;
using Emission.Page;
using System;

namespace Emission
{
    public class GameInstance : IDisposable
    {
        public static Game Game => _gameInstance;
        public static Window.Window Window => HasIntance() ? Game.Window : null;
        public static Renderer Renderer => HasIntance() ? Game.Renderer : null;
        public static Debug Debugger => HasIntance() ? Game.Debugger : null;
        public static Data Data => HasIntance() ? Game.Data : null;

        public static PageManager PageManager => HasIntance() ? Game.PageManager : null;

        public static Input Input => Input.Instance;

        public static Event.EventDispatcher EventDispatcher => Event.EventDispatcher.Instance;
        public static EngineBehaviour.EngineBehaviourDispatcher EngineBehaviourDispatcher => EngineBehaviour.EngineBehaviourDispatcher.Instance;

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

        internal GameInstance()
        {
            _gameInstance = new Game();
            _instance = this;
        }

        public void Dispose() { }

        public static bool HasIntance() => _instance != null;
    }
}
