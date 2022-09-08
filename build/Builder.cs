
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Emission.Build
{
    public class Builder : IDisposable
    {
        public StreamReader Reader;
        public StreamWriter Writer;
        
        private string _inputPath;
        private string _ouputPath;

        private List<string> _files;
        private List<Bundle> _bundles;

        public Builder()
        {
            _files = new List<string>();
            _bundles = new List<Bundle>();
        }

        public void Build()
        {
            Console.WriteLine("[INFO] Build Started! \t" + _inputPath + '\t' + _ouputPath);
            Directory.CreateDirectory(_ouputPath);
            Console.WriteLine($"[INFO] {_files.Count} has been loaded!");
            
            CreateBundles();
        }

        public void Write()
        {
            
        }

        public Builder CreateInput(string input)
        {
            _inputPath = input;
            foreach (var file in Directory.EnumerateFiles(_inputPath, "*", SearchOption.AllDirectories)) _files.Add(file);
            return this;
        }

        public Builder CreateOutput(string output)
        {
            _ouputPath = output;
            return this;
        }

        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }

        private void CreateBundles()
        {
            foreach (var file in _files)
            {
                if (_bundles.Count == 0 || _bundles[^1].IsFull)
                {
                    _bundles.Add(new Bundle(_bundles.Count, _ouputPath));
                }
                
                _bundles[^1].AddFile(new FileInfo(file), File.ReadAllText(file));
            }
        }
    }
}
