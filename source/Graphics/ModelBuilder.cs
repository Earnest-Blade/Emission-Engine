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
    internal class ModelBuilder
    {
        public const PostProcessSteps DEFAULT_POST_PROCESS_STEPS = PostProcessSteps.Triangulate | PostProcessSteps.OptimizeMeshes;
        public static AssimpContext? CurrentContext => _context;

        internal static AssimpContext? _context;
        
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

        public Model CreateModel()
        {
            if (_meshes == null || _meshes.Count == 0) 
                return Model.CreateEmpty();
            
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
    }
}
