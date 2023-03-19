using System.Collections.Generic;
using System.Linq;

namespace Emission
{
    public static class EngineBehaviour
    {
        public static void Call(string func)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            
            foreach (var behaviour in EngineBehaviourDispatcher.Instance.Stack.Reverse())
            {
                if(behaviour.IsActive) behaviour.GetType().GetMethod(func)?.Invoke(behaviour, null);
            }
        }
        
        public static void AddBehaviour(IEngineBehaviour behaviour)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            GameInstance.EngineBehaviourDispatcher.Attach(behaviour);
        }

        public static bool HasEngineBehaviourDispatcher()
        {
            return GameInstance.EngineBehaviourDispatcher != null;
        }
        
        public static void CreateDispatcher()
        {
            EngineBehaviourDispatcher.Instance = new EngineBehaviourDispatcher();
        }

        public static void RemoveDispatcher()
        {
            if(!HasEngineBehaviourDispatcher()) return;
            GameInstance.EngineBehaviourDispatcher.Clear();
            EngineBehaviourDispatcher.Instance = null;
        }
    }

    internal class EngineBehaviourDispatcher
    {
        public static EngineBehaviourDispatcher Instance;

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


