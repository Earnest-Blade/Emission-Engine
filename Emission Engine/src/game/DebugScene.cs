using System;
using Emission;
using Emission.Math;

using Emission.MultiMeshLoader;

namespace Game
{
    class DebugScene : Scene
    {
        private Mesh _mesh;

        public DebugScene() : base("Debug Map")
        {
            
        }

        protected override void OnInitialize()
        {
            _mesh = new Mesh(ModelLoader.LoadWavefront("assets/models/monkey.obj"));

            _mesh.Transform.Scale = 0.1f;
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnUpdate()
        {
            _mesh.Update();
            
            var x = Input.Axis(Axis.Horizontal) * _camera.Front * _camera.Speed * Time.DeltaTime; // Forward-Backward
            var y = Input.Axis(Axis.Vertical) * _camera.Right * _camera.Speed * Time.DeltaTime; // Left-Right
            var z = Input.Axis(Axis.UpDown) * _camera.Up * _camera.Speed * Time.DeltaTime; // Top-Bottom
            _camera.Transform.Position += (x + y + z);

            _mesh.Transform.Scale += Input.Scroll;
            
            if(Input.IsKeyDown(Keys.Z)) _mesh.Transform.RotateFromCurrent(10, 0, 0);
            if(Input.IsKeyDown(Keys.S)) _mesh.Transform.RotateFromCurrent(-10, 0, 0);
        }

        protected override void OnRender()
        {
            _mesh.Render();
        }

        protected override void OnStop()
        {
            
        }
    }
}

/*_mesh = new Mesh(new float[]
            {
                0.5f, 0.5f, 0.0f, 1.0f, 1.0f, // top right
                0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
                -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
                -0.5f, 0.5f, 0.0f, 0.0f, 1.0f
            }, new int[]
            {
                0, 1, 3,   // first triangle
                1, 2, 3
            });*/
