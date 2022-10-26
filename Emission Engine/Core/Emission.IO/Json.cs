using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Emission.IO
{
    public class Json
    {
        public static void Serialize([CanBeNull] object obj, string path)
        {
            using (StreamWriter stream = new StreamWriter(path))
            {
                string text = JsonSerializer.Serialize(obj);
                stream.WriteLine(text);
            }
        }
        
        public static void Serialize<T>([CanBeNull] T obj, string path)
        {
            using (StreamWriter stream = new StreamWriter(path))
            {
                string text = JsonSerializer.Serialize<T>(obj);
                stream.WriteLine(text);
            }
        }
        
        public static async Task SerializeAsync([CanBeNull] object obj, string path)
        {
            await using (FileStream stream = System.IO.File.Create(path))
            {
                await JsonSerializer.SerializeAsync(stream, obj);
                await stream.DisposeAsync();
            }
        }

        public static T Deserialize<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
        
    }
}
