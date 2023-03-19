using Emission;
using Emission.Graphics;
using Emission.Graphics.GeometricPrimitives;
using Emission.Mathematics;
using Emission.Page;
using Emission.Window;

public class Program
{
    private class TestScene : Page
    {
        private Shader _shader;
        private Model _model;
        
        public TestScene()
        {
            Camera = new PerspectiveCamera(Window, 90.0f, 0.1f, 400.0f);
            Camera.Translate((0, 0, -20));
            
            _shader = Shader.FromPath("shader.shader");
            _model = GeometricPrimitive.PrimitivePlane(0.5f, 0.5f);
            _model.Transform.EulerAngle = new Vector3(45, 45, 45);
        }

        public override void Update()
        {
            base.Update();
            
            if(Input.IsKeyDown(Keys.Escape))
                GameController.Stop(0);
        }

        public override void Render()
        {
            base.Render();
            
            _model.Draw(_shader);
        }
    }
    
    public static void Main()
    {
        GameController.Create("../../");
        GameController.CreateWindow("Window Title");
        GameController.Initiate();
        
        PageManager.Enable(new TestScene());
        
        GameController.Start();
    }
}