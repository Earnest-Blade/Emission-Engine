using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Emission.Math;
using OpenTK.Mathematics;

namespace Emission.GFX
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 TextureCoordonates;
        public Vector3 Normals;

        public Vertex(Vector3 position, Vertex vertex) : this(position, vertex.TextureCoordonates, vertex.Normals) {}
        public Vertex(Vector3 position, Vector2 textureCoordonates, Vertex vertex) : this(position, textureCoordonates, vertex.Normals) {}
        public Vertex(Vector3 position, Vector3 normals, Vertex vertex) : this(position, vertex.TextureCoordonates, normals) {}

        public Vertex(float x, float y, float z, float tx, float ty, float nx, float ny, float nz) : 
            this(new Vector3(x, y, z), new Vector2(tx, ty), new Vector3(nx, ny, nz)){}
    
        public Vertex(float[] array) : this(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]) {}
        
        public Vertex(Vector3 position, Vector2 textureCoordonates, Vector3 normals)
        {
            Position = position;
            TextureCoordonates = textureCoordonates;
            Normals = normals;
        }

        public float[] ToArray()
        {
            return new[]
            {
                Position.X, Position.Y, Position.Z, 
                TextureCoordonates.X, TextureCoordonates.Y, 
                Normals.X, Normals.Y, Normals.Z
            };
        }

        public override string ToString()
        {
            return $"[X: {Position.X}; Y: {Position.Y}; Z: {Position.Z}]";
        }
        
        public static Vertex operator -(Vertex a, Vertex b)
        {
            return new Vertex(
                a.Position - b.Position,
                a.TextureCoordonates - b.TextureCoordonates,
                a.Normals == b.Normals ? a.Normals : a.Normals - b.Normals
            );
        }

        public static Vertex[] LoadArray(float[] vertices)
        {
            var array = new Vertex[vertices.Length / 8];
            for (int i = 0; i < array.Length; i++) 
                array[i] = new Vertex(vertices.Skip(i * 8).Take(8).ToArray());

            return array;
        }
        
        public static Vertex[][] LoadArray(float[][] vertices)
        {
            var array = new Vertex[vertices.Length][];
            for (int i = 0; i < array.Length; i++) array[i] = LoadArray(vertices[i]);

            return array;
        }

        public static Vertex[] LoadFace(Face[] faces)
        {
            var list = new List<Vertex>();
            foreach (var face in faces) list.AddRange(face.Vertices);

            return list.ToArray();
        }

        public static float[] VerticesToArray(Vertex[] vertices)
        {
            float[] arr = new float[vertices.Length * 8];
            for (int i = 0; i < vertices.Length; i++)
            {
                arr[i * 8] = vertices[i].Position.X;
                arr[i * 8 + 1] = vertices[i].Position.Y;
                arr[i * 8 + 2] = vertices[i].Position.Z;
                arr[i * 8 + 3] = vertices[i].TextureCoordonates.X;
                arr[i * 8 + 4] = vertices[i].TextureCoordonates.Y;
                arr[i * 8 + 5] = vertices[i].Normals.X;
                arr[i * 8 + 6] = vertices[i].Normals.Y;
                arr[i * 8 + 7] = vertices[i].Normals.Z;
            }

            return arr;
        }

        public static Vertex Lerp(Vertex a, Vertex b, float t)
        {
            return new Vertex(Mathf.Lerp(a.Position, b.Position, t),
                Mathf.Lerp(a.TextureCoordonates, b.TextureCoordonates, t), Mathf.Lerp(a.Normals, b.Normals, t));
        }
        
        public static implicit operator float[](Vertex vertex) => vertex.ToArray();
    }
}