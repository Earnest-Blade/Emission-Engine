# Emission Engine

> Welcome to Emission !

<br>

**Emission Engine** is an open-source custom game and rendering engine made in C# using custom wrapper of OpenGL. 
Emission engine can **only run on Windows** but I'm working on engine's portability. 
I'm currently using this engine to create my own game, and **it grows by adding the technical stuff that I need**. Currently, Emission has his own Math library, use [Stb Image](https://github.com/nothings/stb) to load images and [Assimp](https://github.com/assimp/assimp) to load models. So the engine can handle multiple files like `.png`, `.bmp`, `.fbx` or `.dae` files.

I started to create Emission because I wanted to understand how game engines works and because I didn't find a simple c# rendering framework. So I create a shitty program with [OpenTK](https://opentk.net/) and I loved creating this kind of stuff. That's why I create Emission *- what a nice story ! -*

This engine is not performant as Unity or Unreal but I'm trying to make nicer and nicer every day.
If you want to support Emission's development, you can by helping comment or adding features. You can also report bugs : it helps a lot for improving the engine ! 

<br>

---

## How to install Emission ?

### Prerequisites
1. [Git](https://git-scm.com/) to clone Emission's Repository.
2. [.NET 6.0 (or higher)](https://dotnet.microsoft.com/en-us/download) ro run the engine.
3. [Visual Studio 2022](https://visualstudio.microsoft.com/fr/), a c# IDE (but Rider works too) and these following components in Visual Studio:
   - `.NET desktop development` with `.NET Framework 4.8`

### Create a new project
1. Open a new terminal, get to a directory and clone Emission with `git clone https://github.com/Earnest-Blade/Emission-Engine`.
   - You can also download it as zip from github.
2. Deplicate the [Sample Projet](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.Sample), it will be a base for your project. You can rename the folder and the `.csproj` file. 
3. If you can't use Emission, you'll may have to change Emission's path:
   - Change in IDE :
      1. Open Visual Studio 2022 and open your project. Then, Solution -> Add -> Existing project and select `Emission Engine.csproj` in Emission's `source` folder location.
      2. Select your project and \<Your Project> -> Add -> Project Reference -> Emission Engine.
   - Or change in the `.csproj` file :
       1.  In a text editor, open `<Your project>.csproj` file
       2.  Find `<ProjectReference Inculde="...">` and change the Include parameter by copying the path to `Emission Engine.csproj` in Emission's `source` folder location.

### Basic Program
This following code comes from the [Simple Cube Example](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.SimpleCube). It renders a simple white cube to the screen.
```csharp
public static class SimpleCube
{
    /* Create a scene - a page is kind of like a scene but with more creativity with his name :) - */
    private class CubePage : Page
    {
        // Represent the way that a model is show 
        private Shader _shader;
        
        // Represent vertices of the model
        private Model _model;
        
        /* Constructor */
        public CubePage()
        {
            // Create a camera that use perspective
            Camera = Camera.CreatePerspectiveCamera(90.0f, Window.Viewport, 0.01f, 400.0f);
            Camera.Translate(new Vector3(0, 0, -20)); // Move the camera -20 on the Z axz (move the camera backward)

            // Create a simple geometry
            _model = GeometricPrimitive.PrimitiveCube(new Vector3(10));
            
            // Create a shader by reading a file
            _shader = Shader.FromPath("shader.shader");
        }

        /* Call every time the window needs to be draw */
        public override void Render()
        {
            base.Render();
            
            // draw the model on screen using the shader.
            _model.Draw(_shader);
        }
    }
    
    public static void Main()
    {
        GameController.Create("../"); // Create a new game context.
        GameController.CreateWindow("Window Title"); // Create a new window.
        GameController.Initiate(); // Ask the game context to start initializing.

        // Enable the scene.
        PageManager.Enable(new CubePage());
        
        // start the game.
        GameController.Start();
    }
}

```

## Demos and Examples

In the `example` folder, you can find some basics programs uses to test features of the engine. Theses are some :
- [Basic Cube Example](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.SimpleCube) : a simple program that render a cube to the screen. Use in this README file.
- [Basic Texture Example](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.SimpleTexture) : a plane that has a texture and another that has two textures.
- [Model Loading Example](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.Simple3DModel) : an example of the way to load a 3D model using Assimp and Emission.
  
  > More examples will be added as features comes.

<br>

---

## Shaders (GLSL)
In order to create shaders, you will need to learn GLSL. [Here is a good introduction](https://learnopengl.com/Getting-started/Shaders) into OpenGL and GLSL shaders.

Here is a simple example use in the [Basic Cube Example](https://github.com/Earnest-Blade/Emission-Engine/tree/master/examples/Emission.SimpleCube) to render a monochromatic shape.
```glsl
// This block is copied on top of every shader
define:
	#version 440

// Vertex shader
vertex:
	layout(location = 0) in vec3 iPosition; // Get vertex position in 3D space from C# code.

	uniform mat4 uProjection; // Get Projection Matrix4 from camera.
	uniform mat4 uView; // Get View Matrix4 from camera.
	uniform mat4 uTransform; // Get Transform Matrix4 from object that is rendered.

	void main(void){
        // Define the vertex's position.
		gl_Position = uProjection * uView * uTransform * vec4(iPosition, 1.0);
	}

// Fragment shader
fragment:
	out vec4 oFrag;

	void main(void){
		oFrag = vec4(1.0, 1.0, 1.0, 1.0);
	}
```
<br>

---

Emission Engine is *completely free and open source* covered by the [MIT Licence](https://github.com/Earnest-Blade/Emission-Engine/blob/main/License.md) but some files are copied from other projects.
You can find the list of the Third Party projects [here](https://github.com/Earnest-Blade/Emission-Engine/blob/main/THIRD%20PARTY.md).

> *Note : Sorry for my bad english : I'm french :)*