using Emission;
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

            // Enable startup scene
            //Page pg = new MainPage();
            //PageManager.Enable(pg);

            PageManager.Enable(new BasicPlane());
            
            GameController.Start();
        }
    }
}
