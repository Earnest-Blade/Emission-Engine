using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace Emission.Math
{
    class MatrixHelper
    {
        public static Matrix4 CreatePerspective(float fov, int width, int height, float nearPlane, float farPlane)
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, nearPlane, farPlane);
        }

        public static Matrix4 CreateOrtho(int width, int height, float nearPlane, float farPlane)
        {
            return Matrix4.CreateOrthographicOffCenter(0, width, height, 0, nearPlane, farPlane);
        } 

    }
}
