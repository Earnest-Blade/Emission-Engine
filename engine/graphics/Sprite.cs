using Emission.IO;
using Emission.Math;
using Emission.Shading;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.GFX
{
    public class Sprite : Mesh, IEngineRenderer
    {
        public string Path { get; }

        public Image Image;
        public Texture Texture;

        public Vector2 Size;

        public Sprite(string path) : this(path, "assets/internal/shader/sprites/sprite.glsl") {}
        
        public Sprite(string path, string shader)
        {
            Material = new Material("material", new Shader(shader));
            Path = path;
            Image = new Image(path);
            
            Texture = new Texture(Path, "texture0", Image);
            Size = Image.Size / Window.Singleton.WindowSize.X;
            
            LoadGeometry(StaticMeshes.Plane(1, 1).Faces);
            
            Initialize();
        }

        public override void Initialize()
        {
            _vaoID = Renderer.BindVertexArray();
            _vboID = Renderer.BindVertexBuffer(0, VerticesArray);
            _eboID = Renderer.BindIndices(Indices);
            
            Material.BindTexture(Texture);
            Material.BindTextures();
            
            Renderer.DisableVertexArray();
        }

        public override void Update(ref Transform transform)
        {
            Material.Start();
            
            // Light
            Material.Update();
            
            // Transformation
            Material.Shader.UseUniformMat4("uTransform", transform.LookAt(Camera.Main.Transform));
            Material.Shader.UseUniformMat4("uView", Camera.Main.ViewMatrix);
            Material.Shader.UseUniformProjectionMat4("uProjection", Camera.Main.ProjectionMatrix);
            
            Material.Stop();
        }

        public override void Render()
        {
            PreRender();

            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
                
            PostRender();
        }
    }
}
