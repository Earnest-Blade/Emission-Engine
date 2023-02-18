# Emission Engine

Emission is custom game and rendering engine made in C# using custom warper of OpenGl. 
Emission engine can run on Window/MacOS/Linux.
I'm currently using this engine to create my own game, and it grows by technicals stuff that I need. 

## Engine 
It's the main project use for rendering.
Rendering take his foundations on Modern OpenGL and GLFW. Also use OpenAL and Assimp.
It uses faces arrays to render differents kinds of meshes. It also use pre-compiled statics meshes and debug rendering.
Emission cannot render bones animations but im working on these features. 

## Page
Emission Engine is a scene based engine. A scene is named "page". Each page has his own Camera. 

## I/O System
### File formats
Emission Engine come with his own io system.
Emission I/O tools can use:
- Images Files (Png, Jpg, Bmp...) with STB Images
- 3D Objects (Obj, Dae, Fbx...) with Assimp

### Build System
Currently working on custom building system and custom asset compression.

#

## Exemple Code
This following code display a simple orange square.
```csharp
public class BasicPlane : Page
{
    // define vertex shader content
    private const string _vertexShaderSource = "#version 330 core\n"
                                               + "layout (location = 0) in vec3 aPos;\n"
                                               + "void main()\n"
                                               + "{\n"
                                               + "   gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\n"
                                               + "}\0";

    // define fragement shader content.
    private const string _fragmentShaderSource = "#version 330 core\n"
                                                 + "out vec4 FragColor;\n"
                                                 + "void main()\n"
                                                 + "{\n"
                                                 + "   FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);\n"
                                                 + "}\n\0";

    private Shader _shader;
    private Model _model;
        
    public BasicPlane() : base("Triangle Scene", new EmptyActor())
    {
        // setup basic camera
        Camera = new PerspectiveCamera(Window, 90f, 0.1f, 400f);
        Camera.Translate((0, 0, -20));
        
        // create a shader from strings
        _shader = new Shader(new ShaderLoader.ShaderStruct(_vertexShaderSource, _fragmentShaderSource));

        // create a new model from a primitive mesh (here a plane).
        _model = ModelPrimitive.PrimitiveFrontPlane(0.5f, 0.5f);
    }

    public override void Update()
    {
        base.Update();

        // when escape key hit, quit game.
        if(Input.IsKeyDown(Keys.Escape)) GameController.Stop(0);
    }

    public override void Render()
    {
        base.Render();
        
        // draw model
        _model.Draw(_shader);
    }

    public static void Main()
    {
        // create main engine
        GameController.Create(EngineSettings.FromJson(".settings"));

        // create window
        GameController.CreateWindow(WindowConfig.Default("Window"));
        GameController.Initiate();

        // load a new page (a new scene)
        PageManager.Enable(new BasicPlane());
            
        // start game
        GameController.Start();
    }
}
```

# Third Party Software

Emission uses the following open-source projects :
- [LTP.Interop.OpenGL](https://github.com/latet-party/LTP.Interop.OpenGL)
- [ASSIMP-NET](https://github.com/assimp/assimp-net)
- [OpenAL-CS](https://github.com/flibitijibibo/OpenAL-CS)
- [Stb Image Sharp](https://github.com/StbSharp/StbImageSharp)