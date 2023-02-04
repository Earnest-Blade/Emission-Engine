using System;
using System.IO;
using System.Collections.Generic;

using JetBrains.Annotations;

using Assimp;
using Assimp.Unmanaged;
using Emission.Graphics.Shading;
using Emission.IO;
using Emission.Mathematics;
using Material = Assimp.Material;

namespace Emission.Graphics
{
    public class ModelBuilder
    {
        public const PostProcessSteps DEFAULT_POST_PROCESS_STEPS = PostProcessSteps.Triangulate | PostProcessSteps.OptimizeMeshes;
        public static AssimpContext CurrentContext => _context;

        private static AssimpContext _context;
        
        private List<Mesh> _meshes;
        private string _assetDirectory;

        private ModelBuilder(Scene scene, string assetDirectory)
        {
            _meshes = new List<Mesh>();
            _assetDirectory = assetDirectory;
            
            ProcessNode(scene.RootNode, scene);
        }

        private Model CreateModel()
        {
            if (_meshes == null) return null;
            return new Model(Transform.Zero, _meshes);
        }
        
        private void ProcessNode(Node node, Scene scene)
        {
            for (int i = 0; i < node.MeshCount; i++)
            {
                Assimp.Mesh mesh = scene.Meshes[node.MeshIndices[i]];
                _meshes.Add(ProcessMesh(mesh, scene));
            }

            for (int i = 0; i < node.ChildCount; i++)
                ProcessNode(node.Children[i], scene);
        }

        private Mesh ProcessMesh(Assimp.Mesh mesh, Scene scene)
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
                    Debug.Warning($"[WARNING] Vertex {i} don't have texture coords!");
                
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

        internal static void InitializeContext()
        {
            _context = new AssimpContext();
        }

        internal static void ReleaseContext()
        {
            _context.Dispose();
        }
        
        public static Model FromFile([CanBeNull] string path, [CanBeNull] string asset) => FromFile(path, asset, PostProcessSteps.None);
        public static Model FromFile([CanBeNull] string path, [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = GameFile.OpenText(path);
            return FromStream(reader.BaseStream, asset, steps);
        }

        public static Model FromMemory(MemoryStream stream, [CanBeNull] string asset) => FromMemory(stream, asset, PostProcessSteps.None);
        public static Model FromMemory(MemoryStream stream,  [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (stream.Length == 0) return null;
            return FromStream(stream, asset, steps);
        }

        public static Model FromStream(Stream stream, [CanBeNull] string asset) => FromStream(stream, asset, PostProcessSteps.None);
        public static Model FromStream(Stream stream, [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (_context == null)
                throw new EmissionException(EmissionErrors.EmissionAssimpException, "Trying to import model, but Assimp Context is not create!");
            if (_context.IsDisposed)
                throw new EmissionException(EmissionErrors.EmissionAssimpException, "Trying to import model, but Assimp Context is disposed!");
            
            if(string.IsNullOrEmpty(asset))
                Debug.Warning($"[WARNING] Trying to import model, but asset directory is null or empty. Assimp will try to load assets in {GameDirectory.GetCurrentDirectory()}");

            if (stream == null || !stream.CanRead)
                throw new EmissionException(EmissionErrors.EmissionAssimpException, $"Trying to import import model from stream, but {nameof(stream)} is null or cannot be read!");
            
            Scene scene = _context.ImportFileFromStream(stream, steps);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == null)
            {
                stream.Dispose();
                throw new EmissionException(EmissionErrors.EmissionAssimpException, "Cannot load Assimp Model!");
            }

            ModelBuilder builder = new ModelBuilder(scene, asset);
            return builder.CreateModel();
        }

        public static Model CreateEmpty()
        {
            return new Model(Transform.Zero, new Mesh());
        }

        public static Model FromMesh(Mesh mesh)
        {
            return new Model(Transform.Zero, mesh);
        }
    }
}
