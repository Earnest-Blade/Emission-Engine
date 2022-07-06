using Emission;
using Emission.Toolbox.ImGuiTool;
using Emission.Lighting;
using Emission.Math;
using Emission.Shading;
using Emission.MultiMeshLoader;

using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

class DebugScene : Scene
{
    private Mesh _cube;
    private Mesh _lightSource;

    private Light _light;

    private ImGuiController _controller;

    public DebugScene() : base("Debug Map")
    { 
        Window.Current.ClearColor = new Vector4(0, 0, 0, 255);
        Camera.Transform.Position = new Vector3(5, 0, 0);
    }

    protected override void OnInitialize()
    {
        Material mat = new Material("assets/shader/whiteColor.glsl");

        LightMaterial mat2 = new LightMaterial("assets/shader/basic.glsl");
        //mat2.BindTexture("assets/textures/debug.jpg", "texture0");
        mat2.BindTexture("assets/textures/proto.png", "texture0", TextureUnit.Texture0);

        _light = new Light(new Vector3(10.0f, 4.0f, 2.0f), new Vector3(1.0f, 1.0f, 1.0f));

        _lightSource = new Mesh(mat, ModelLoader.LoadWavefront("assets/models/basic.obj"));
        _lightSource.Transform.Scale = 0.15f;
        _lightSource.Transform.Position = _light.LightPosition;
        
        _cube = new Mesh(mat2, ModelLoader.LoadWavefront("assets/models/basic.obj"));

        _controller = new ImGuiController(Window.Current.WindowSize);
    }

    protected override void OnStart()
    {
        _cube.Show();
        _lightSource.Show();
    }

    protected override void OnUpdate()
    {
        _cube.Update();
        _lightSource.Update();

        _lightSource.Transform.Position = _light.LightPosition;
        
        _controller.WindowResized(Window.Current.WindowSize);
        _controller.Update(Time.DeltaTime);
        
        UpdateCamera();
        
        // Debug Input
        if (Input.IsKeyDown(Keys.Escape)) Application.Singleton.Stop(1);
    }

    protected override void OnRender()
    {
        _cube.Render();
        _lightSource.Render();

        RenderGui();
        _controller.Render();
    }

    protected override void OnStop()
    {
        _cube.Dispose();
    }

    void UpdateCamera()
    {
        var x = Input.Axis(Axis.Vertical) * Mathf.Left * Camera.Speed * Time.DeltaTime; // Left-Right
        var y = Input.Axis(Axis.UpDown) * Mathf.Up * Camera.Speed * Time.DeltaTime; // Top-Bottom
        var z = Input.Axis(Axis.Horizontal) * Mathf.Front * Camera.Speed * Time.DeltaTime; // Forward-Backward
        Camera.Transform.Position += (x + y + z);

        if (Input.IsMouseButtonDown(MouseButton.Button2))
        {
            Camera.Transform.Rotate(0.0f, 
                Input.DeltaMousePosition.Y * Input.Sensivity * Time.DeltaTime,
                Input.DeltaMousePosition.X * Input.Sensivity * Time.DeltaTime
            );
        }
    }

    void RenderGui()
    {
        ImGui.Begin("Debug Window");
        ImGui.SetWindowSize(new System.Numerics.Vector2(400, 500));
        
        ImGui.Text($"FPS: {Time.Fps}");
        ImGui.Text($"DeltaTime {Time.DeltaTime}");
        
        Camera.SubmitImGui();
        _cube.SubmitImGui();
        _light.SubmitImGui();
        
        ImGui.End();
    }
}