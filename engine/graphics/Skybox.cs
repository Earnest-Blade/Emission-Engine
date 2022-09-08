using Emission.IO;
using Emission.Math;
using Emission.Shading;

using OpenTK.Graphics.OpenGL;

namespace Emission.GFX
{
    public class Skybox : Mesh, IEngineRenderer
    {
        public static float DEFAULT_SIZE => 99;

        private float _size;
        private Texture _texture;

        private Color _color;

        public Skybox(string path) : this(new Image(path), DEFAULT_SIZE){}
        public Skybox(Image image) : this(image, DEFAULT_SIZE){}

        public Skybox(Image image, float size)
        {
            _size = size;
            _texture = new Texture("texture0", image);
            Material = new Material("material", "assets/internal/shader/skybox.glsl");
            Material.BindTexture(_texture);
            
            LoadGeometry(StaticMeshes.Cube(size).Faces);
            
            Initialize();
        }

        public override void Initialize()
        {
            _vaoID = Renderer.BindVertexArray();
            _vboID = Renderer.BindVertexBuffer(0, VerticesArray);
            _eboID = Renderer.BindIndices(Indices);
            
            Renderer.EnableVertexArray(2, Renderer.STRIDE, 5);
            
            Material.BindTextures();

            Renderer.DisableVertexArray();
        }

        public override void Update(ref Transform transform)
        {
            Material.Start();
            
            // Color
            Material.Shader.UseUniformVec3("iColor", _color);
            
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

