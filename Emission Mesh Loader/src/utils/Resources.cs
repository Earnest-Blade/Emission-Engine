using System;
using System.IO;

namespace Emission.MultiMeshLoader
{
    /// <summary>
    /// Copy from 'engine/Resources.cs' in main namespace
    /// </summary>
    internal class Resources
    {
         /// <summary>
        /// Return the absolute path from relative path.
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <returns></returns>
        private static string GetFullPath(string path)
        {
            return Path.Combine(Path.GetFullPath("."), path);
        }

         /// <summary>
        /// Return all strings, lines by lines of a file as a string array. Use <see cref="File.ReadAllLines(string)"/>
        /// to get string array.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>File's content as string array</returns>
        internal static string[] GetAllLines(string path)
        {
            string fullPath = GetFullPath(path);

            try
            {
                Console.WriteLine("[INFO] Reading '" + fullPath + "'...");
                return File.ReadAllLines(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("[ERROR] Cannot find directory in " + fullPath);
                throw new DirectoryNotFoundException();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("[ERROR] Cannot find file in " + fullPath);
                throw new FileNotFoundException();
            }
        }
    }
}