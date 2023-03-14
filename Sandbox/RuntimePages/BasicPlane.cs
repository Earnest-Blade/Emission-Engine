using System;
using Emission;
using Emission.UI;
using Emission.Page;
using Emission.Graphics;
using Emission.Graphics.GeometricPrimitives;
using Emission.Mathematics;
using ImGuiNET;

namespace Sandbox.RuntimePages
{
    public class BasicPlane : Page
    {
        private Shader _shader;
        private Model _model;

        private ImGuiController _controller;
        
        public BasicPlane() : base("Plane Scene")
        {
            Camera = new PerspectiveCamera(Window, 90f, 0.1f, 400f);
            Camera.Translate((0, 0, -20));
            
            _shader = Shader.FromPath("Assets/Shaders/cube.shader");
            
            //_controller = new ImGuiController();
            
            //_model = ModelBuilder.FromFile("Assets/Models/duck.dae");

            _model = GeometricPrimitive.PrimitiveCube((100, 100, 100));
            _model.Transform.Position = (0, 00, 200);
        }

        public override void Update()
        {
            base.Update();

            //_controller.Update();
            
            _model.Transform.EulerAngle += Vector3.UnitY * Time.DeltaTime * 10;
            
            if(Input.IsKeyDown(Keys.Escape)) GameController.Stop(0);
        }

        public override void Render()
        {
            base.Render();
            
            //ImGui.ShowDemoWindow();
            
            //_controller.Render();
            
            _model.Draw(_shader);
        }
    }
}
