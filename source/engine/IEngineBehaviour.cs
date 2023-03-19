namespace Emission
{
    public interface IEngineBehaviour
    {
        IEngineBehaviour Behaviour { get; }
        public bool IsActive { get; set; }

        public void Initialize();
        public void Start();
        public void Update();
        public void Render();
        public void Stop();

        public void BindBehaviour()
        {
            if (EngineBehaviour.HasEngineBehaviourDispatcher())
            {
                GameInstance.EngineBehaviourDispatcher.Attach(Behaviour);
            }
        }
    }

    
}
