using System;
using System.Collections.Generic;

namespace Emission.Page
{
    public class PageManager : IDisposable
    {
        public List<Page> ActivePages;
        public List<Page> RegisterdPages;

        public PageManager()
        {
            ActivePages = new List<Page>();
            RegisterdPages = new List<Page>();
        }

        public Page FindPage(string? name)
        {
            if (name == null) return null;
            if (name.Length == 0) return null;
            
            return ActivePages.Find(x => x.Name == name);
        }

        public void EnablePage(Page page)
        {
            ActivePages.Add(page);
            page.Enable();
        }

        public void DisablePage(Page page)
        {
            ActivePages.Remove(page);
            page.Disable();
        }

        public void Dispose()
        {
            ActivePages.Clear();
        }

        /// <summary>
        /// Enable a new page with all visible pages.
        /// </summary>
        /// <param name="page">Page to enable</param>
        public static void Enable(Page page) => GameInstance.PageManager.EnablePage(page);

        /// <summary>
        /// Enable a new page and disable all loaded pages.
        /// </summary>
        /// <param name="page">Page to enable</param>
        public static void Invoke(Page page)
        {
            DisableAll();
            Enable(page);
        }

        /// <summary>
        /// Try to find a page in active pages. 
        /// Return null if the page is not in active pages.
        /// </summary>
        /// <param name="name">Page's name.</param>
        /// <returns></returns>
        public static Page Find(string name) => GameInstance.PageManager.FindPage(name);

        /// <summary>
        /// Check if a page is active or not. Return true if active, false otherwise.
        /// </summary>
        /// <param name="name">Page's name</param>
        /// <returns></returns>
        public static bool IsPageActive(string name) => Find(name) != null;

        /// <summary>
        /// Check if a page is active or not. Return true if active, false otherwise.
        /// </summary>
        /// <param name="page">Page to check</param>
        /// <returns></returns>
        public static bool IsPageActive(Page page)
        {
            if (page == null) return false;

            return GameInstance.PageManager.ActivePages.Contains(page);
        }

        /// <summary>
        /// Disable an active page.
        /// </summary>
        /// <param name="page">Page to disable</param>
        public static void Disable(Page page) => GameInstance.PageManager.DisablePage(page);

        /// <summary>
        /// Disable all loaded pages.
        /// </summary>
        public static void DisableAll()
        {
            foreach(Page pg in GameInstance.PageManager.ActivePages)
                pg.Disable();
        }

        /*public static Page FromXmlFile(string path)
        {
            Page page = null;

            using(FileStream stream = GameFile.Read(path))
            {
                
            }

            return page;
        }

        public static Page CreateNewEmptyPage(string path) => CreateNewEmptyPage(path, string.Empty);
        public static Page CreateNewEmptyPage(string path, string name)
        {
            Page empty = new Page(name, new EmptyEntity());
            SavePage(path, empty);
            return empty;
        }

        public static void SavePage(string path, Page page)
        {
            JsonSerializer serializer = JsonSerializer.CreateDefault(SerializerSettings);
            serializer.Formatting = Formatting.Indented;

            using (FileStream stream = GameFile.Create(path))
            {
                TextWriter writer = new StreamWriter(stream);
                using(JsonTextWriter jsonTextWriter = new JsonTextWriter(writer))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonTextWriter, page, typeof(Page));
                }
                writer.Close();
            }
        }

        private static Type[] GetTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetCustomAttribute<PageSerializable>() != null).ToArray();
        }*/
    }
}
