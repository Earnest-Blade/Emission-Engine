using Emission.IO;

namespace Emission
{
    public static class Instances
    {
        public static Application Application => Application.GetCurrentApplication();
        public static Window Window => Application.HasCurrentInstance() ? Application.Window : null;
        public static Renderer Renderer => Application.HasCurrentInstance() ? Application.Renderer : null;
        public static Debug Debugger => Application.HasCurrentInstance() ? Application.Debugger : null;
        public static Data Data => Application.HasCurrentInstance() ? Application.Data : null;

        public static Event.EventDispatcher EventDispatcher => Event.EventDispatcher.Instance;
        public static EngineBehaviour.EngineBehaviourDispatcher EngineBehaviourDispatcher => EngineBehaviour.EngineBehaviourDispatcher.Instance;
    }
}
