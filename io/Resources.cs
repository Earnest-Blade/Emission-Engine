using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Emission.IO
{
    static class Resources
    {
        /// <summary>
        /// Return the absolute path from relative path.
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            return Path.Combine(Path.GetFullPath("."), path);
        }

        /// <summary>
        /// Return all strings, lines by lines of a file. Use <see cref="File.ReadAllText(string)"/>
        /// to Read all file content.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>File's content as string</returns>
        public static string ReadFile(string path)
        {
            if(path.Length == 0)
            {
                Debug.LogError("[WARNING] The path you are trying to load is empty. Return value will be null");
                return null;
            }

            string fullPath = GetFullPath(path);

            try
            {
                Debug.Log("[INFO] Reading '" +  fullPath + "'...");
                return File.ReadAllText(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find file in " + fullPath);
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Return a FileStream use to read a file. Use <see cref="File.OpenRead(string)"/>
        /// to return a <see cref="FileStream"/>.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>FileStream object</returns>
        public static FileStream OpenFile(string path)
        {
            string fullPath = GetFullPath(path);

            try
            {
                Debug.Log("[INFO] Reading '" + fullPath + "'...");
                return File.OpenRead(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find file in " + fullPath);
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Return all strings, lines by lines of a file as a string array. Use <see cref="File.ReadAllLines(string)"/>
        /// to get string array.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>File's content as string array</returns>
        public static string[] GetAllLines(string path)
        {
            string fullPath = GetFullPath(path);

            try
            {
                Debug.Log("[INFO] Reading '" + fullPath + "'...");
                return File.ReadAllLines(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("[ERROR] Cannot find file in " + fullPath);
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Create a New file using a relative path. Use <see cref="File.Create(string)"/> to create it.
        /// Check if the file exist, then create it. If it's already create, it will do nothing and just display
        /// a warning message.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateNew(string fileName)
        {
            string fullPath = GetFullPath(fileName);
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }
            else
            {
                Debug.Log("[WARNING] File '" + fullPath + "' is not create because it already exist!");
            }
        }

        /// <summary>
        /// Create a New file using a relative path. Use <see cref="File.Create(string)"/> to create it.
        /// Check if the file exist, then create it. If it's already create, it will do nothing and just display
        /// a warning message.
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateNew(string path, string fileName)
        {
            string fullPath = GetFullPath(path + fileName);
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }
            else
            {
                Debug.Log("[WARNING] File '" + fullPath + "' is not create because it already exist!");
            }
        }

        /// <summary>
        /// Write into a file using a relative path. Use <see cref="File.WriteAllText"/> to write with a string as
        /// content. If the file isn't create, it will return an error.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void Write(string path, string content)
        {
            string fullPath = Path.Combine(Path.GetFullPath("."), path);
            if (File.Exists(fullPath))
            {
                try
                {
                    File.WriteAllText(path, content);
                }
                catch(Exception e)
                {
                    Debug.LogError("[ERROR] Cannot write in '" + path + "':\n" + e.Message);
                }
            }
        }
    }
}
