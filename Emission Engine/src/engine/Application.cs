using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Emission
{
    // TODO: Setup a singleton
    class Application
    {
        private static readonly Application current = new Application();

        public Window Window { get; }
        public Camera Camera { get; }
        public ApplicationConsole Console { get; }

        private Application()
        {
            Window = new Window(WindowSettings.GetDefault());
            Camera = new Camera();
            Console = new ApplicationConsole();
        }

        public void Start()
        {
            Window.Show();

            Mesh mesh = new Mesh(
                new float[]
                {
                    0f,   100f, -100f, 0.0f, 0.0f,
                    0f,   0f,   -100f, 0.0f, 1.0f,
                    100f, 0f,   -100f, 1.0f, 1.0f,
                    100f, 100f, -100f, 1.0f, 0.0f
                },
                new int[]
                {
                    1, 2, 0, 0, 3, 2
                }
            );

            //mesh.Transform.Scale = 5f;

            while (!Window.ShouldClose())
            {
                Input.Current.Update();

                Window.Update();
                Camera.Update();

                mesh.Update();

                if (Input.IsKeyDown(Keys.Escape)) Stop(1);

                Window.PreRender();

                mesh.Render();

                Window.PostRender();
            }

            Stop(0);
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
            Application.Current.Start();
        }
    }
}
