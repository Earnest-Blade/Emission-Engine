using System;
using System.Runtime.InteropServices;
using Emission.IO;

namespace Emission
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EngineSettings
    {
        internal const string DEFAULT_DIRECTORY = "DEFAULT";
        
        public short Version { get; set; }
        public string VersionAsStr { get; set; }
        public int Framerate { get; set; }
        
        public bool Debug { get; set; }

        public string Directory { get; set; }

        public static EngineSettings GetDefault()
        {
            return new EngineSettings()
            {
                Version = 0,
                VersionAsStr = "Development Ver.",
                Framerate = 60,
                Debug = true,
                Directory = "DEFAULT"
            };
        }

        public static implicit operator string(EngineSettings engineSettings)
        {
            return $"Emission Engine - {engineSettings.VersionAsStr}";
        }
        
        public static EngineSettings FromJson(string path)
        {
            return Json.Deserialize<EngineSettings>(path);
        }

        public override string ToString() => (string)this;
    }
}
