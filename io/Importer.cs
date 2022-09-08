namespace Emission.IO
{
    public class Importer
    {
        public static Model LoadOBJ(string path)
        {
            var loader = new OBJLoader(path);
            return loader.Load();
        }
    }
}