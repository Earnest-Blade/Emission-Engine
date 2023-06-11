using Emission.Core;

namespace Emission.Engine
{
    public interface IEngineBehaviour
    {
        IEngineBehaviour Behaviour { get; }
        public bool IsActive { get; set; }

        public void Initialize();
        public void Start();
        public void Update(float delta);
        public void Render();
        public void Stop(int status);

        public void BindBehaviour()
        {
            if (Behaviour == null)
                throw new ArgumentException("Trying to bind a null behaviour!", nameof(Behaviour));
            
            if (Application.HasInstance() && Event.HasEventDispatcher())
            {
                Event.AddDelegate(Event.INITIALIZE, Initialize);
                Event.AddDelegate(Event.START, Start);
                Event.AddDelegate<float>(Event.UPDATE, Update);
                Event.AddDelegate(Event.RENDER, Render);
                Event.AddDelegate<int>(Event.STOP, Stop);
            }
            else Debug.LogWarning("[WARNING] Cannot bind behaviour because there is no behaviour dispatcher instance !");
        }
    }

    
}
