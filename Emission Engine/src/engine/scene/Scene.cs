namespace Emission
{
    abstract class Scene
    {
        private static Scene _current;

        public string Name { get; }
        
        protected Camera _camera;

        private bool _enable;

        public Scene(string name)
        {
            Name = name;
            _camera = Camera.LoadFrom(Window.Current.Settings);
            
            Initialize();
        }

        protected abstract void OnInitialize();
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnRender();
        protected abstract void OnStop();

        private void Initialize()
        {
            OnInitialize();
        }

        public void Update()
        {
            _camera.Update();
            
            OnUpdate();
        }

        public void Render()
        {
            OnRender();
        }

        public void Call()
        {
            Scene.SetCurrentScene(this);
            Camera.SetAsMain(_camera);
            
            this.OnStart();
        }

        public void Unload()
        {
            this.OnStop();
            
            SetCurrentScene(null);
        }

        private static void SetCurrentScene(Scene scene)
        {
            _current = scene;
        }
        
        public static Scene Current { get => _current; }
    }
}