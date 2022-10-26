using Emission;
using Emission.Graphics.Shading;
using Emission.Mathematics;
using static Emission.Graphics.GL.GL;

namespace Emission.Graphics
{
    public class Model : Mesh
    {
        public Material Material { get; }

        public Transform Transform;

        public Model(Material material, float[] data, int[] indices)
        {
            Material = material;
            Transform = Transform.Zero;
            
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
            Material.UseTransform(Transform);
            Material.UseProjection();
            
            Material.Stop();
        }
        
        public override void Draw()
        {
            Material.Start();
            
            glBindVertexArray(_vao);
            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
            
            Material.UseTextures();

            unsafe
            {
                glDrawElements(GL_TRIANGLES, _indices.Length, GL_UNSIGNED_INT, NULL);
            }
            
            glDisableVertexAttribArray(0);
            glDisableVertexAttribArray(1);
            glBindVertexArray(0);
            
            Material.Stop();
        }

        public override void Dispose()
        {
            base.Dispose();
            Material.Dispose();
        }
    }
}
