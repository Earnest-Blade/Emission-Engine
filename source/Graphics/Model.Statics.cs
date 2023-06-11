using Assimp;
using Emission.Core;
using Emission.Core.IO;

namespace Emission.Graphics
{
    public partial class Model
    {
        private static AssimpContext? _context => ModelBuilder._context;

        public static Model FromPath(string? path) => FromPath(path, EDirectory.GetDirectoryFromFilePath(path));
        public static Model FromPath(string? path, string? asset) => FromPath(path, asset, ModelBuilder.DEFAULT_POST_PROCESS_STEPS);
        public static Model FromPath(string? path, string? asset, PostProcessSteps steps)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = EFile.OpenText(path);
            return FromStream(reader.BaseStream, asset, steps);
        }

        public static Model FromMemory(MemoryStream stream, string? asset) => FromMemory(stream, asset, ModelBuilder.DEFAULT_POST_PROCESS_STEPS);
        public static Model FromMemory(MemoryStream stream,  string? asset, PostProcessSteps steps)
        {
            if (stream.Length == 0) return null;
            return FromStream(stream, asset, steps);
        }

        public static Model FromStream(Stream stream, string? asset) => FromStream(stream, asset, ModelBuilder.DEFAULT_POST_PROCESS_STEPS);
        public static Model FromStream(Stream stream, string? asset, PostProcessSteps steps)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            if (_context.IsDisposed)
                throw new ObjectDisposedException(nameof(_context));

            if (string.IsNullOrEmpty(asset))
            {
                Debug.LogWarning($"[WARNING] Trying to import model, but asset directory is null or empty. Assimp will try to load assets in {EDirectory.GetCurrentDirectory()}");
                asset = string.Empty;                
            }

            if (stream == null || !stream.CanRead)
                throw new ArgumentNullException(nameof(steps));
            
            Scene scene = _context.ImportFileFromStream(stream, steps);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == null)
            {
                stream.Dispose();
                throw new EmissionException(EmissionException.ERR_ASSIMP, "Cannot load Assimp Model!");
            }
            
            stream.Close();

            ModelBuilder builder = new ModelBuilder();
            builder.LoadAssimpScene(scene, asset);
            return builder.CreateModel();
        }

        public static Model CreateEmpty()
        {
            return new Model(Transform.Zero, new Mesh());
        }
        
        public static Model FromMesh(Mesh mesh)
        {
            return new Model(Transform.Zero, mesh);
        }
        
        public static void InitializeContext()
        {
            ModelBuilder._context = new AssimpContext();
        }

        public static void ReleaseContext()
        {
            if(_context != null)
                _context.Dispose();
        }
    }
}