using System.Collections.Generic;
using Assimp;
using Material = Assimp.Material;

using Emission.Graphics.Shading;
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

        public Model()
        {
            Transform = new Transform();
            _meshes = new List<Mesh>();
        }
    
        public Model(Transform transform, Mesh mesh) : this(transform, new List<Mesh>() { mesh }) { }
        public Model(Transform transform, List<Mesh> meshes)
        {
            _meshes = meshes;
            Transform = transform;
        }
        
        private void Initialize(string path, string assetDirectory)
        {
            _path = path;
            _assetDirectory = assetDirectory;
            
            AssimpContext ctx = new AssimpContext();
            
            System.IO.StreamReader stream = new System.IO.StreamReader(path);
            Assimp.Scene scene = ctx.ImportFileFromStream(stream.BaseStream, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == null)
            {
                throw new EmissionException(Errors.EmissionAssimpException, $"Cannot load Assimp model '{path}'");
            }

            ProcessNode(scene.RootNode, scene);

            stream.Dispose();
            ctx.Dispose();
        }
        
        public void Draw(Shader shader) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetMain().View, ICamera.GetMain().Projection);

        public void Draw(Shader shader, Matrix4 transformation)
            => Draw(shader, transformation, ICamera.GetMain().View, ICamera.GetMain().Projection);

        public void DrawProjection(Shader shader, Matrix4 projection) 
            => Draw(shader, Transform.ToMatrix(), ICamera.GetMain().View, projection);
        
        public void Draw(Shader shader, Matrix4 transformation, Matrix4 view, Matrix4 projection)
        {
            shader.Start();

            shader.UseUniformMat4(Shader.UNIFORM_TRANSFORM, transformation);
            shader.UseUniformMat4(Shader.UNIFORM_VIEW, view);
            shader.UseUniformProjectionMat4(Shader.UNIFORM_PROJECTION, projection);

            foreach (Mesh mesh in _meshes)
                mesh.Draw(shader);
            
            shader.Stop();
        }

        private void ProcessNode(Node node, Assimp.Scene scene)
        {
            for (int i = 0; i < node.MeshCount; i++)
            {
                Assimp.Mesh mesh = scene.Meshes[node.MeshIndices[i]];
                _meshes.Add(ProcessMesh(mesh, scene));
            }

            for (int i = 0; i < node.ChildCount; i++)
                ProcessNode(node.Children[i], scene);
        }

        private Mesh ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<uint> indices = new List<uint>();
            List<Texture> textures = new List<Texture>();

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vertex vertex = new Vertex()
                {
                    Position = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z),
                    Normal = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z)
                };

                if (mesh.HasTextureCoords(0))
                {
                    vertex.TextureCoords = new Vector2(
                        mesh.TextureCoordinateChannels[0][i].X,
                        mesh.TextureCoordinateChannels[0][i].Y
                    );
                }
                else
                    Debug.Warning($"[WARNING] Vertex {i} don't have texture coordonate!");
                
                vertices.Add(vertex);
            }

            for (int i = 0; i < mesh.FaceCount; i++)
            {
                Face face = mesh.Faces[i];
                for (int j = 0; j < face.IndexCount; j++)
                {
                    indices.Add((uint)face.Indices[j]);
                }
            }

            if (mesh.MaterialIndex >= 0)
            {
                Material mat = scene.Materials[mesh.MaterialIndex];
                List<Texture> diffuseMaps = LoadMaterialTexture(mat, TextureType.Diffuse, "texture_diffuse");
                textures.AddRange(diffuseMaps);
                List<Texture> specularMaps = LoadMaterialTexture(mat, TextureType.Specular, "texture_specular");
                textures.AddRange(specularMaps);
            }
            
            return new Mesh(vertices, indices, textures);
        }

        private List<Texture> LoadMaterialTexture(Material mat, TextureType type, string typeName)
        {
            List<Texture> textures = new List<Texture>();
            for (int i = 0; i < mat.GetMaterialTextureCount(type); i++)
            {
                mat.GetMaterialTexture(type, i, out TextureSlot slot);
                Texture texture = new Texture(slot, _assetDirectory + slot.FilePath);
                texture.Bind();
                
                textures.Add(texture);
            }
            
            return textures;
        }

        public static Model FromPath(string path) => FromPath(path, GameFile.ExtractDirectory(path) + '/');
        public static Model FromPath(string path, string assetDirectory)
        {
            Model model = new Model();
            model.Initialize(path, assetDirectory);
            return model;
        }
    }
}
