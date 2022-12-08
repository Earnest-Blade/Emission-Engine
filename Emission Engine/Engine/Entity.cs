using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Emission.Annotations;
using Emission.Mathematics;

namespace Emission
{
    [Serializable]
    [PageSerializable]
    public abstract class Entity : IDisposable, IEquatable<Entity>
    {
        public List<ObjectBehaviour> Components;
        public List<Entity> Childs;

        public Transform Transform;

        [XmlIgnore]
        public bool IsActive { get; private set; }

        public Entity()
        {
            Components = new List<ObjectBehaviour>();
            Childs = new List<Entity>();
            Transform = Transform.Zero;
        }

        public void Initialize()
        {
            foreach (var child in Childs) child.Initialize();
        }

        public void Start()
        {
            foreach (var child in Childs) child.Start();
            foreach (var component in Components) component.Start();
        }

        public void Update()
        {
            foreach (var child in Childs) child.Update();
            foreach (var component in Components) component.Update();
        }

        public void Render()
        {
            foreach (var child in Childs) child.Render();
            foreach (var component in Components) component.Render();
        }

        public void Stop()
        {
            foreach (var child in Childs) child.Stop();
        }

        public void Dispose()
        {
            
        }
        
        public void OnEnable()
        {
            foreach (var child in Childs) child.OnEnable();
        }

        public void OnDisable()
        {
            foreach (var child in Childs) child.OnDisable();
        }
        
        public void AddComponent<T>(T component) where T : ObjectBehaviour
        {
            Components.Add(component);
        }

        public T GetComponent<T>() where T : ObjectBehaviour
        {
            return (T)Components.Find(x => x.GetType() == typeof(T));
        }

        public void RemoveComponent<T>(T component) where T : ObjectBehaviour
        {
            Components.Remove(component);
        }

        public void SetActive(bool value) => IsActive = value;

        public bool Equals(Entity other)
        {
            return other.Transform == Transform && other.Childs == Childs 
                && other.Components == Components && other.IsActive == IsActive;
        }
    }
    
    public class EmptyEntity : Entity
    {
        public EmptyEntity(){}
    }
}