using System;
using System.Collections.Generic;

namespace Emission
{
    public static class Event
    {
        public const byte None = 0x0;
        
        /* Window Events */
        public const byte WindowResize = 0x1;
        public const byte WindowMove = 0x2;
        public const byte WindowClose = 0x3; 
        public const byte WindowMinimize = 0x4; 
        public const byte WindowFocus = 0x5;
        public const byte WindowLostFocus = 0x6;
        
        /* Engine Life Cycle Events */
        public const byte Initialize = 0x7;
        public const byte Start = 0x8; 
        public const byte Update = 0x9; 
        public const byte Render = 0xA; 
        public const byte Stop = 0xB;
        
        /* Keyboard Events */
        public const byte KeyPress = 0xC; 
        public const byte KeyDown = 0xD; 
        public const byte KeyRelease = 0xE; 
        public const byte KeyType = 0xF;
        
        /* Mouse Events */
        public const byte ButtonPress = 0x10; 
        public const byte ButtonDown = 0x11; 
        public const byte ButtonRelease = 0x12;
        public const byte MouseScroll = 0x13; 
        public const byte MouseMove = 0x14;
        
        public static byte Type => !HasCurrentEvent() ? None : Instances.EventDispatcher.Current.Type;
        public static bool HasNullType => Type == None;

        public static T RegisterEvent<T>(byte type, T eventArgs) 
            where T : EventArgs
        {
            if (!HasEventDispatcher()) return (T)EventArgs.Null;
            Instances.EventDispatcher.AddEvent(eventArgs, type);
            return eventArgs;
        }

        public static void RemoveEvent(byte type)
        {
            if (!HasEventDefine(type)) return;
            
            Instances.EventDispatcher.RmvEvent(type);
        }

        public static void HandleEvent(byte type)
        {
            if (!HasEventDefine(type)) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event '{type}' Does not exist.");
            
            var eventArgs = Instances.EventDispatcher.GetEvent(type);
            Instances.EventDispatcher.Current = eventArgs;
            eventArgs.Handle(true);
            eventArgs.OnHandled();
        }
        
        public static void HandleEvent<T>(byte type, T args)
            where T : EventArgs
        {
            if (!HasEventDefine(type)) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event '{type}' Does not exist.");
            
            var eventArgs = Instances.EventDispatcher.SetEvent(type, args);
            Instances.EventDispatcher.Current = eventArgs;
            eventArgs.Handle(true);
            eventArgs.OnHandled();
        }

        public static void ResetEvent(byte type)
        {
            if (!HasEventDefine(type)) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event '{type}' Does not exist.");
            
            var eventArgs = Instances.EventDispatcher.GetEvent(type);
            Instances.EventDispatcher.Current = EventArgs.Empty;
            eventArgs.Handle(false);
            eventArgs.OnUnhandled();
        }
        
        public static void ResetEvent<T>(byte type, T args)
            where T : EventArgs
        {
            if (!HasEventDefine(type)) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event '{type}' Does not exist.");
            
            var eventArgs = Instances.EventDispatcher.SetEvent(type, args);
            Instances.EventDispatcher.Current = EventArgs.Empty;
            eventArgs.Handle(false);
            eventArgs.OnUnhandled();
        }

        public static T GetEventArgs<T>(byte type)
            where T : EventArgs
        {
            if (!HasEventDispatcher()) return (T)EventArgs.Null;
            return (T)Instances.EventDispatcher.GetEvent(type);
        }

        public static bool HasEventDispatcher()
        {
            return Instances.EventDispatcher != null;
        }

        public static bool HasEventDefine(byte type)
        {
            return Instances.EventDispatcher.HasEvent(type);
        }

        public static bool HasCurrentEvent()
        {
            return HasEventDispatcher() & Instances.EventDispatcher.Current != null;
        }
        
        public static void CreateEventDispatcher()
        {
            EventDispatcher.Instance = new EventDispatcher();
        }

        public static void ResetEventDispatcher()
        {
            if (!HasCurrentEvent()) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event Dispatcher isn't initialize!");
            
            ResetEvent(Instances.EventDispatcher.Current.Type);
            Instances.EventDispatcher.Current = EventArgs.Null;
        }

        public static void DisposeEventDispatcher()
        {
            if (!HasEventDispatcher()) 
                throw new EmissionException(EmissionException.EmissionEventException, $"Event Dispatcher isn't initialize!");
            
            Instances.EventDispatcher.Dispose();
            EventDispatcher.Instance = null;
        }

        public class EventDispatcher : IDisposable
        {
            public static EventDispatcher Instance;

            public EventArgs Current;
            
            private List<EventArgs> _eventArgs;

            public EventDispatcher()
            {
                _eventArgs = new List<EventArgs>();
            }
            
            public void AddEvent(EventArgs args, byte type) => _eventArgs.Add(args.SetType(type));
            public void RmvEvent(byte type) => _eventArgs.Remove(GetEvent(type));

            public EventArgs GetEvent(byte type)
            {
                return _eventArgs.Find(x => x.Type == type);
            }

            public bool HasEvent(byte type)
            {
                return _eventArgs.Find(x => x.Type == type) != null;
            }

            public EventArgs SetEvent(byte type, EventArgs args)
            {
                int index = _eventArgs.FindIndex(x => x.Type == type);
                _eventArgs[index] = args;
                return _eventArgs[index];
            }

            public void Dispose()
            {
                _eventArgs.Clear();
            }
        }
    }
}
