using System.Collections;
using Emission.Core;

namespace Emission.Graphics.UI
{
    public class UserInterfaceDispatcher : IDispatcher<IUserInterface>
    {
        public static UserInterfaceDispatcher Instance => _instance;
        public Stack<IUserInterface> Stack => _interfaces;

        private Stack<IUserInterface> _interfaces;
        private static UserInterfaceDispatcher _instance;
        
        public UserInterfaceDispatcher()
        {
            _interfaces = new Stack<IUserInterface>();
            _instance = this;
        }
        
        public void Attach(IUserInterface item)
        {
            _interfaces.Push(item);
        }

        public void Clear()
        {
            _interfaces.Clear();
        }

        public bool Contains(IUserInterface item)
        {
            return _interfaces.Contains(item);
        }

        public void CallAll()
        {
            foreach (IUserInterface ui in Stack.Reverse()) ui.RenderGUI();
        }
    }
}