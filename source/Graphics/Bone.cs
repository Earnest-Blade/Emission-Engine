using Assimp;
using Emission.Core.Mathematics;

namespace Emission.Graphics
{
    public class Bone
    {
        public readonly string Name;

        public readonly List<VertexWeight> VertexWeights;

        public readonly Matrix4 OffsetMatrix;
        
        public bool HasVertexWeight => VertexWeights.Count > 0;
        public int VertexWeightCount => VertexWeights.Count;

        public Bone()
        {
            Name = string.Empty;
            OffsetMatrix = Matrix4.Identity;
            VertexWeights = new List<VertexWeight>();
        }

        public Bone(string name, List<VertexWeight> vertexWeights, Matrix4 offsetMatrix)
        {
            Name = name;
            VertexWeights = vertexWeights;
            OffsetMatrix = offsetMatrix;
        }
        
        public Bone(string name, List<VertexWeight> vertexWeights, Matrix4x4 offsetMatrix)
        {
            Name = name;
            VertexWeights = vertexWeights;
            OffsetMatrix = new Matrix4(
                offsetMatrix.A1, offsetMatrix.A2, offsetMatrix.A3, offsetMatrix.A4, 
                offsetMatrix.B1, offsetMatrix.B2, offsetMatrix.B3, offsetMatrix.B4, 
                offsetMatrix.C1, offsetMatrix.C2, offsetMatrix.C3, offsetMatrix.C4,
                offsetMatrix.D1, offsetMatrix.D2, offsetMatrix.D3, offsetMatrix.D4);
        }
    }
}
