using System;
using System.IO;
using System.Collections.Generic;

using JetBrains.Annotations;

using Assimp;

using Emission.Graphics;
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
        
        private readonly List<Mesh> _meshes;
        private string _assetDirectory;

        public ModelBuilder()
        {
            _meshes = new List<Mesh>();
            _assetDirectory = null;
        }

        public void LoadAssimpScene(Scene scene, string assetDirectory)
        {
            _assetDirectory = assetDirectory;
            
            ProcessNode(scene.RootNode, scene);
        }

        public void AddMesh(Mesh mesh)
        {
            _meshes.Add(mesh);
        }

        private Model CreateModel()
        {
            if (_meshes == null || _meshes.Count == 0) 
                return CreateEmpty();
            
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
            Bone[] bones = new Bone[mesh.BoneCount];

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
                {
                    Debug.LogWarning($"[WARNING] Vertex {i} don't have texture coords!");
                }

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

            for (int i = 0; i < bones.Length; i++)
            {
                List<VertexWeight> vertexWeights = mesh.Bones[i].VertexWeights.Select(vw => new VertexWeight(vw.VertexID, vw.Weight)).ToList();
                bones[i] = new Bone(mesh.Bones[i].Name, vertexWeights, new Matrix4(mesh.Bones[i].OffsetMatrix));
            }

            return new Mesh(new MeshData(
                mesh.Name, 
                vertices.ToArray(), 
                indices.ToArray(), 
                textures.ToArray(), 
                bones.ToArray(),
                mesh.PrimitiveType, 
                mesh.BoundingBox
            ));
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

        public static Model FromFile([CanBeNull] string? path) => FromFile(path, GameDirectory.GetDirectoryFromFilePath(path));
        public static Model FromFile([CanBeNull] string? path, [CanBeNull] string asset) => FromFile(path, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromFile([CanBeNull] string? path, [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = GameFile.OpenText(path);
            return FromStream(reader.BaseStream, asset, steps);
        }

        public static Model FromMemory(MemoryStream stream, [CanBeNull] string asset) => FromMemory(stream, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromMemory(MemoryStream stream,  [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (stream.Length == 0) return null;
            return FromStream(stream, asset, steps);
        }

        public static Model FromStream(Stream stream, [CanBeNull] string asset) => FromStream(stream, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromStream(Stream stream, [CanBeNull] string asset, PostProcessSteps steps)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            if (_context.IsDisposed)
                throw new ObjectDisposedException(nameof(_context));
            
            if(string.IsNullOrEmpty(asset))
                Debug.LogWarning($"[WARNING] Trying to import model, but asset directory is null or empty. Assimp will try to load assets in {GameDirectory.GetCurrentDirectory()}");

            if (stream == null || !stream.CanRead)
                throw new ArgumentNullException(nameof(steps));
            
            Scene scene = _context.ImportFileFromStream(stream, steps);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == null)
            {
                stream.Dispose();
                throw new EmissionException(EmissionException.ERR_ASSIMP, "Cannot load Assimp Model!");
            }
            
            stream.Close();

            ModelBuilder builder = new ModelBuilder();
            builder.LoadAssimpScene(scene, asset);
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
