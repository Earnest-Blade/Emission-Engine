using Emission.Mathematics;

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
    }
}
