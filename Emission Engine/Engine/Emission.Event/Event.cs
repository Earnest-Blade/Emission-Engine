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
        public const string Initialize = "Initialize";
        public const string Start = "Start"; 
        public const string Update = "Update"; 
        public const string Render = "Render"; 
        public const string Stop = "Stop";
        
        /* Window Events */
        public const string WindowResize = "WindowResize";
        public const string WindowMove = "WindowMove";
        public const string WindowClose = "WindowClose"; 
        public const string WindowIconify = "WindowIconify"; 
        public const string WindowMaximize = "WindowMaximize";
        public const string WindowFocus = "WindowFocus";
        
        /* Keyboard and Mouse Events */
        public const string Key = "Key";
        public const string Button = "Button";
        public const string MouseScroll = "MouseScroll"; 
        public const string MouseMove = "MouseMove";

        public static void AddDelegate(string type, EmissionHandler handler)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                GameInstance.EventDispatcher.Add(type, handler);
                return;
            }

            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }
        
        public static void AddDelegate<T>(string type, EmissionHandler<T> handler)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                GameInstance.EventDispatcher.Add(type, handler);
                return;
            }
            
            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }
        
        public static void RemoveDelegate(string type, EmissionHandler handler)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                GameInstance.EventDispatcher.Remove(type, handler);
                return;
            }
            
            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }
        
        public static void RemoveDelegate<T>(string type, EmissionHandler<T> handler)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                GameInstance.EventDispatcher.Remove(type, handler);
                return;
            }
            
            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }

        public static void Invoke(string type)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                try
                {
                    GameInstance.EventDispatcher.Invoke(type);
                }
                catch (Exception e) { throw new EmissionException(Errors.EmissionEventException, e); }
                return;
            }
            
            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }
        
        public static void Invoke<T>(string type, T args)
        {
            if (HasEventDispatcher() && EventExists(type))
            {
                try
                {
                    GameInstance.EventDispatcher.Invoke<T>(type, args);
                }
                catch (Exception e) { throw new EmissionException(Errors.EmissionEventException, e); }
                return;
            }
            
            throw new EmissionException(Errors.EmissionEventException, $"Cannot find Event type '{type}'!");
        }
        
        public static bool HasEventDispatcher()
        {
            return GameInstance.EventDispatcher != null;
        }

        public static bool EventExists(string name)
        {
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
                throw new EmissionException(Errors.EmissionEventException, $"Event Dispatcher isn't initialize!");

            GameInstance.EventDispatcher.Dispose();
            EventDispatcher.Instance = null;
        }

        public class EventDispatcher : IDisposable
        {
            public static EventDispatcher Instance;
            
            public event EmissionHandler OnInitialize;
            public event EmissionHandler OnStart;
            public event EmissionHandler OnUpdate;
            public event EmissionHandler OnRender;
            public event EmissionHandler<int> OnStop;

            public event EmissionHandler OnWindowClose;
            public event EmissionHandler<Vector2> OnWindowResize;
            public event EmissionHandler<Vector2> OnWindowMove;
            public event EmissionHandler<bool> OnWindowIconify;
            public event EmissionHandler<bool> OnWindowMaximize;
            public event EmissionHandler<bool> OnWindowFocus;

            public event EmissionHandler<(Keys keys, InputState action)> OnKey;
            public event EmissionHandler<(MouseButton buttons, InputState action)> OnButton;
            
            public event EmissionHandler<Vector2> OnMouseMove;
            public event EmissionHandler<double> OnMouseScroll;

            //private string _current;

            public EventDispatcher() { }

            public void Add([NotNull]string name, EmissionHandler handler)
            {
                GetEvent("On" + name)!.AddEventHandler(this, handler);
            }

            public void Add<T>([NotNull]string name, EmissionHandler<T> handler)
            {
                GetEvent("On" + name)!.AddEventHandler(this, handler);
            }

            public void Remove([NotNull]string name, EmissionHandler handler)
            {
                GetEvent("On" + name)!.RemoveEventHandler(this, handler);
            }

            public void Remove<T>([NotNull]string name, EmissionHandler<T> handler)
            {
                GetEvent("On" + name)!.RemoveEventHandler(this, handler);
            }
            
            public void Invoke([NotNull]string name)
            {
                var eventDel = (MulticastDelegate)GetType().GetField("On" + name, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(Instance);
                if (eventDel == null) throw new EmissionException(Errors.EmissionEventException, $"Cannot find delegate {name}");
                
                foreach (Delegate handle in eventDel.GetInvocationList())
                {
                    handle.Method.Invoke(handle.Target, Array.Empty<object>());
                }
            }

            public void Invoke<T>([NotNull]string name, T args)
            {
                var eventDel = (MulticastDelegate)GetType().GetField("On" + name, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(Instance);
                if (eventDel == null) throw new EmissionException(Errors.EmissionEventException, $"Cannot find delegate {name}");
                
                foreach (Delegate handle in eventDel.GetInvocationList())
                {
                    handle.Method.Invoke(handle.Target, new object[] { args });
                }
            }

            public EventInfo GetEvent([NotNull]string name)
            {
                return GetType().GetEvent(name);
            }

            public void Dispose()
            {
                OnInitialize = null;
                OnStart = null;
                OnUpdate = null;
                OnRender = null;
                OnStop = null;
                OnWindowClose = null;
                OnWindowResize = null;
                OnWindowMove = null;
                OnWindowIconify = null;
                OnWindowMaximize = null;
                OnWindowFocus = null;
                OnKey = null;
                OnButton = null;
                OnMouseMove = null;
                OnMouseScroll = null; 
            }
        }
    }
}
