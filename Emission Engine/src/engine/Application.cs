using System;
using Emission.Math;

namespace Emission
{
    // TODO Create a scene class and use this class as a scene
    class Application
    {
        private static readonly Application current = new Application();

        public Window Window { get; }
        public Camera Camera { get; }
        public ApplicationConsole Console { get; }

        private Mesh _mesh;

        private Application()
        {
            Window = new Window(WindowSettings.GetDefault());
            Camera = new Camera();
            Console = new ApplicationConsole();
        }

        public void Start()
        {
            Camera.SetAsMain(Camera);
            
            Window.Show();

            _mesh = new Mesh(
                new float[]
                {
                    0.5f, 0.5f,  0f, 1.0f, 1.0f,
                    0.5f, -0.5f, 0f, 1.0f, 0.0f,
                    -0.5f, -0.5f,0f, 0.0f, 0.0f,
                    -0.5f, 0.5f, 0f, 0.0f, 1.0f
                },
                new int[]
                {
                    1, 2, 0, 0, 3, 2
                }
            );

            Loop();
        }

        private void Loop()
        {
            double totalElapsedTime = 0, previousTime = Time.CurrentTime;
            int frameCount = 0;
            
            while (!Window.ShouldClose())
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
            
            Camera.Update();
            
            _mesh.Update();
            
            if (Input.IsKeyPressed(Keys.D)) ApplicationConsole.Print("[INFO] Current framerate: " + Time.Fps);
            if (Input.IsKeyPressed(Keys.C)) ApplicationConsole.Print("[INFO] Current Camera Position" + Camera.Transform.Position);
            if (Input.IsKeyDown(Keys.Escape)) Stop(1);
        }

        private void Render()
        {
            Window.PreRender();

            _mesh.Render();

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
