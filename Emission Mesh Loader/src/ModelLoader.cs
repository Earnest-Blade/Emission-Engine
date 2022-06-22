using System;

namespace Emission.MultiMeshLoader
{
    public class ModelLoader
    {
        public static (float[], int[]) LoadWavefront(string path)
        {
            WavefontLoader loader = new WavefontLoader(path);
            return loader.Load();
        }
    }
}