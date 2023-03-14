using Emission;
using Emission.IO;
using Emission.Natives.STB;
using Emission.Page;
using Emission.Window;

using Sandbox.RuntimePages;

namespace Sandbox
{
    static class Program
    {
        static void Main(string[] args)
        {
            GameController.Create(EngineSettings.FromJson(".settings"));
            GameController.CreateDebugger("Emission Console");
            GameController.CreateWindow(WindowConfig.FromJson(".window"));
            GameController.Initiate();

            PageManager.Enable(new BasicPlane());
            
            GameController.Start();
        }
    }
}
