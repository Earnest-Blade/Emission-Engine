using System;
using Emission;

public class Game : IEngineBehaviour, IConstructable
{
    public IEngineBehaviour Behaviour { get => this; }

    private DebugScene _debugScene;

    public void Construct()
    {
        Behaviour.BindCallbacks();
        _debugScene = new DebugScene();
    }
        
    public void Initialize(){ }

    public void Start()
    {         
        _debugScene.Call();
    }

    public void Update()
    {
        
    }

    public void PreRender() {}
    public void Render()
    {
            
    }
    public void PostRender() {}

    public void Dispose()
    {
        
    }

    public static void Main(string[] args)
    {
        Application.Singleton.Initialize(new Game());
    }

    
}