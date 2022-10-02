using System;
using Emission;
using Emission.IO;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assets.Compress();

            //return;
            Application.CreateApplication();
            Application.CreateWindow("Window");
            Application.InitializeApplication();
            
            Console.WriteLine(File.ReadAllData("Assets/demo.shader"));
            //Application.StartApplication();
        }
    }
}
