using System;

namespace Emission
{
    internal class GameInstance : IDisposable
    {
        public static GameInstance Instance => _instance;
        public Game Game => _gameInstance;
            
        private static GameInstance _instance;
        private Game _gameInstance;

        public GameInstance()
        {
            _gameInstance = new Game();
            _instance = this;
        }

        public void Dispose() { }
    }
}
