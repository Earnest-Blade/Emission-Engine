using Emission.Core;
using Emission.Core.Mathematics;

using Emission.Engine;
using Emission.Engine.Page;
using Emission.Engine.Window;

using Emission.Graphics;
using Emission.Graphics.Emission.UI;
using Emission.Graphics.GeometricPrimitives;

namespace Emission.Projects.Indev
{
    public class IndevPage : Page
    {
        private Model _model;
        private Shader _shader;

        public IndevPage()
        {
            Camera = Camera.CreatePerspectiveCamera(90.0f, Window.Viewport, 0.01f, 400.0f);
            Camera.Position = new Vector3(0, 0, -20);
            
            _model = GeometricPrimitive.PrimitiveCube(new Vector3(10), Texture.CreateTextureRGBFromPath("img.png", "texture0"));

            _shader = Shader.FromPath("shader.glsl");
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
            Nuklear.PrepareRendering();
            
            base.Render();
            
            _model.Draw(_shader, _model.Transform.ToMatrix(), Camera.View, Camera.Projection);

            Nuklear.DrawDemo();
            
            Nuklear.Render();
        }
    }
}
