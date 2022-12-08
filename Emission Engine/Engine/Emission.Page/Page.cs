
using Emission.Graphics;
using System;

namespace Emission.Page
{
    [Serializable]
    public class Page : IEngineBehaviour
    {
        public string Name;
        public string Path;
        public Entity Root;

        public IEngineBehaviour Behaviour => this;
        public bool IsActive { get; set; }

        protected ICamera PageCamera;

        public Page() { }

        public Page(string name, Entity root)
        {
            Name = name;
            Root = root;
            PageCamera = null;
            
            Behaviour.BindBehaviour();
        }

        public void Enable()
        {
            IsActive = true;
            Root.OnEnable();
        }

        public void Disable()
        {
            IsActive = false;
            Root.OnDisable();
        }
        
        public virtual void Initialize()
        {
            Root.Initialize();
        }

        public virtual void Start()
        {
            Root.Start();
        }

        public virtual void Update()
        {
            Root.Update();
        }

        public virtual void Render()
        {
            Root.Render();
        }

        public virtual void Stop()
        {
            Disable();
            Root.Stop();
        }
    }
}
