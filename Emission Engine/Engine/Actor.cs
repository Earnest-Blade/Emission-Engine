using System;
using System.Collections.Generic;

using Emission.Mathematics;

namespace Emission
{
    [Serializable]
    public abstract class Actor : IDisposable, IEquatable<Actor>
    {
        public List<Actor> Childs;

        public Transform Transform;

        public bool IsActive { get; private set; }

        public Actor()
        {
            Childs = new List<Actor>();
            Transform = Transform.Zero;
        }

        public void Initialize()
        {
            foreach (var child in Childs) child.Initialize();
        }

        public void Start()
        {
            foreach (var child in Childs) child.Start();
        }

        public void Update()
        {
            foreach (var child in Childs) child.Update();
        }

        public void Render()
        {
            foreach (var child in Childs) child.Render();
        }

        public void Stop()
        {
            foreach (var child in Childs) child.Stop();
        }

        public void Dispose()
        {
            SetActive(false);
            
            Transform = null;
            Childs = null;
        }
        
        public void OnEnable()
        {
            foreach (var child in Childs) child.OnEnable();
        }

        public void OnDisable()
        {
            foreach (var child in Childs) child.OnDisable();
        }
        
        public void SetActive(bool value) => IsActive = value;

        public bool Equals(Actor other)
        {
            return other.Transform == Transform && other.Childs == Childs && other.IsActive == IsActive;
        }
    }
    
    public class EmptyActor : Actor
    {
        public EmptyActor(){}
    }
}