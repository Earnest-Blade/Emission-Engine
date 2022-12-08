
using Emission.Page;

using System;
using System.IO;
using System.Xml.Serialization;

namespace Sandbox
{
    public class Assets
    {
        /*public static Type[] ComponentsTypes = new[] { typeof(EntityComponent), typeof(GameComponent) };

        public static void Save<T>(T data, string path) where T : struct
        {
            FileStream fs = File.Create(path);
            XmlSerializer serializer = new XmlSerializer(typeof(T), ComponentsTypes);
            serializer.Serialize(fs, data);
            fs.Close();
        }

        public static T Load<T>(string path)
        {
            FileStream fs = File.OpenRead(path);
            XmlSerializer serializer = new XmlSerializer(typeof(T), ComponentsTypes);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);

            using(MemoryStream ms = new MemoryStream(buffer))
            {
                T data = (T)serializer.Deserialize(ms);
                return data;
            }
        }*/
    }
}
