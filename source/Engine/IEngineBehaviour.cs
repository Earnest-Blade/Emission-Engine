using Emission.Core;

namespace Emission.Engine
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
                ((Game)Application.Instance!).BehaviourDispatcher.Attach(Behaviour);
            }
            else Debug.LogWarning("[WARNING] Cannot bind behaviour because there is no behaviour dispatcher instance !");
        }
    }

    
}
