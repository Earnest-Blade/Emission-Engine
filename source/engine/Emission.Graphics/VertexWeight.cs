namespace Emission.Graphics
{
    public struct VertexWeight : IEquatable<VertexWeight>
    {
        public int VertexID;
        public float Weight;

        public VertexWeight(int vertexId, float weight)
        {
            VertexID = vertexId;
            Weight = weight;
        }

        public static implicit operator VertexWeight(Assimp.VertexWeight v) => new VertexWeight(v.VertexID, v.Weight);
        
        public bool Equals(VertexWeight other)
        {
            return VertexID == other.VertexID && Weight.Equals(other.Weight);
        }

        public override bool Equals(object? obj)
        {
            return obj is VertexWeight other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VertexID, Weight);
        }
    }
}
