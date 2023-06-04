using Emission.Core;
using Emission.Core.Mathematics;

using Emission.Engine;
using Emission.Engine.Page;
using Emission.Engine.Window;

using Emission.Graphics;
using Emission.Graphics.UI;
using Emission.Graphics.GeometricPrimitives;

namespace Emission.Projects.Indev
{
    public class IndevPage : Page, IUserInterface
    {
        public UIContext Context { get; }

        private Model _model;
        private Shader _shader;

        private Skybox _skybox;

        public IndevPage()
        {
            Camera = Camera.CreatePerspectiveCamera(90.0f, Window.Viewport, 0.01f, 400.0f);
            Camera.Position = new Vector3(0, 0, -20);

            Context = new UIContext();
            Context.Register(this);

            //_skybox = new Skybox("skybox.png", 500);
            //RegisterActor(_skybox);

            //_model = GeometricPrimitive.PrimitiveCube(new Vector3(10), Texture.CreateTextureRGBFromPath("container.png", "texture0"));

            //_shader = Shader.FromPath("shader.glsl");
        }

        public override void Update()
        {
            base.Update();
            
            if (Input.IsKeyDown(Keys.Escape))
            {
                Application.Stop(0);
            }
        }

        public override void Render()
        {
            base.Render();
            
            //_model.Draw(_shader, _model.Transform.ToMatrix(), Camera.View, Camera.Projection);
        }

        public void RenderGUI()
        {
            Nuklear.DrawDemo();
        }
    }
}
