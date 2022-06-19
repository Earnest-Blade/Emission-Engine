using System;

using Emission.Math;
using OpenTK.Graphics.OpenGL;

using Emission.Shading;

namespace Emission
{
    public class Mesh
    {
        public int ID { get; }
        public string Name { get; }
        
        public Shader Shader { get; }
        public Transform Transform;

        protected int _vaoID;
        protected int _vboID;
        protected int _eboID;
        protected int _texID;

        protected float[] _vertices;
        protected int[] _indices;

        public Mesh(float[] vertices, int[] indices)
        {
            Transform = new Transform();
            Shader = new Shader("assets/shader/basic.glsl");

            _vertices = vertices;
            _indices = indices;
            _vaoID = GraphicAllocator.BindVertexArray(_vertices);
            _vboID = GraphicAllocator.Bind3DBuffer(0, _vertices);
            _eboID = GraphicAllocator.BindIndices(_indices);
            _texID = GraphicAllocator.BindTexture2D("assets/textures/debug.jpg");

            ID = int.Parse(_vaoID + "" + _vboID + "" + _eboID);
            Name = "mesh" + ID;

            ApplicationConsole.Print("[INFO] " + Name + " initialized!");
        }

        public virtual void Update()
        {
            Shader.Start();
            Shader.UseUniformMat4("uTransform", Transform.ToMatrix());
            Shader.UseUniformMat4("uView", Camera.Main.ViewMatrix());
            Shader.UseUniformProjectionMat4("uProjection", Camera.Main.ProjectionMatrix());
        }

        public virtual void PreRender()
        {
            
        }

        public virtual void Render()
        {
            PreRender();

            GL.BindVertexArray(_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _texID);

            GL.DrawElements(BeginMode.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            PostRender();
        }

        public virtual void PostRender()
        {
            Shader.Stop();
        }

        public virtual void Destroy()
        {
            Shader.Destroy();
            GraphicAllocator.Clear(_vaoID, _vboID, _eboID, _texID);
        }

    }
}
