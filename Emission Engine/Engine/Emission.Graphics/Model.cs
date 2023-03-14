using System.Collections.Generic;
using Assimp;
using Emission.Graphics;
using Material = Assimp.Material;
using Emission.Mathematics;
using Emission.IO;

namespace Emission.Graphics
{
    public class Model
    {
        public Transform Transform;
        
        private List<Mesh> _meshes;
        private string _path;
        private string _assetDirectory;

        public Model(Transform transform, Mesh mesh) : this(transform, new List<Mesh>() { mesh }) { }
        public Model(Transform transform, List<Mesh> meshes)
        {
            _meshes = meshes;
            Transform = transform;
        }

        public void Draw(Shader shader) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetCurrent().View, ICamera.GetCurrent().Projection);

        public void DrawTransform(Shader shader, Matrix4 transformation)
            => Draw(shader, transformation, ICamera.GetCurrent().View, ICamera.GetCurrent().Projection);

        public void DrawProjection(Shader shader, Matrix4 projection) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetCurrent().View, projection);

        public void DrawView(Shader shader, Matrix4 view)
            => Draw(shader, Transform.ToMatrix(), view, ICamera.GetCurrent().Projection);
        
        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view, Matrix4 projection)
        {
            shader.Start();

            shader.UseUniformMat4(Shader.UNIFORM_TRANSFORM, transformation);
            shader.UseUniformMat4(Shader.UNIFORM_VIEW, view);
            shader.UseUniformProjectionMat4(Shader.UNIFORM_PROJECTION, projection);

            foreach (Mesh mesh in _meshes)
                mesh.Draw();
            
            shader.Stop();
        }

        public Mesh[] GetMeshes() => _meshes.ToArray();
    }
}
