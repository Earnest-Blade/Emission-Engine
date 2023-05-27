using Emission.Core;

namespace Emission.Engine
{
    public static class EngineBehaviour
    {
        public static void Call(string func)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            
            foreach (var behaviour in ((Game)Application.Instance!).BehaviourDispatcher!.Stack.Reverse())
            {
                if(behaviour.IsActive) behaviour.GetType().GetMethod(func)?.Invoke(behaviour, null);
            }
        }
        
        public static void AddBehaviour(IEngineBehaviour behaviour)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            ((Game)Application.Instance!).BehaviourDispatcher!.Attach(behaviour);
        }

        public static bool HasEngineBehaviourDispatcher()
        {
            return Application.HasInstance() && ((Game)Application.Instance!).BehaviourDispatcher != null;
        }
        
        public static void CreateDispatcher()
        {
            ((Game)Application.Instance!).BehaviourDispatcher = new EngineBehaviourDispatcher();
        }

        public static void RemoveDispatcher()
        {
            if(!HasEngineBehaviourDispatcher()) return;
            ((Game)Application.Instance!).BehaviourDispatcher!.Clear();
            ((Game)Application.Instance!).BehaviourDispatcher = null;
        }
    }

    internal class EngineBehaviourDispatcher
    {
        public Stack<IEngineBehaviour> Stack => _behaviours;

        private Stack<IEngineBehaviour> _behaviours;

        public EngineBehaviourDispatcher()
        {
            _behaviours = new Stack<IEngineBehaviour>();
        }
            
        public void Attach(IEngineBehaviour behaviour)
        {
            _behaviours.Push(behaviour);
        }

        public bool ContainBehaviour(IEngineBehaviour behaviour)
        {
            return _behaviours.Contains(behaviour);
        }

        public void Clear()
        {
            _behaviours.Clear();
        }
    }
}


