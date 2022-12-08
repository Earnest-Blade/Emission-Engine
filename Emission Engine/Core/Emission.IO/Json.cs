using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emission.IO
{
    public class Json
    {
        public static void Serialize(object? obj, string path)
        {
            using (StreamWriter stream = new StreamWriter(path))
            {
                string text = JsonSerializer.Serialize(obj);
                stream.WriteLine(text);
            }
        }
        
        public static void Serialize<T>(T? obj, string path)
        {
            using (StreamWriter stream = new StreamWriter(path))
            {
                string text = JsonSerializer.Serialize<T>(obj);
                stream.WriteLine(text);
            }
        }
        
        public static async Task SerializeAsync(object? obj, string path)
        {
            await using (FileStream stream = GameFile.Create(path))
            {
                await JsonSerializer.SerializeAsync(stream, obj);
                await stream.DisposeAsync();
            }
        }

        public static T Deserialize<T>(string path)
        {
            string json = GameFile.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
        
    }
}
