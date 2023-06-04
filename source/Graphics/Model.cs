using System.Collections.Generic;
using Emission.Core;
using Emission.Core.IO;
using Emission.Graphics;
using Emission.Core.Mathematics;
using Emission.Natives.GLFW;

namespace Emission.Graphics
{
    public class Model
    {
        public Transform Transform;

        public string UniformTransformName;
        public string UniformViewName;
        public string UniformProjectionName;
        public string UniformWindowResolution;
        
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
            UniformWindowResolution = Shader.UNIFORM_WINDOW_RESOLUTION;
        }

        /*public void Draw(Shader shader) 
            => Draw(shader, Transform.ToMatrix(), PageManager.ActiveCamera.View, PageManager.ActiveCamera.Projection);

        public void DrawTransform(Shader shader, Matrix4 transformation)
            => Draw(shader, transformation, PageManager.ActiveCamera.View, PageManager.ActiveCamera.Projection);

        public void DrawProjection(Shader shader, Matrix4 projection) 
            => Draw(shader, Transform.ToMatrix(), PageManager.ActiveCamera.View, projection);

        public void DrawView(Shader shader, Matrix4 view)
            => Draw(shader, Transform.ToMatrix(), view, PageManager.ActiveCamera.Projection);

        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view) => Draw(shader, transformation, view, PageManager.ActiveCamera.Projection);*/
        
        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view, Matrix4 projection)
        {
            shader.Start();

            shader.UseUniformMat4(UniformTransformName, transformation);
            shader.UseUniformMat4(UniformViewName, view);
            shader.UseUniformProjectionMat4(UniformProjectionName, projection);

            /*unsafe
            {
                int width, height;
                Glfw.glfwGetWindowSize(Application.Instance.Context.Window, &width, &height);
                shader.UseUniformVec2(UniformWindowResolution, width, height);
            }*/
            
            foreach (Mesh mesh in _meshes)
                mesh.Draw(shader);
            
            shader.Stop();
        }

        public Mesh[] GetMeshes() => _meshes.ToArray();
        public Mesh FindMeshByIndex(int index) => _meshes[index];
        public Mesh FindMeshByName(string name) => _meshes.Find(x => x.Name != string.Empty || x.Name == name);
    }
}
