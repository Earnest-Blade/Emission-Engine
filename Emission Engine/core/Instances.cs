using Emission.IO;
using Emission.Window;
using Emission.Graphics;

namespace Emission
{
    public static class Instances
    {
        public static Game Game => GameController.Instance;
        public static Window.Window Window => GameController.HasInstance ? Game.Window : null;
        public static Renderer Renderer => GameController.HasInstance ? Game.Renderer : null;
        public static Debug Debugger => GameController.HasInstance ? Game.Debugger : null;
        public static Data Data => GameController.HasInstance ? Game.Data : null;

        public static Input Input => Input.Instance;

        public static Event.EventDispatcher EventDispatcher => Event.EventDispatcher.Instance;
        public static EngineBehaviour.EngineBehaviourDispatcher EngineBehaviourDispatcher => EngineBehaviour.EngineBehaviourDispatcher.Instance;
    }
}
