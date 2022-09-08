using System.Threading;
using Emission.Math;

namespace Emission.Scene
{
    public abstract class Scene : IEngineBehaviour
    {
        private static Scene _current;

        public string Name { get; }
        public IEngineBehaviour Behaviour {get => this;}
        
        protected Camera Camera;
        protected bool Enable;

        public Scene(string name)
        {
            Name = name;
            
            Behaviour.BindCallbacks();

            Camera = Camera.LoadFrom(Window.Singleton.Settings);
            Enable = false;
        }

        protected abstract void OnInitialize();
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnRender();
        protected abstract void OnStop();

        void IEngineBehaviour.Initialize()
        {
            OnInitialize();
        }
        
        void IEngineBehaviour.Start()
        {
            
        }

        void IEngineBehaviour.Update()
        {
            if (Enable)
            {
                OnUpdate();
            }
        }

        void IEngineBehaviour.PreRender()
        {
            
        }

        void IEngineBehaviour.Render()
        {
            if (Enable)
            {
                OnRender();
            }
        }

        void IEngineBehaviour.PostRender()
        {
            
        }

        void IEngineBehaviour.Dispose()
        {
            OnStop();
        }

        public void Load()
        {
            if(Enable) return;
            Camera.SetAsMain(Camera);

            Enable = true;

            SetCurrentScene(this);
            OnStart();
        }

        public void Unload()
        {            
            if(!Enable) return;
            Enable = false;
            
            OnStop();
            
            Behaviour.UnbindCallbacks();
            
            SetCurrentScene(null);
        }

        private static void SetCurrentScene(Scene scene)
        {
            _current = scene;
        }
        
        public static Scene Current { get => _current; }
    }
}