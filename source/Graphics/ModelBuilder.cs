using System;
using System.IO;
using System.Collections.Generic;

using JetBrains.Annotations;

using Assimp;
using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Core.IO;
using Material = Assimp.Material;

namespace Emission.Graphics
{
    public class ModelBuilder
    {
        #region Assimp Loader
        
        public const PostProcessSteps DEFAULT_POST_PROCESS_STEPS = PostProcessSteps.Triangulate | PostProcessSteps.OptimizeMeshes;
        public static AssimpContext? CurrentContext => _context;

        private static AssimpContext? _context;
        
        private readonly List<Mesh>? _meshes;
        private string? _assetDirectory;

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
                Texture[] diffuseMaps = LoadMaterialTexture(mat, TextureType.Diffuse, "texture_diffuse");
                textures.AddRange(diffuseMaps);
                Texture[] specularMaps = LoadMaterialTexture(mat, TextureType.Specular, "texture_specular");
                textures.AddRange(specularMaps);
            }

            for (int i = 0; i < bones.Length; i++)
            {
                List<VertexWeight> vertexWeights = mesh.Bones[i].VertexWeights.Select(vw => new VertexWeight(vw.VertexID, vw.Weight)).ToList();
                bones[i] = new Bone(mesh.Bones[i].Name, vertexWeights, mesh.Bones[i].OffsetMatrix);
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

        private Texture[] LoadMaterialTexture(Material mat, TextureType type, string typeName)
        {
            int size = mat.GetMaterialTextureCount((Assimp.TextureType)(int)type);
            Texture[] arr = new Texture[size];
            for (int i = 0; i < size; i++)
            {
                mat.GetMaterialTexture((Assimp.TextureType)(int)type, i, out TextureSlot slot);

                arr[i] = Texture.CreateTextureFromTextureSlot(typeName, _assetDirectory, slot, TextureUnit.Texture0);
            }
            
            return arr;
        }

        public static void InitializeContext()
        {
            _context = new AssimpContext();
        }

        public static void ReleaseContext()
        {
            if(_context != null)
                _context.Dispose();
        }
        
        #endregion

        #region Static Methods
        public static Model FromPath(string? path) => FromPath(path, EDirectory.GetDirectoryFromFilePath(path));
        public static Model FromPath(string? path, string? asset) => FromPath(path, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromPath(string? path, string? asset, PostProcessSteps steps)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = EFile.OpenText(path);
            return FromStream(reader.BaseStream, asset, steps);
        }

        public static Model FromMemory(MemoryStream stream, string? asset) => FromMemory(stream, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromMemory(MemoryStream stream,  string? asset, PostProcessSteps steps)
        {
            if (stream.Length == 0) return null;
            return FromStream(stream, asset, steps);
        }

        public static Model FromStream(Stream stream, string? asset) => FromStream(stream, asset, DEFAULT_POST_PROCESS_STEPS);
        public static Model FromStream(Stream stream, string? asset, PostProcessSteps steps)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            if (_context.IsDisposed)
                throw new ObjectDisposedException(nameof(_context));

            if (string.IsNullOrEmpty(asset))
            {
                Debug.LogWarning($"[WARNING] Trying to import model, but asset directory is null or empty. Assimp will try to load assets in {EDirectory.GetCurrentDirectory()}");
                asset = string.Empty;                
            }

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
        
        #endregion
    }
}
