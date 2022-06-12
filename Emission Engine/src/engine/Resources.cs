using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Emission
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
                ApplicationConsole.PrintError("[WARNING] The path you are trying to load is empty. Return value will be null");
                return null;
            }

            string fullPath = GetFullPath(path);

            try
            {
                ApplicationConsole.Print("[INFO] Reading '" +  fullPath + "'...");
                return File.ReadAllText(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find file in " + fullPath);
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
                ApplicationConsole.Print("[INFO] Reading '" + fullPath + "'...");
                return File.OpenRead(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find file in " + fullPath);
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
                ApplicationConsole.Print("[INFO] Reading '" + fullPath + "'...");
                return File.ReadAllLines(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                ApplicationConsole.PrintError("[ERROR] Cannot find file in " + fullPath);
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
                ApplicationConsole.Print("[WARNING] File '" + fullPath + "' is not create because it already exist!");
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
                ApplicationConsole.Print("[WARNING] File '" + fullPath + "' is not create because it already exist!");
            }
        }

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
                    ApplicationConsole.PrintError("[ERROR] Cannot write in '" + path + "':\n" + e.Message);
                }
            }
        }

        public static unsafe BitmapData ReadTexture(string path, int* width, int* height)
        {
            using(var image = new Bitmap(path))
            {
                BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                *width = data.Width;
                *height = data.Height;
                return data;
            }
        }
    }
}
