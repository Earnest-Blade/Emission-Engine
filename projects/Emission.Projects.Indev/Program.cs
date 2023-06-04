using System;
using Emission.Core;
using Emission.Core.IO;
using Emission.Engine;
using Emission.Engine.Page;
using Emission.Graphics.UI;

namespace Emission.Projects.Indev
{
    class Program : Game
    {
        public Program(ApplicationContext context) : base(context, "../")
        {
            Debug.Log(EDirectory.GetCurrentDirectory());
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            PageManager.Enable(new IndevPage());
        }

        public override void Exit(int status)
        {
            base.Exit(status);
        }

        public static void Main(String[] args)
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

