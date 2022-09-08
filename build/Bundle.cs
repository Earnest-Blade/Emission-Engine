
using System;
using System.IO;

namespace Emission.Build
{
    public struct BundleFile
    {
        public string Name;
        public string Path;
        public string Data;

        public BundleFile(string name, string path, string data)
        {
            Name = name;
            Path = path;
            Data = data;
        }
    }
    
    public class Bundle
    {
        public const int BUNDLE_BOUNDS = int.MaxValue;
        public const int MAX_LINE_CONTENT = 0x1F4;
        
        public BundleFile[] Files = new BundleFile[BUNDLE_BOUNDS];
        
        public string FullPath => _path + _id + ".bundle";
        public bool IsFull => _fileCounts == BUNDLE_BOUNDS;

        private int _id;
        private string _path;
        private int _fileCounts;
        private long _pointerPosition;
        private long[,] _pointers = new long[BUNDLE_BOUNDS, 2];

        public Bundle(int id, string path)
        {
            _id = id;
            _path = path;
            _pointerPosition = 0;
            _fileCounts = 0;
        }

        public void AddFile(FileInfo file, string data)
        {
            if (!IsFull)
            {
                Files[_pointerPosition] = new BundleFile(file.Name, file.DirectoryName, data);
                _fileCounts++;
                
                // update pointers positions
                _pointers[_pointerPosition, 0] = _pointerPosition;
                _pointerPosition += Files[_pointerPosition].Data.Length / MAX_LINE_CONTENT;
                _pointers[_pointerPosition, 1] = _pointerPosition;
                
                Console.WriteLine(_pointerPosition);
            }
        }
    }
}