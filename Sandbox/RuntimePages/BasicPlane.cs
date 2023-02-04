
using Emission;
using Emission.UI;
using Emission.Page;
using Emission.Graphics;
using Emission.Graphics.Shading;
using Emission.Mathematics;

namespace Sandbox.RuntimePages
{
    public class BasicPlane : Page
    {
        private Shader _shader;
        private Model _model;

        private ImGuiController _guiController;
        
        public BasicPlane() : base("Triangle Scene", new EmptyActor())
        {
            Camera = new PerspectiveCamera(Window, 90f, 0.1f, 400f);
            Camera.Move((0, -20, -20), (0, 0, -00));
            
            _shader = new Shader("Assets/Shaders/textureObj.shader");

            _guiController = new ImGuiController();

            _model = ModelBuilder.FromFile("Assets/Models/duck.dae", "Assets/Textures/");
            _model.Transform.Position.Z = 200;
        }

        public override void Update()
        {
            base.Update();

            if(Input.IsKeyDown(Keys.Escape)) GameController.Stop(0);
        }

        public override void Render()
        {
            base.Render();
            
            _model.Draw(_shader);
        }
    }
}
