using System;
using Emission.IO;

namespace Sandbox
{
    public class Assets
    {
        public static void Compress()
        {
            Bundle asset = new Bundle("Assets");
            asset.Save("Assets");
            asset.Dispose();
        }
    }
}
