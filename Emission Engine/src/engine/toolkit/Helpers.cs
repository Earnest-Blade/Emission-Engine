using OpenTK.Mathematics;

using Numerics = System.Numerics;

namespace Emission.Toolbox
{
    public class ColorHelper
    {
        public static Numerics.Vector4 RGB(float r, float g, float b)
        {
            return new Numerics.Vector4(r / 255.0f, g / 255.0f, b / 255.0f, 1); 
        }
        
        public static Vector4 RGBGL(float r, float g, float b)
        {
            return new Vector4(r / 255.0f, g / 255.0f, b / 255.0f, 1); 
        }
    }

    public class NumericsHelper
    {
        public static Numerics.Vector3 Vector3(Vector3 vector)
        {
            return new Numerics.Vector3(vector.X, vector.Y, vector.Z);
        }
        
        public static Vector3 Vector3(Numerics.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
        
        public static Numerics.Vector4 Vector4(Vector4 vector)
        {
            return new Numerics.Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }
        
        public static Vector4 Vector4(Numerics.Vector4 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }
    }
}
