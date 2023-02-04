
using Emission;
using Emission.UI;
using Emission.Page;
using Emission.Graphics;
using Emission.Graphics.Shading;
using Emission.Window;

namespace Sandbox.RuntimePages
{
    public class BasicPlane : Page
    {
        private Shader _shader;
        private Model _model;
        
        private const string _vertexShaderSource = "#version 330 core\n"
                                                   + "layout (location = 0) in vec3 aPos;\n"
                                                   + "void main()\n"
                                                   + "{\n"
                                                   + "   gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\n"
                                                   + "}\0";

        private const string _fragmentShaderSource = "#version 330 core\n"
                                                     + "out vec4 FragColor;\n"
                                                     + "void main()\n"
                                                     + "{\n"
                                                     + "   FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);\n"
                                                     + "}\n\0";

        //private ImGuiController _guiController;
        
        public BasicPlane() : base("Triangle Scene", new EmptyActor())
        {
            Camera = new PerspectiveCamera(Window, 90f, 0.1f, 400f);
            Camera.Translate((0, 0, -20));
            
            _shader = new Shader(new ShaderLoader.ShaderStruct(_vertexShaderSource, _fragmentShaderSource));

            _model = ModelPrimitive.PrimitiveFrontPlane(0.5f, 0.5f);

            /* _guiController = new ImGuiController();
 
             _model = ModelBuilder.FromFile("Assets/Models/duck.dae", "Assets/Textures/");
             _model.Transform.Position.Z = 200;*/
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
