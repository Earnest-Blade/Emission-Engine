using Emission;
using Emission.GFX;
using Emission.Math;
using Emission.Scene;
using Emission.IO;
using Emission.Shading;
using OpenTK.Mathematics;

public class DebugScene : Scene
{
    private CameraController _cameraController;

    private Entity<Mesh> _water;
    private Entity<Mesh> _cube;

    private Entity<Skybox> _skybox;

    public DebugScene() : base("Debug Scene")
    {
        
    }

    protected override void OnInitialize()
    {
        var mat = new Material("material", "assets/shader/basic.glsl");
        mat.BindTexture("assets/textures/container2.png", "texture0");
        
        _water = new Entity<Mesh>()
        {
            Transform = new Transform(Vector3.Zero, new Vector3(90, 0, 0), 1),
            Mesh = new Sprite("assets/textures/debug.jpg")
        };

        _cube = new Entity<Mesh>()
        {
            Transform = Transform.Zero,
            Mesh = new Mesh(mat, StaticMeshes.Cube(2, 2, 2))
        };

        _skybox = new Entity<Skybox>()
        {
            Transform = Transform.Zero,
            Mesh = new Skybox("assets/textures/cubemap.png")
        };

        _cameraController = new CameraController(Camera, 2);
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        _cameraController.Update();
        
        _water.Update();
        _cube.Update();
        _skybox.Update();

        if (Input.IsKeyPressed(Keys.R)) _water.Mesh.Subdivide();
        if (Input.IsKeyDown(Keys.A)) _water.Mesh = new Mesh(Importer.LoadOBJ("assets/models/cube.obj"));
        if(Input.IsKeyDown(Keys.Escape)) Application.Quit();
    }

    protected override void OnRender()
    {
        //_water.Render();
        _cube.Render();
        _skybox.Render();
        
        /*Debug.DrawLine((-200, 0, 0), (200, 0, 0), Color.White);
        Debug.DrawLine((0, -200, 0), (0, 200, 0), Color.White);
        Debug.DrawLine((0, 0, -200), (0, 0, 200), Color.White);*/
    }

    protected override void OnStop()
    {
        
    }
}
