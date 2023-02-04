
using Emission.Graphics;
using Emission.Window;
using System;

namespace Emission.Page
{
    [Serializable]
    public class Page : IEngineBehaviour, IEquatable<Page>
    {
        public string Name;
        public string Path;
        public Actor Root;

        public IEngineBehaviour Behaviour => this;
        public string Uuid => _uuid.ToString();

        public bool IsActive { get; set; }

        protected ICamera Camera;
        protected Window.Window Window => GameInstance.Window;

        private Guid _uuid;

        public Page() 
        {
            _uuid = Guid.NewGuid();
        }

        public Page(string name, Actor root)
        {
            Name = name;
            Root = root;
            Camera = null;

            _uuid = Guid.NewGuid();

            Behaviour.BindBehaviour();
        }

        ~Page()
        {
            Name= null;
            Root = null;
            Camera = null;

            Behaviour.IsActive = false;
        }

        public void Enable()
        {
            IsActive = true;
            Root.OnEnable();
            OnEnable();
        }

        public void Disable()
        {
            IsActive = false;
            Root.OnDisable();
            OnDisable();
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

        public virtual void OnEnable()
        {
            if (Camera == null)
                throw new EmissionException(EmissionErrors.EmissionPageException,
                    "Camera does not exists in current context!");
        }

        public virtual void OnDisable()
        {

        }

        public bool Equals(Page other)
        {
            return other == null ? false : _uuid.ToString() == other._uuid.ToString();
        }

        public override int GetHashCode()
        {
            return _uuid.GetHashCode();
        }
    }
}
