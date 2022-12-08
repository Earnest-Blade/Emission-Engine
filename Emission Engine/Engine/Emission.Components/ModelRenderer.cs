using Emission.Graphics;
using Emission.Graphics.Shading;

namespace Emission.Components
{
    public class ModelRenderer : ObjectBehaviour
    {
        public Model Model;
        public Shader Shader;

        public ModelRenderer(){}

        public ModelRenderer(Model model, Shader shader)
        {
            Model = model;
            Shader = shader;
        }

        public override void Render()
        {
            Model.Draw(Shader);
        }
    }
}
