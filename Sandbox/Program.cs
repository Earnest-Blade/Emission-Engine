using System;

using Emission;
using Emission.IO;
using Emission.Mathematics.Numerics;
using Emission.Shading;

using Sandbox.Scripts;

namespace Sandbox
{
    class Program
    {
        private Model _model;
        
        private PerspectiveCamera _perspectiveCamera;
        private FreeCameraController _controller;

        private Program()
        {
            // Register program methods to events
            Event.AddDelegate(Event.Update, Update);
            Event.AddDelegate(Event.Render, Render);
            
            Instances.Window.LoadIcon("icon2.png");
            
            _perspectiveCamera = new PerspectiveCamera(Instances.Window, 90, 0.1f, 100.0f);
            _controller = new FreeCameraController(_perspectiveCamera, 2.0f);

            var mat = new Material("material", "Assets/cube.shader");
            _model = new Model(mat, new[]
            {
                -1f,  1f, -5, 1.0f, 1.0f, 0, 0, 0,
                -1f, -1f, -5, 1.0f, 0.0f, 0, 0, 0,
                 1f, -1f, -5, 0.0f, 0.0f, 0, 0, 0,
                 1f,  1f, -5, 0.0f, 1.0f, 0, 0, 0
            }, new[] { 0, 1, 2, 2, 3, 0 });
            
            _model.Transform.Scale = new Vector3(1f);
        }

        private void Update()
        {
            _model.Update();
            _controller.Update();

            if(Input.IsKeyDown(Keys.Escape)) GameController.Stop(0);
        }

        private void Render()
        {
            _model.Draw();
        }
        
        static void Main(string[] args)
        {
            GameController.Create();
            GameController.CreateWindow("Window");
            GameController.Initiate();

            Program program = new Program();
            GameController.Start();
        }
    }
}
