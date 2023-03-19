using System;
using Emission.Mathematics;

namespace Emission.Graphics.GeometricPrimitives
{
    public static partial class GeometricPrimitive
    {
        /// <summary>
        /// Create a simple plane model. 
        /// </summary>
        /// <param name="width">Width of the plane</param>
        /// <param name="heigth">Height of the plane</param>
        public static Model PrimitivePlane(float width, float height) => PrimitivePlane(Transform.Zero, width, height);

        /// <summary>
        /// Create a simple plane model. 
        /// </summary>
        /// <param name="transform">Transformation of the created model</param>
        /// <param name="width">Width of the plane</param>
        /// <param name="heigth">Height of the plane</param>
        public static Model PrimitivePlane(Transform transform, float width, float height) => PrimitivePlane(transform, width, height, Array.Empty<Texture>());
        
        /// <summary>
        /// Create a simple plane model. 
        /// </summary>
        /// <param name="transform">Transformation of the created model</param>
        /// <param name="width">Width of the plane</param>
        /// <param name="heigth">Height of the plane</param>
        /// <param name="texture">Texture to apply on the plane</param>
        public static Model PrimitivePlane(Transform transform, float width, float height, Texture texture) => PrimitivePlane(transform, width, height, new[] { texture });
        
        /// <summary>
        /// Create a simple plane model. 
        /// </summary>
        /// <param name="transform">Transformation of the created model</param>
        /// <param name="width">Width of the plane</param>
        /// <param name="heigth">Height of the plane</param>
        /// <param name="textures">An array of textures to apply to the plane</param>
        public static Model PrimitivePlane(Transform transform, float width, float height, Texture[] textures)
        {
            return ModelBuilder.FromMesh(new Mesh(
                new Vertex[]
                {
                    new Vertex(new Vector3(-width/2, 0, height/2), Vector3.UnitY, (0, 1)),
                    new Vertex(new Vector3(-width/2, 0, -height/2), Vector3.UnitY, (0, 0)),
                    new Vertex(new Vector3(width/2, 0, -height/2), Vector3.UnitY, (1, 1)),
                    new Vertex(new Vector3(width/2, 0, height/2), Vector3.UnitY, (1, 0)),
                },
                new uint[]
                {
                    0, 1, 2,
                    0, 2, 3
                },
                textures
            ));
        }

        public static Model PrimitiveLine(Ray ray, float distance) => PrimitiveLine(ray.Position, ray.Position + (distance * ray.Direction));
        public static Model PrimitiveLine(Vector3 start, Vector3 end) => PrimitiveLine(Transform.Zero, start, end);
        public static Model PrimitiveLine(Transform transform, Vector3 start, Vector3 end)
        {
            return ModelBuilder.FromMesh(new Mesh(
                new Vertex[]
                {
                    new Vertex(start, Vector3.Zero, Vector2.Zero),
                    new Vertex(end, Vector3.Zero, Vector2.Zero)
                },
                new uint[]
                {
                    0, 1
                },
                Array.Empty<Texture>()
            ));
        }
    }
}
