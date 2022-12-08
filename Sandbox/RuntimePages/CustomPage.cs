using System.Collections.Generic;

using Emission;
using Emission.Components;
using Emission.Graphics;
using Emission.Graphics.Shading;
using Emission.Mathematics;
using Emission.Page;

namespace Sandbox.RuntimePages
{
    public class CustomPage : Page
    {
        private Model _model;
        private Shader _shader;

        private Matrix4 _ortho;
        
        public CustomPage() : base("Debug Scene", new EmptyEntity())
        {
            PageCamera = new PerspectiveCamera(GameInstance.Window, 90.0f, 0.1f, 400.0f);
            PageCamera.Translate(new Vector3(0, -20, 0));

            _shader = new Shader("Assets/Shaders/cube.shader");
            
            Root.Childs.Add(new EmptyEntity()
            {
                Components = new List<ObjectBehaviour>(new ObjectBehaviour[]
                {
                    new ModelRenderer(ModelPrimitive.PrimitivePlane(100, 100), _shader)
                }),
                Transform = new Transform((0, 0, 100))
            });
            
            _model = Model.FromPath("Assets/Models/duck.dae");
            _model.Transform.Position.Z = 200;

            Viewport viewport = GameInstance.Window.Viewport;
            _ortho = Matrix4.Orthographic(viewport.Width, viewport.Height, 0.01f, 400.0f);
        }

        public override void Update()
        {
            base.Update();
            
            if(Input.IsKeyPressed(Keys.Escape)) GameController.Stop(0);
            if(Input.IsKeyPressed(Keys.R)) Disable();
        }

        public override void Render()
        {
            base.Render();

            _model.DrawProjection(_shader, _ortho);
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
