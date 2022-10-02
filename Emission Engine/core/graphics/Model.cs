using System;
using Emission.Shading;
using OpenTK.Graphics.OpenGL;

namespace Emission
{
    public class Model : Mesh
    {
        public Material Material { get; }

        public Model(Material material, float[] data, int[] indices)
        {
            Material = material;
            
            Initialize(data, indices);
        }
        
        public sealed override void Initialize(float[] data, int[] indices)
        {
            base.Initialize(data, indices);
            
            Material.BindTextures();
        }

        public override void Update()
        {
            Material.Start();
            
            Material.Update();
            
            Material.Stop();
        }
        
        public override void Draw()
        {
            Material.Start();
            
            GL.BindVertexArray(_vao);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            
            Material.UseTextures();
            
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
            
            Material.Stop();
        }

        public override void Dispose()
        {
            base.Dispose();
            Material.Dispose();
        }
    }
}
