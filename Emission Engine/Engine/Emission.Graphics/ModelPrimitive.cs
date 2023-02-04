using System.Collections.Generic;
using Emission.Graphics.Shading;
using Emission.Mathematics;

namespace Emission.Graphics
{
    public static class ModelPrimitive
    {
        public static Model PrimitivePlane(float width, float height) => PrimitivePlane(Transform.Zero, width, height);
        public static Model PrimitivePlane(Transform transform, float width, float height)
        {
            return ModelBuilder.FromMesh(new Mesh(
                new List<Vertex>
                {
                    new Vertex(new Vector3(-width/2, 0, height/2), Vector3.UnitY, (0, 1)),
                    new Vertex(new Vector3(-width/2, 0, -height/2), Vector3.UnitY, (0, 0)),
                    new Vertex(new Vector3(width/2, 0, -height/2), Vector3.UnitY, (1, 1)),
                    new Vertex(new Vector3(width/2, 0, height/2), Vector3.UnitY, (1, 0)),
                },
                new List<uint>
                {
                    0, 1, 2,
                    0, 2, 3
                },
                new List<Texture>()
            ));
        }

        public static Model PrimitiveFrontPlane(float width, float height) => PrimitiveFrontPlane(Transform.Zero, width, height);
        public static Model PrimitiveFrontPlane(Transform transform, float width, float height)
        {
            return ModelBuilder.FromMesh(new Mesh(
                new List<Vertex>
                {
                    new Vertex(new Vector3(-width/2, height/2, 0), Vector3.UnitY, (0, 1)),
                    new Vertex(new Vector3(-width/2, -height/2, 0), Vector3.UnitY, (0, 0)),
                    new Vertex(new Vector3(width/2, -height/2, 0), Vector3.UnitY, (1, 1)),
                    new Vertex(new Vector3(width/2, height/2, 0), Vector3.UnitY, (1, 0)),
                },
                new List<uint>
                {
                    0, 1, 2,
                    0, 2, 3
                },
                new List<Texture>()
            ));
        }

        public static Model PrimitiveLine(Ray ray, float distance) => PrimitiveLine(ray.Position, ray.Position + (distance * ray.Direction));
        public static Model PrimitiveLine(Vector3 start, Vector3 end) => PrimitiveLine(Transform.Zero, start, end);
        public static Model PrimitiveLine(Transform transform, Vector3 start, Vector3 end)
        {
            return ModelBuilder.FromMesh(new Mesh(
                new List<Vertex>
                {
                    new Vertex(start, Vector3.Zero, Vector2.Zero),
                    new Vertex(end, Vector3.Zero, Vector2.Zero)
                },
                new List<uint>
                {
                    0, 1
                },
                new List<Texture>()
            ));
        }

        public static Model PrimitiveCube(float width, float height, float depth)
        {
            width /= 2;
            height /= 2; 
            depth /= 2;

            return null;
        }
        
        public static Model PrimitiveSphere()
        {
            return null;
        }

    }
}
