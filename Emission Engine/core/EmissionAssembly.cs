using System;
using System.IO;
using System.Reflection;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Emission
{
    public static class EmissionAssembly
    {
        public static int Load()
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load("OpenTK.Graphics");
            }
            catch
            {
                return -1;
            }

            var provider = new GLFWBindingsContext();

            void LoadBindings(string typeNamespace)
            {
                var type = assembly.GetType($"OpenTK.Graphics.{typeNamespace}.GL");
                if (type == null)
                {
                    return;
                }

                var load = type.GetMethod("LoadBindings");
                load.Invoke(null, new object[] { provider });
            }

            LoadBindings("ES11");
            LoadBindings("ES20");
            LoadBindings("ES30");
            LoadBindings("OpenGL");
            LoadBindings("OpenGL4");

            return 0;
        }
    }
}
