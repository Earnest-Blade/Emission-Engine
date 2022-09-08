using OpenTK.Mathematics;

namespace Emission.Math
{
    public class Random
    {
        public static int RandomFloat(int min, int max)
        {
            return new System.Random().Next(min, max);
        }

        public static Vector3i RandomVector3(Vector3 min, Vector3 max)
        {
            var rnd = new System.Random();
            return new Vector3i(rnd.Next((int)min.X, (int)max.Y), rnd.Next((int)min.Y, (int)max.Y), rnd.Next((int)min.Z, (int)max.Z));
        }
        
        public static Vector3i RandomVector3(int min, int max)
        {
            return RandomVector3(new Vector3(min), new Vector3(max));
        }
    }
}
