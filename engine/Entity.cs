using System;
using Emission.GFX;
using Emission.Math;

namespace Emission
{
    public class Entity<T> : IInternalEngineBehavior, IDisposable where T : Mesh, IEngineRenderer
    {
        public int ID { get; }
        public string Name { get; }
        
        public Transform Transform;

        public T Mesh
        {
            get => _mesh;
            set
            { 
                if (_mesh != null)
                {
                    _mesh.LoadGeometry(value.Faces);
                    _mesh.Initialize();
                }
                else _mesh = value;
            }
        }

        public bool IsActive
        {
            get => _isActive && _mesh != null;
            set => _isActive = value;
        }

        private T _mesh;
        private bool _isActive;
        
        public Entity()
        {
            Transform = Transform.Zero;
            _mesh = null;
            
            _isActive = true;
        }

        public void Initialize()
        {
            _mesh.Initialize();
        }

        public void Update()
        {
            if (this)
            {
                _mesh.Update(ref Transform);
            }
        }

        public void Render()
        {
            if (this)
            {
                _mesh.Render();
            }
        }

        public void Dispose()
        {
            _mesh.Dispose();
        }

        public static implicit operator bool(Entity<T> e)
        {
            return e.IsActive;
        }
    }
}