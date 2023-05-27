using System;
using System.Collections.Generic;
using Emission.Core.Mathematics;

namespace Emission.Graphics.GeometricPrimitives
{
    public static partial class GeometricPrimitive
    {
        public static Model PrimitiveSphere(float radius, float rings, float segments) => PrimitiveSphere(radius, rings, segments, new TextureArray());
        public static Model PrimitiveSphere(float radius, float rings, float segments, TextureArray textures)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<uint> indices = new List<uint>();

            float x, y, z, xy;
            float nx, ny, nz, lengthInv = 1.0f / radius;
            float s, t;

            float sectorStep = 2 * MathF.PI / rings;
            float stackStep = MathF.PI / segments;
            float sectorAngle, stackAngle;

            for (int i = 0; i <= segments; i++)
            {
                stackAngle = MathF.PI / 2 - i * stackStep;
                xy = radius * MathF.Cos(stackAngle);
                z = radius * MathF.Sin(stackAngle);

                for (int j = 0; j < rings; j++)
                {
                    sectorAngle = j * sectorStep;

                    x = xy * MathF.Cos(sectorAngle);
                    y = xy * MathF.Sin(sectorAngle);
                    
                    nx = x * lengthInv;
                    ny = y * lengthInv;
                    nz = z * lengthInv;
                    
                    s = j / rings;
                    t = i / segments;
                    
                    vertices.Add(new Vertex((x, y, z), (nx, ny, nz), (s, t)));
                }
            }

            uint k1, k2;
            for (int i = 0; i < segments; i++)
            {
                k1 = (uint)(i * (rings + 1));
                k2 = (uint)(k1 + rings + 1);

                for (int j = 0; j < rings; ++j)
                {
                    if (i != 0)
                    {
                        indices.Add(k1);
                        indices.Add(k2);
                        indices.Add(k1 + 1);
                    }

                    if (i != (segments - 1))
                    {
                        indices.Add(k1 + 1);
                        indices.Add(k2);
                        indices.Add(k2 + 1);
                    }

                    k1++;
                    k2++;
                }
            }
            
            return ModelBuilder.FromMesh(new Mesh(vertices.ToArray(), indices.ToArray(), textures));
        }
    }
}
