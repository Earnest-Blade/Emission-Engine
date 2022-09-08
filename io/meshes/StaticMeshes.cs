using Emission.GFX;

namespace Emission.IO
{
    public static class StaticMeshes
    {
        #region Vertices Only
        /// <summary>
        /// Return a float array containing vertices positions, texture coordinates position and normals.
        /// </summary>
        /// <param name="startX">Start Point X</param>
        /// <param name="startY">Start Point Y</param>
        /// <param name="startZ">Start Point Z</param>
        /// <param name="endX">End Point X</param>
        /// <param name="endY">End Point Y</param>
        /// <param name="endZ">End Point Z</param>
        /// <returns></returns>
        public static float[] LineVertices(float startX, float startY, float startZ, float endX, float endY, float endZ)
        {
            return new[]
            {
                startX, startY, startZ, 0, 0, 0, 0, 0,
                endX,   endY,   endZ,   0, 0, 0, 0, 0
            };
        }

        public static float[] PlaceVertices(float x, float y)
        {
            return new[]
            {
                -x / 2, y / 2, 0, 0.0f, 0.0f, 0, 0, 0,
                -x / 2, -y / 2, 0, 0.0f, 1.0f, 0, 0, 0,
                x / 2, -y / 2, 0, 1.0f, 1.0f, 0, 0, 0,
                x / 2, -y / 2, 0, 1.0f, 1.0f, 0, 0, 0,
                x / 2, y / 2, 0, 1.0f, 0.0f, 0, 0, 0,
                -x / 2, y / 2, 0, 0.0f, 0.0f, 0, 0, 0,
            };
        }

        /// <summary>
        /// Return a float array containing vertices positions, texture coordinates position and normals.
        /// </summary>
        /// <param name="x">X Size of the cube.</param>
        /// <param name="y">Y Size of the cube.</param>
        /// <param name="z">Z Size of the cube.</param>
        /// <returns></returns>
        public static float[] CubeVertices(float x, float y, float z)
        {
            return new[]
            {
                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,

                x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,

                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,

                x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,

                -x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,

                x / 2, -y / 2, -z / 2, 0, 0, 0, 0, 0,
                x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, -z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,

                x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                -x / 2, y / 2, z / 2, 0, 0, 0, 0, 0,
                x / 2, -y / 2, z / 2, 0, 0, 0, 0, 0,
            };
        }
        #endregion

        #region Model

        public static Model Plane(float x, float y, float z)
        {
            Model model = new Model(1);
            model.AddFace(new[]
            {
                -x / 2, y / 2, z, 0.0f, 0.0f, 0, 0, 0,
                -x / 2, -y / 2, z, 0.0f, 1.0f, 0, 0, 0,
                x / 2, -y / 2, z, 1.0f, 1.0f, 0, 0, 0,
                x / 2, y / 2, z, 1.0f, 0.0f, 0, 0, 0
            }, new[]
            {
                0, 1, 2,
                2, 3, 0
            });
            
            return model;
        }

        public static Model Plane(float x, float y) => Plane(x, y, 0);

        public static Model Cube(float x, float y, float z)
        {
            x /= 2; y /= 2; z /= 2;
            
            Model model = new Model(6);
            model.AddFace(new []
                { -x, -y, z, 0, 0, -1, 0, 0, -x, y, z, 0, 1, -1, 0, 0, -x, y, -z, 1, 1, -1, 0, 0, -x, -y, -z, 1, 0, -1, 0, 0 }, 
                Face.QUAD_INDICES);
            
            model.AddFace(new []
                { -x, -y, -z, 0, 0, 0, 0, -1, -x, y, -z, 0, 1, 0, 0, -1, x, y, -z, 1, 1, 0, 0, -1, x, -y, -z, 1, 0, 0, 0, -1 }, 
                Face.QUAD_INDICES);
            
            model.AddFace(new []
                { x, -y, -z, 1, 0, 1, 0, 0, x, y, -z, 1, 1, 1, 0, 0, x, y, z, 0, 1, 1, 0, 0, x, -y, z, 0, 0, 1, 0, 0, }, 
                Face.QUAD_INDICES);
            
            model.AddFace(new []
                { x, -y, z, 1, 0, 0, 0, 1, x, y, z, 1, 1, 0, 0, 1, -x, y, z, 0, 1, 0, 0, 1, -x, -y, z, 0, 0, 0, 0, 1, },
                Face.QUAD_INDICES);
            
            model.AddFace(new [] 
                { -x, -y, -z, 0, 1, 0, -1, 0, x, -y, -z, 1, 1, 0, -1, 0, x, -y, z, 1, 0, 0, -1, 0, -x, -y, z, 0, 0, 0, -1, 0, }, 
                Face.QUAD_INDICES);
            
            model.AddFace(new []
                { x, y, -z, 1, 1, 0, 1, 0, -x, y, -z, 0, 1, 0, 1, 0, -x, y, z, 0, 0, 0, 1, 0, x, y, z, 1, 0, 0, 1, 0 }, 
                Face.QUAD_INDICES);

            return model;
        }

        public static Model Cube(float size) => Cube(size, size, size);

        #endregion
    }
}
