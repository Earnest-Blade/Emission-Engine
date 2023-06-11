namespace Emission.Core
{
    public interface IApplication
    {
        public ApplicationContext Context { get; set; }

        public bool IsRunning { get; }
        public bool IsDebug { get; }

        public void Initialize();
        public void Start();
        public void Exit(int status);
        
        public void Loop();

        /// <summary>
        /// Define current delta time value.
        /// </summary>
        /// <param name="value">New delta time value</param>
        protected static void SetDeltaTime(double value) => Time.DeltaTime = value;
        
        /// <summary>
        /// Define current frame per second value.
        /// </summary>
        /// <param name="value">New Fps value</param>
        protected static void SetFps(uint value) => Time.Fps = value;
    }
}
