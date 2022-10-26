using System;

namespace Emission
{
    internal class GameInstance : IDisposable
    {
        public static GameInstance Instance
        {
            get
            {
                lock (_padlock) return _instance;
            }
        }

        public Game Game => _gameInstance;

        private static GameInstance _instance;
        private static Game _gameInstance;
        private static readonly object _padlock = new object();

        public GameInstance()
        {
            _gameInstance = new Game();
            _instance = this;
        }

        public void Dispose() { }
    }
}
