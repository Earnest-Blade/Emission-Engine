using Emission.Core;

namespace Emission.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LonelyActor : Attribute
    {
        public static bool IsActorLonely(Type type)
        {
            if (type.GetCustomAttributes(true).Contains(typeof(LonelyActor)))
            {
                Debug.LogWarning($"[WARNING] Type class '{type.Name}' cannot use parent or child system because he's considered as a Lonely Actor!");
                return true;
            }

            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DisableEngineBehaviorOnActor : Attribute
    {
        public static bool IsActorDisabled(Type actor)
        {
            return actor.GetCustomAttributes(true).Contains(typeof(DisableEngineBehaviorOnActor));
        }
    }
}