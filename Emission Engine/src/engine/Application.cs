using System;
using Emission.Math;
using Game;
using OpenTK.Mathematics;

namespace Emission
{
    // TODO Create a scene class and use this class as a scene
    class Application
    {
        private static readonly Application current = new Application();

        public Window Window { get; }
        public ApplicationConsole Console { get; }

        private Scene _debugScene;

        private Application()
        {
            Window = new Window(WindowSettings.GetDefault());
            Console = new ApplicationConsole();
            
        }

        public void Start()
        {
            _debugScene = new DebugScene();
            _debugScene.Call();

            Window.Visible = true;

            Loop();
        }

        private void Loop()
        {
            double totalElapsedTime = 0, previousTime = Time.CurrentTime;
            int frameCount = 0;
            
            while (!Window.ShouldClose)
            {
                Time.SetDeltaTime(Time.CurrentTime - totalElapsedTime);
                totalElapsedTime = Time.CurrentTime;

                frameCount++;
                if (Time.CurrentTime - previousTime >= 1.0)
                {
                    Time.SetFps(frameCount);
                    frameCount = 0;
                    previousTime = Time.CurrentTime;
                }
                
                Update();
                Render();
            }
            
            Stop(0);
        }

        private void Update()
        {
            Input.Current.Update();
            Window.Update();
            
            _debugScene.Update();
            
            // Debug Input
            if (Input.IsKeyPressed(Keys.D)) ApplicationConsole.Print("[INFO] Current framerate: " + Time.Fps);
            if (Input.IsKeyDown(Keys.Escape)) Stop(1);
        }

        private void Render()
        {
            Window.PreRender();

            _debugScene.Render();

            Window.PostRender();
        }

        public void Stop(int status)
        {
            Window.Destroy();

            GraphicAllocator.Clear();

            ApplicationConsole.Print("[INFO] Quitting with status " + status);

            ApplicationConsole.Write();
            Environment.Exit(status);
        }

        public static Application Current
        {
            get => current;
        }

        static void Main(string[] args)
        {
            Current.Start();
        }
    }
}
