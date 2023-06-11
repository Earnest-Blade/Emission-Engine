using Emission.Core;
using Emission.Engine;
using Emission.Engine.Page;

namespace Emission.Projects.Indev
{
    class Program : Game
    {
        public Program(ApplicationContext context) : base(context, "../")
        {
            
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            PageManager.Enable(new IndevPage());
        }

        public static void Main(string[] args)
        {
            ApplicationContext ctx = new ApplicationContext
            {
                Name = "Emission.Projects.Indev",
                Framerate = 60,
                VersionMajor = 0,
                VersionMinor = 0,
                Patch = 1,
                IsDebug = true,
                DebugFlags = DebugFlags.ShowAll
            };

            Program p = new Program(ctx);
            Application.Create(p);
            Application.Start();
        }
    }
}

