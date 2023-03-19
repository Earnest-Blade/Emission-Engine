using System;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

using Emission.Mathematics;
using Exception = System.Exception;

namespace Emission
{
    public static class Event
    {
        /* Engine Life Cycle Events */
        public const string INITIALIZE = "Initialize";
        public const string START = "Start"; 
        public const string UPDATE = "Update"; 
        public const string RENDER = "Render"; 
        public const string STOP = "Stop";
        
        /* Window Events */
        public const string WINDOW_RESIZE = "WindowResize";
        public const string WINDOW_MOVE = "WindowMove";
        public const string WINDOW_CLOSE = "WindowClose"; 
        public const string WINDOW_ICONIFY = "WindowIconify"; 
        public const string WINDOW_MAXIMIZE = "WindowMaximize";
        public const string WINDOW_FOCUS = "WindowFocus";
        
        /* Keyboard and Mouse Events */
        public const string KEY = "Key";
        public const string BUTTON = "Button";
        public const string MOUSE_SCROLL = "MouseScroll"; 
        public const string MOUSE_MOVE = "MouseMove";

        public static void AddDelegate(string type, EmissionHandler handler)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            GameInstance.EventDispatcher.Add(type, handler);
        }
        
        public static void AddDelegate<T>(string type, EmissionHandler<T> handler)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            GameInstance.EventDispatcher.Add(type, handler);
        }
        
        public static void RemoveDelegate(string type, EmissionHandler handler)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            GameInstance.EventDispatcher.Remove(type, handler);
        }
        
        public static void RemoveDelegate<T>(string type, EmissionHandler<T> handler)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            GameInstance.EventDispatcher.Remove(type, handler);
        }

        public static void Invoke(string type)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            try
            {
                GameInstance.EventDispatcher.Invoke(type);
            }
            catch (Exception e) { throw new EmissionException(EmissionException.ERR_EVENT, e); }
        }
        
        public static void Invoke<T>(string type, T args)
        {
            if (!HasEventDispatcher() || !EventExists(type))
                throw new EmissionException(EmissionException.ERR_EVENT, $"Cannot find Event type '{type}'!");
            
            try
            {
                GameInstance.EventDispatcher.Invoke<T>(type, args);
            }
            catch (Exception e) { throw new EmissionException(EmissionException.ERR_EVENT, e); }
        }
        
        public static bool HasEventDispatcher()
        {
            return GameInstance.EventDispatcher != null;
        }

        public static bool EventExists(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (HasEventDispatcher())
                return GameInstance.EventDispatcher.GetEvent("On" + name) != null;
            
            return false;
        }

        public static void CreateEventDispatcher()
        {
            EventDispatcher.Instance = new EventDispatcher();
        }

        public static void DisposeEventDispatcher()
        {
            if (!HasEventDispatcher()) 
                throw new EmissionException(EmissionException.ERR_EVENT, $"Event Dispatcher isn't initialize!");

            GameInstance.EventDispatcher.Dispose();
            EventDispatcher.Instance = null;
        }

        
    }
}
