using Emission.Core;
using Emission.Core.Mathematics;

using Emission.Engine;
using Emission.Engine.Page;
using Emission.Engine.Window;

using Emission.Graphics;
using Emission.Graphics.UI;
using Emission.Graphics.GeometricPrimitives;

using nuklear;
using static nuklear.nuklear;

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

            _model = GeometricPrimitive.PrimitiveCube(new Vector3(10), Texture.CreateTextureRGBFromPath("container.png", "texture0"));

            //_shader = Shader.FromPath("shader.glsl");
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (Input.IsKeyDown(Keys.Escape))
                Application.Stop(0);
            
        }

        public override void Render()
        {
            base.Render();

            //Draw(_model, _shader);
            //_model.Draw(_shader, _model.Transform.ToMatrix(), Camera.View, Camera.Projection);
        }

        public void RenderGUI()
        {
            Nuklear.DrawDemo();
            
            if (NkBegin(Nuklear.ActiveContext, "Gamepad buttons", nk_rect(0, 150, 300, 250), (uint)NkPanelFlags.NK_WINDOW_BORDER | (uint)NkPanelFlags.NK_WINDOW_MOVABLE | (uint)NkPanelFlags.NK_WINDOW_TITLE | (uint)NkPanelFlags.NK_WINDOW_NO_SCROLLBAR))
            {
                NkLayoutRowStatic(Nuklear.ActiveContext, 10, 300, 1);

                foreach (Controllers controllers in Input.GetControllers())
                {
                    NkLabel(Nuklear.ActiveContext, $"'{controllers.ToString()}' ", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'A' : {Input.IsControllerButtonDown(ControllerButton.A).ToString()}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'B' : {Input.IsControllerButtonDown(ControllerButton.B).ToString()}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'X' : {Input.IsControllerButtonDown(ControllerButton.X).ToString()}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'Y' : {Input.IsControllerButtonDown(ControllerButton.Y).ToString()}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    
                    NkLabel(Nuklear.ActiveContext, $"'Left X' : {Input.GetControllerAxis(ControllerAxis.LeftX)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'Left X' : {Input.GetControllerAxis(ControllerAxis.LeftY)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    
                    NkLabel(Nuklear.ActiveContext, $"'Right X' : {Input.GetControllerAxis(ControllerAxis.RightX)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'Right X' : {Input.GetControllerAxis(ControllerAxis.RightY)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    
                    NkLabel(Nuklear.ActiveContext, $"'Left Trigger' : {Input.GetControllerAxis(ControllerAxis.LeftTrigger)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                    NkLabel(Nuklear.ActiveContext, $"'Right Trigger' : {Input.GetControllerAxis(ControllerAxis.RightTrigger)}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                }
            }
            
            NkEnd(Nuklear.ActiveContext);
        }
    }
}
