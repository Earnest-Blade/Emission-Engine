using Emission;
using Emission.GFX;

public class Game : IEngineBehaviour
{
    public IEngineBehaviour Behaviour { get => this; }

    private DebugScene _debugScene;

    public void BeforeInit()
    {
        Behaviour.BindCallbacks();
        _debugScene = new DebugScene();
    }

    void IEngineBehaviour.Initialize(){}

    void IEngineBehaviour.Start()
    {        
        _debugScene.Load();
    }

    void IEngineBehaviour.Update()
    {
        switch (Input.CurrentKey)
        {
            case Keys.F1:
                Camera.Main.DrawMode = DrawMode.Fill;
                break;
            
            case Keys.F2:
                Camera.Main.DrawMode = DrawMode.Line;
                break;
        }
    }

    void IEngineBehaviour.PreRender() {}
    void IEngineBehaviour.Render()
    {
        
    }
    void IEngineBehaviour.PostRender() {}

    void IEngineBehaviour.Dispose()
    {
        _debugScene.Unload();
    }

    public static void Main(string[] args)
    {
        /*Builder builder = new Builder();
        builder.CreateInput(Resources.GetFullPath("assets/"))
               .CreateOutput(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location));
        
        builder.Build();
        builder.Write();*/

        Game game = new Game();
        Application app = Application.GenerateApplication();

        app.CreateDebugger()
            .CreateWindow(WindowSettings.GetDefault())
            .CreateRenderer();
        
        app.RegisterInitMethod(game.BeforeInit);
        app.Initialize();
    }

    
}