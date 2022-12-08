using System;

namespace Emission
{
    public abstract class ObjectBehaviour
    {
        public virtual void Initialize() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void Dispose() { }
    }
}
