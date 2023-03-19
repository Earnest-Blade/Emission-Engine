using System;
using System.Runtime.InteropServices;
using Emission.IO;

namespace Emission
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EngineSettings
    {
        internal const string DEFAULT_DIRECTORY = "DEFAULT";

        public readonly short Version { get; private init; }
        public readonly int Framerate { get; private init; }
        public readonly bool Debug { get; private init; }
        public readonly string Directory { get; private init; }
        
        public string VersionAsStr
        {
            get
            {
                if (!string.IsNullOrEmpty(_versionAsStr)) return _versionAsStr;
                if (Version == 0) return "Development Ver.";
                return Version + "v";
            }
            private set => _versionAsStr = value;
        }
        
        private string? _versionAsStr;

        public EngineSettings(short version, int framerate, bool debug, string directory)
        {
            Version = version;
            Framerate = framerate;
            Debug = debug;
            Directory = directory;
            
            _versionAsStr = null;
        }

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
        
        public static EngineSettings FromJson(string? path)
        {
            return Json.Deserialize<EngineSettings>(path);
        }

        public override string ToString() => (string)this;
    }
}
