using System.Runtime.InteropServices;
using Emission.Graphics.RenderConfig;
using Emission.IO;
using Emission.Mathematics;

namespace Emission.Window
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowConfig
    {
        public int Width { get; set; } 
        public int Height { get; set; } 
        public string Title { get; set; } 
        public string Icon { get; set; }

        public bool IsFocused { get; set; } 
        public bool IsFloating { get; set; } 
        public bool IsMaximized { get; set; } 
        public bool IsDecorated { get; set; } 
        public bool IsResizable { get; set; } 
        public bool IsVisible { get; set; } 
        public bool IsCursorCentered { get; set; } 
        public bool IsFocusedOnShow { get; set; } 

        public string Version { get; set; } 
        public int MinorVersion { get; set; } 
        public int MajorVersion { get; set; } 

        public int DepthBits { get; set; } 
        public int StencilBits { get; set; }

        public RenderConfig RenderConfig { get; set; }

        public static WindowConfig Default(string name) => Default(name, 960 * 2, 540 * 2);
        public static WindowConfig Default(string name, int width, int height)
        {
            return new WindowConfig()
            {
                Width = width,
                Height = height,
                Title = name,
                Icon = null,
                
                IsFloating = true,
                IsFocused = false,
                IsMaximized = false,
                IsDecorated = true,
                IsResizable = true,
                IsVisible = false,
                IsCursorCentered = false,
                IsFocusedOnShow = true,
                
                Version = "0.0.1",
                MinorVersion = 3,
                MajorVersion = 4,
                
                DepthBits = 24,
                StencilBits = 8,
                
                RenderConfig = new GlConfig().GetDefault()
            };
        }

        public static WindowConfig FromJson(string path)
        {
            return Json.Deserialize<WindowConfig>(path);
        }
    }
}
