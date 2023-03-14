using System;
using System.Reflection;
using Emission.Mathematics;
using JetBrains.Annotations;

namespace Emission
{
    internal class EventDispatcher : IDisposable
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

        internal EventDispatcher() { }

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

        public void Invoke([NotNull] string name) => Invoke<object>(name, null);
        public void Invoke<T>([NotNull]string name, T args)
        {
            MulticastDelegate eventDel = (MulticastDelegate)GetType().GetField("On" + name, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(Instance);
            if (eventDel == null) 
                throw new EmissionException(EmissionErrors.EmissionEventException, $"Cannot find delegate {name}");
            
            object[] param = args == null ? Array.Empty<object>() : new object[] { args };
                
            foreach (Delegate handle in eventDel.GetInvocationList())
            {
                if (!InvokeDelegate(handle, param))
                    throw new EmissionException(EmissionErrors.EmissionEventException, $"Error while calling {handle.Method.Name}!");
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

        private bool InvokeDelegate(Delegate @delegate, [CanBeNull] object[] args)
        {
            if (@delegate == null) return false;

            try
            {
                @delegate.Method.Invoke(@delegate.Target, args);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
