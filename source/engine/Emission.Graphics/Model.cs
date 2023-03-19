using System.Collections.Generic;

using Emission.IO;
using Emission.Graphics;
using Emission.Mathematics;

namespace Emission.Graphics
{
    public class Model
    {
        public Transform Transform;

        public string UniformTransformName;
        public string UniformViewName;
        public string UniformProjectionName;
        
        private List<Mesh> _meshes;
        private string _path;
        private string _assetDirectory;

        public Model(Transform transform, Mesh mesh) : this(transform, new List<Mesh>() { mesh }) { }
        public Model(Transform transform, List<Mesh> meshes)
        {
            _meshes = meshes;
            Transform = transform;

            UniformTransformName = Shader.UNIFORM_TRANSFORM;
            UniformViewName = Shader.UNIFORM_VIEW;
            UniformProjectionName = Shader.UNIFORM_PROJECTION;
        }

        public void Draw(Shader shader) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetCurrent().View, ICamera.GetCurrent().Projection);

        public void DrawTransform(Shader shader, Matrix4 transformation)
            => Draw(shader, transformation, ICamera.GetCurrent().View, ICamera.GetCurrent().Projection);

        public void DrawProjection(Shader shader, Matrix4 projection) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetCurrent().View, projection);

        public void DrawView(Shader shader, Matrix4 view)
            => Draw(shader, Transform.ToMatrix(), view, ICamera.GetCurrent().Projection);

        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view) => Draw(shader, transformation, view, ICamera.GetCurrent().Projection);
        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view, Matrix4 projection)
        {
            shader.Start();

            shader.UseUniformMat4(UniformTransformName, transformation);
            shader.UseUniformMat4(UniformViewName, view);
            shader.UseUniformProjectionMat4(UniformProjectionName, projection);

            foreach (Mesh mesh in _meshes)
                mesh.Draw();
            
            shader.Stop();
        }

        public Mesh[] GetMeshes() => _meshes.ToArray();
        public Mesh FindMeshByIndex(int index) => _meshes[index];
        public Mesh FindMeshByName(string name) => _meshes.Find(x => x.Name != string.Empty || x.Name == name);
    }
}
