using Emission.Core;

namespace Emission.Engine
{
    public static class EngineBehaviour
    {
        public static void Call(string func)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            
            foreach (var behaviour in Application.GetInstanceAs<Game>().BehaviourDispatcher!.Stack.Reverse())
            {
                if(behaviour.IsActive) behaviour.GetType().GetMethod(func)?.Invoke(behaviour, null);
            }
        }
        
        public static void AddBehaviour(IEngineBehaviour behaviour)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            Application.GetInstanceAs<Game>().BehaviourDispatcher!.Attach(behaviour);
        }

        public static bool HasEngineBehaviourDispatcher()
        {
            return Application.HasInstance() && ((Game)Application.Instance!).BehaviourDispatcher != null;
        }
        
        public static void CreateDispatcher()
        {
            Application.GetInstanceAs<Game>().BehaviourDispatcher = new EngineBehaviourDispatcher();
        }

        public static void RemoveDispatcher()
        {
            if(!HasEngineBehaviourDispatcher()) return;
            Application.GetInstanceAs<Game>().BehaviourDispatcher!.Clear();
            Application.GetInstanceAs<Game>().BehaviourDispatcher = null;
        }
    }

    internal class EngineBehaviourDispatcher : IDispatcher<IEngineBehaviour>
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

        public void Clear()
        {
            _behaviours.Clear();
        }

        public bool Contains(IEngineBehaviour item)
        {
            return _behaviours.Contains(item);
        }
    }
}


