﻿using System.Collections.Generic;
using System.Linq;

namespace Emission
{
    public class EngineBehaviour
    {
        public const string Initialize = "Initialize";
        public const string Start = "Start";
        public const string Update = "Update";
        public const string Render = "Render";
        public const string Stop = "Stop";
        
        public static void Call(string func)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            foreach (var behaviour in EngineBehaviourDispatcher.Instance.Stack.Reverse())
            {
                behaviour.GetType().GetMethod(func)?.Invoke(behaviour, null);
            }
        }
        
        public static void AddBehaviour(IEngineBehaviour behaviour)
        {
            if (!HasEngineBehaviourDispatcher()) return;
            Instances.EngineBehaviourDispatcher.Attach(behaviour);
        }

        public static bool HasEngineBehaviourDispatcher()
        {
            return Instances.EngineBehaviourDispatcher != null;
        }
        
        public static void CreateDispatcher()
        {
            EngineBehaviourDispatcher.Instance = new EngineBehaviourDispatcher();
        }

        public static void RemoveDispatcher()
        {
            if(!HasEngineBehaviourDispatcher()) return;
            Instances.EngineBehaviourDispatcher.Clear();
            EngineBehaviourDispatcher.Instance = null;
        }
        
        public class EngineBehaviourDispatcher
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

    public interface IEngineBehaviour
    {
        IEngineBehaviour Behaviour { get; }
        
        public void Initialize();
        public void Start();
        public void Update();
        public void Render();
        public void Stop();

        public void BindBehaviour()
        {
            if (EngineBehaviour.HasEngineBehaviourDispatcher())
            {
                Instances.EngineBehaviourDispatcher.Attach(this);
            }
        }
    }

    
}