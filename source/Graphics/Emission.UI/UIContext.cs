using System.Runtime.InteropServices;

using Emission.Core;
using nuklear;
using static nuklear.nuklear;

namespace Emission.Graphics.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UIContext
    {
        public const int DEFAULT_BUFFER_SIZE = 524288;
        public const int DEFAULT_ELEM_BUFFER_SIZE = 524288;

        public long* Handle;
        public int MaxBufferSize;
        public int MaxElementBufferSize;

        public bool UseAntiAliasing;

        private NkContext? _context;

        public UIContext()
        {
            if(!Application.HasInstance())
                return;
            
            Handle = Application.Instance!.Context.Window;
            MaxBufferSize = DEFAULT_BUFFER_SIZE;
            MaxElementBufferSize = DEFAULT_ELEM_BUFFER_SIZE;
            
            InitializeNuklear(Handle, MaxBufferSize, MaxElementBufferSize);
        }

        public UIContext(long* handle, int maxBufferSize, int maxElementBufferSize)
        {
            Handle = handle;
            MaxBufferSize = maxBufferSize;
            MaxElementBufferSize = maxElementBufferSize;

            InitializeNuklear(handle, maxBufferSize, maxElementBufferSize);
        }

        public void Register<T>(T item) where T : IUserInterface
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            
            if(_context == null)
                Debug.LogWarning($"[WARNING] Cannot register UserInterface '{typeof(T).Name}' because UIContext isn't initialize!");
            if (!Application.HasInstance()) return;
            
            if(!UserInterfaceDispatcher.Instance.Contains(item))
                UserInterfaceDispatcher.Instance.Attach(item);
        }

        private NkContext InitializeNuklear(long* handle, int maxVertexBuffer, int maxElementBuffer)
        {
            _context = NkCreateGlfwContext(handle, maxVertexBuffer, maxElementBuffer);
            
            NkBeginFontAtlas();
            
            // TODO: Load custom font
            
            NkEndFontAtlas();

            Nuklear.ActiveContext = _context;
            return _context;
        }
    }
}