using System;
using System.Collections.Generic;
using System.Text;

using Emission;
using Emission.Graphics;
using Emission.Natives.GL;
using Emission.Graphics.Shading;
using Emission.Mathematics;
using Emission.Page;

namespace Sandbox.RuntimePages
{
    public class MainPage : Page
    {
        private Model _model;
        private Shader _shader;

        private Matrix4 _ortho;

        public MainPage() : base("Debug Scene", new EmptyActor())
        {
            Camera = new PerspectiveCamera(GameInstance.Window, 90.0f, 0.1f, 400.0f);
            Camera.Translate(new Vector3(0, -20, 0));
            
            _model = ModelPrimitive.PrimitivePlane(50, 50);

            _shader = new Shader("Assets/Shaders/cube.shader");

            Viewport viewport = GameInstance.Window.Viewport;
            _ortho = Matrix4.Orthographic(viewport.Width, viewport.Height, 0.1f, 400.0f);
            //_guiController = new ImGuiController();
        }

        public override void Update()
        {
            base.Update();
            
            //_guiController.Update();

            if(Input.IsKeyPressed(Keys.Escape)) GameController.Stop(0);
            if(Input.IsKeyPressed(Keys.R)) Disable();
        }

        public override void Render()
        {
            base.Render();
            
            _model.DrawProjection(_shader, _ortho);
        }
    }
}
