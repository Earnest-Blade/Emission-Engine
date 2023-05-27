using Emission.Core;

namespace Emission.Engine.Page
{
    public class PageManager : IDisposable
    {
        public static Page[] ActivePages => ((Game)Application.Instance!).PageManager._activePages.ToArray();
        public static Page[] RegisteredPages => ((Game)Application.Instance!).PageManager._registerdPages.ToArray();

        public static Camera? ActiveCamera => ((Game)Application.Instance!).PageManager._activeCamera;

        private readonly List<Page> _activePages;
        private readonly List<Page> _registerdPages;

        private Camera? _activeCamera;

        public PageManager()
        {
            _activePages = new List<Page>();
            _registerdPages = new List<Page>();
            _activeCamera = null;
        }

        /// <summary>
        /// Add a new page to the registered pages.
        /// </summary>
        /// <param name="page">Page to add.</param>
        public void Register(Page? page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            
            _registerdPages.Add(page);
        }

        /// <summary>
        /// Remove a page from the registered pages.
        /// </summary>
        /// <param name="page">Page to remove.</param>
        public void Remove(Page? page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            if (_registerdPages.Contains(page))
                _registerdPages.Remove(page);
        }

        /// <summary>
        /// Return a page using his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Page? FindPage(string? name)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException(name);
            
            return _registerdPages.Find(x => x.Name == name);
        }

        /// <summary>
        /// Return a page using his Guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Page? FindPage(Guid? guid)
        {
            if (guid == null)
                throw new ArgumentNullException(nameof(guid));

            return _registerdPages.Find(x => x.Uuid == guid.ToString());
        }
        
        /// <summary>
        /// Activate a page. 
        /// </summary>
        /// <param name="page"></param>
        public void EnablePage(Page page)
        {
            if(!_registerdPages.Contains(page))
                _registerdPages.Add(page);
            
            _activePages.Add(page);
            page.Enable();
        }

        /// <summary>
        /// Desactivate a page.
        /// </summary>
        /// <param name="page"></param>
        public void DisablePage(Page page)
        {
            _activePages.Remove(page);
            page.Disable();
        }

        internal void SetActiveCamera(Camera? camera) => _activeCamera = camera;

        public void Dispose()
        {
            _activePages.Clear();
        }

        /// <summary>
        /// Enable a new page with all visible pages.
        /// </summary>
        /// <param name="page">Page to enable</param>
        public static void Enable(Page page) => ((Game)Application.Instance!).PageManager.EnablePage(page);

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
        /// Try to find a page in registered pages. 
        /// Return null if the page is not in active pages.
        /// </summary>
        /// <param name="name">Page's name.</param>
        /// <returns></returns>
        public static Page? Find(string name) => ((Game)Application.Instance!).PageManager.FindPage(name);

        /// <summary>
        /// Try to find a page in registered pages.
        /// Return null if the page is not registered.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Page? Find(Guid? guid) => ((Game)Application.Instance!).PageManager.FindPage(guid);

        /// <summary>
        /// Check if a page is active or not. Return true if active, false otherwise.
        /// </summary>
        /// <param name="name">Page's name</param>
        /// <returns></returns>
        public static bool IsPageActive(string? name) => IsPageActive(Find(name!));

        public static bool IsPageActive(Guid? guid) => IsPageActive(Find(guid));

        /// <summary>
        /// Check if a page is active or not. Return true if active, false otherwise.
        /// </summary>
        /// <param name="page">Page to check</param>
        /// <returns></returns>
        public static bool IsPageActive(Page? page)
        {
            if (page == null) return false;

            return ((Game)Application.Instance!).PageManager._activePages.Contains(page);
        }

        /// <summary>
        /// Disable an active page.
        /// </summary>
        /// <param name="page">Page to disable</param>
        public static void Disable(Page page) => ((Game)Application.Instance!).PageManager.DisablePage(page);

        /// <summary>
        /// Disable all loaded pages.
        /// </summary>
        public static void DisableAll()
        {
            foreach(Page pg in ((Game)Application.Instance!).PageManager._activePages)
                pg.Disable();
        }
    }
}
