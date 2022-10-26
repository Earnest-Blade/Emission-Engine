using Emission;
using Emission.IO;
using Emission.Window;
using Emission.Graphics;
using Emission.Mathematics;
using Emission.Graphics.Shading;

using Sandbox.Scripts;
using System;

namespace Sandbox
{
    class Program
    {
        private Model _model;
        
        private PerspectiveCamera _perspectiveCamera;
        private FreeCameraController _controller;

        Program()
        {
            // Register program methods to events
            Event.AddDelegate(Event.Update, Update);
            Event.AddDelegate(Event.Render, Render);

            _perspectiveCamera = new PerspectiveCamera(Instances.Window, 45.0f, 0.1f, 100.0f);
            _controller = new FreeCameraController(_perspectiveCamera, 2.0f);

            var mat = new Material("material", "Assets/cube.shader");
            _model = new Model(mat, new[]
            {
                -1f,  1f, 0, 1.0f, 1.0f, 0, 0, 0,
                -1f, -1f, 0, 1.0f, 0.0f, 0, 0, 0,
                 1f, -1f, 0, 0.0f, 0.0f, 0, 0, 0,
                 1f,  1f, 0, 0.0f, 1.0f, 0, 0, 0
            }, new[] { 0, 1, 2, 2, 3, 0 });
            
            _model.Transform.Scale = new Vector3(1f);
            _model.Transform.Position = new Vector3(0, 0, 4);
            _model.Transform.EulerAngle = new Vector3(0, 40, 0);
        }

        private void Update()
        {
            _model.Update();
            _controller.Update();

            Instances.Window.Title = $"Window - {Time.Fps} FPS";

            if (Input.IsKeyPressed(Keys.M)) Debug.Log(_model.Transform.ToMatrix());
            if (Input.IsKeyPressed(Keys.P)) Debug.Log(_perspectiveCamera.Projection);
            if (Input.IsKeyPressed(Keys.V)) Debug.Log(_perspectiveCamera.View);

            _model.Transform.EulerAngle += new Vector3(0, 0, 0);

            if (Input.IsKeyPressed(Keys.Escape)) GameController.Stop(0);
        }

        private void Render()
        {
            _model.Draw();
        }
        
        static void Main(string[] args)
        {
            File.SetCurrentDirectory(@"D:\\Projets\\Code\\C#\\Emission Engine");

            GameController.Create();
            GameController.CreateDebugger("Emission Console");
            GameController.CreateWindow(WindowParameters.FromJson("window_options.json"));
            GameController.Initiate();

            Program program = new Program();
            GameController.Start();
        }
    }
}
