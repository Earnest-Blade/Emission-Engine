using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Emission.Core
{
    public sealed class Application
    {
        public static IApplication? Instance => _instance;

        private static IApplication? _instance;
        private static readonly object InstanceLock = new object();

        public static T Create<T>(ApplicationContext context) where T : IApplication
        {
            if(HasInstance())
                Debug.LogWarning("[WARNING] A new Application Instance has been created!");

            lock (InstanceLock)
            {
                if (typeof(T).IsAbstract)
                {
                    throw new EmissionException(new MissingMethodException(typeof(T).Name, "Constructor"));
                }
                
                _instance = (T)Activator.CreateInstance(typeof(T))!;
                _instance.Context = context;
            }

            return (T)_instance;
        }

        public static void Create(IApplication application, ApplicationContext context)
        {
            if(HasInstance())
                Debug.LogWarning("[WARNING] A new Application Instance has been created!");

            lock (InstanceLock)
            {
                _instance = application;
                _instance.Context = context;
            }
        }
        
        public static void Create(IApplication? application)
        {
            if(HasInstance())
                Debug.LogWarning("[WARNING] A new Application Instance has been created!");
            
            lock (InstanceLock)
            {
                _instance = application;
            }
        }

        public static void Start()
        {
            Debug.Log(HasInstance());
            if (HasInstance())
            {
                _instance?.Initialize();
                _instance?.Start();
            }
        }

        [DoesNotReturn]
        public static void Stop(int code)
        {
            if (HasInstance())
            {
                _instance?.Exit(code);
            }
        }

        public static bool HasInstance() => Instance != null;
        public static bool IsRunning() => HasInstance() && _instance!.IsRunning;
        public static T GetInstanceAs<T>() => ((T)Application.Instance!);
    }
}
