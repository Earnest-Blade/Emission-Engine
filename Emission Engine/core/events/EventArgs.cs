using System;

namespace Emission
{
    public class EventArgs : System.EventArgs
    {
        public new static EventArgs Empty => new EventArgs();
        public static EventArgs Null => null;

        public byte Type => _type;

        private byte _type;
        private string _name;
        private bool _isHandle;
        
        public EventArgs()
        {
            _type = Event.None;
            _name = "None";
            _isHandle = false;
        }

        public EventArgs SetType(byte type)
        {
            _type = type;
            return this;
        }

        public EventArgs SetName(string name)
        {
            _name = name;
            return this;
        }
        
        public EventArgs Handle(bool value)
        {
            _isHandle = value;
            return this;
        }

        public virtual void OnHandled() { }
        public virtual void OnUnhandled() { }
    }

    public class ActionEvent : EventArgs
    {
        private Action _action;

        public ActionEvent(Action action)
        {
            _action = action;
        }

        public override void OnHandled()
        {
            base.OnHandled();
            _action?.Invoke();
        }
    }

    public class FloatEvent : EventArgs
    {
        private float _float;

        public FloatEvent(float f)
        {
            _float = f;
        }

        public override void OnHandled()
        {
            
        }
    }
}
