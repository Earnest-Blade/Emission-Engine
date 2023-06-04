namespace Emission.Graphics
{
    public struct ShaderStruct
    {
        public string? VertexData;
        public string? GeomertyData;
        public string? FragmentData;

        public string? TCSData;
        public string? TESData;

        public bool HasVertexShader = false;
        public bool HasFragmentShader = false;
        public bool HasGeometryShade = false;
        public bool HasTesselationShader = false;

        public ShaderStruct()
        {
            VertexData = null;
            GeomertyData = null;
            FragmentData = null;
            TCSData = null;
            TESData = null;
        }

        public ShaderStruct(string vertexData, string fragmentData)
        {
            VertexData = vertexData;
            GeomertyData = null;
            FragmentData = fragmentData;
            TCSData = null;
            TESData = null;
            HasVertexShader = true;
            HasFragmentShader = true;
        }

        public ShaderStruct(string vertexData, string geomertyData, string fragmentData)
        {
            VertexData = vertexData;
            GeomertyData = geomertyData;
            FragmentData = fragmentData;
            TCSData = null;
            TESData = null;
            HasVertexShader = true;
            HasFragmentShader = true;
            HasGeometryShade = true;
        }

        public void Write(int type, string line)
        {
            switch (type)
            {
                case ShaderBuilder.NONE:
                    return;

                case ShaderBuilder.DEFINE_SHADER:
                    VertexData += line + '\n';
                    GeomertyData += line + '\n';
                    FragmentData += line + '\n';
                    return;

                case ShaderBuilder.VERTEX_SHADER:
                    HasVertexShader = true;
                    VertexData += line + '\n';
                    return;

                case ShaderBuilder.GEOMETRY_SHADER:
                    HasGeometryShade = true;
                    GeomertyData += line + '\n';
                    return;

                case ShaderBuilder.FRAGMENT_SHADER:
                    HasFragmentShader = true;
                    FragmentData += line + '\n';
                    return;

                case ShaderBuilder.TESSELATION_CONTROL_SHADER:
                    HasTesselationShader = true;
                    TCSData = line + '\n';
                    return;

                case ShaderBuilder.TESSELATION_EVAL_SHADER:
                    HasTesselationShader = true;
                    TESData = line + '\n';
                    return;
            }
        }
    }
}