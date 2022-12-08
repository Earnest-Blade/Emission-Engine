using Emission;
using Emission.IO;
using Emission.Graphics;
using Emission.Mathematics;
using Emission.Graphics.Shading;
using Emission.UI;

using Sandbox.Scripts;
using Emission.Page;
using Emission.Window;
using Sandbox.RuntimePages;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDirectory.SetCurrentDirectory(@"*\\C#\\Emission Engine");
            
            GameController.Create();
            GameController.CreateDebugger("Emission Console");
            GameController.CreateWindow(WindowParameters.FromJson("window.json"));
            GameController.Initiate();

            // Enable startup scene
            Page pg = new CustomPage();
            PageManager.Enable(pg);

            GameController.Start();
        }
    }
}
