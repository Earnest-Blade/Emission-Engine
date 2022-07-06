namespace Emission
{
    abstract class Scene : IEngineBehaviour
    {
        private static Scene _current;

        public string Name { get; }
        public IEngineBehaviour Behaviour {get => this;}
        
        protected Camera Camera;
        protected bool IsSceneEnable => _enable;

        private bool _enable;

        public Scene(string name)
        {
            Name = name;
            Behaviour.BindCallbacks();
            
            Camera = Camera.LoadFrom(Window.Current.Settings);
            _enable = false;
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
            if (_enable)
            {
                Camera.Update();
            
                OnUpdate();
            }
        }

        void IEngineBehaviour.PreRender()
        {
            
        }

        void IEngineBehaviour.Render()
        {
            if (_enable)
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

        public void Call()
        {
            SetCurrentScene(this);
            Camera.SetAsMain(Camera);

            _enable = true;
            OnStart();
        }

        public void Unload()
        {
            OnStop();
            _enable = false;
            
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