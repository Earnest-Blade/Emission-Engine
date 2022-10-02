using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Emission.IO
{
    public class Bundle : IDisposable
    {
        public const char FILE_SEPARATOR_CHAR = '/';
        public const char FILE_INFO_SEPARATOR_CHAR = '?';

        public string Path => _name;
        public string FullPath => _directory.FullName;

        private string _name;
        private List<BundleFile> _files;
        private DirectoryInfo _directory;

        private ulong _pointer;
        private string _bytes;

        public Bundle() { }
        public Bundle(string path)
        {
            _files = new List<BundleFile>();
            _name = path;
            _directory = new DirectoryInfo(Directory.GetCurrentDirectory() + "/" + path);
            _pointer = 0;
            _bytes = "";

            foreach (FileInfo file in _directory.EnumerateFiles())
                AddFile(file);
        }

        public void AddFile(string file) => AddFile(new FileInfo(file));
        public void AddFile(FileInfo info)
        {
            string fileCtnt = File.ReadAllText(info.FullName);
            _files.Add(new BundleFile(
                info.Name,
                _pointer, (uint)fileCtnt.Length
            ));

            _bytes += fileCtnt;
            _pointer += (ulong)fileCtnt.Length;
        }

        public void Save() => Save(_name);
        public void Save(string fileName)
        {
            string header = _files.Aggregate(_name + FILE_SEPARATOR_CHAR, (current, f) => current + (f.FileName + FILE_INFO_SEPARATOR_CHAR + f.Pointer.start + FILE_INFO_SEPARATOR_CHAR + f.Length + FILE_SEPARATOR_CHAR));
            header += '\n';
            
            FileStream fs = System.IO.File.Create(File.DATA_FILE + fileName + ".bundle");
            
            fs.Write(Encoding.UTF8.GetBytes(header));
            fs.Write(LzwCompressor.Compress(_bytes));
            
            fs.Close();
        }

        public void Open(string path)
        { 
            if (!File.Exists(path))
                throw new EmissionException(EmissionException.EmissionIoException, $"'{path}' Does not Exists!");
            
            string[] header = File.ReadLine(path, 0).Split(FILE_SEPARATOR_CHAR);
            string content = LzwCompressor.DecompressStr(System.IO.File.ReadAllBytes(path).Skip(string.Join("",header).Length + header.Length).ToArray());
            _name = header[0];
            _bytes = content;
            _directory = new DirectoryInfo(_name);
            _pointer = (ulong)_bytes.Length;
            _files = new List<BundleFile>();

            foreach (var t in header)
            {
                string[] info = t.Split(FILE_INFO_SEPARATOR_CHAR);
                if(info.Length != 3) continue;
                
                _files.Add(new BundleFile(
                    info[0],
                    ulong.Parse(info[1]),
                    uint.Parse(info[2])
                ));
            }
        }

        public BundleFile GetFile(string filename)
        {
            return _files.First(x => x.FileName == filename);
        }

        public string ReadFile(string name) => ReadFile(GetFile(name));
        public string ReadFile(BundleFile file)
        {
            return _bytes.Substring((int)file.Pointer.start, (int)file.Length);
        }

        public void Dispose()
        {
            _files.Clear();
            _name = null;
            _directory = null;
            _pointer = 0;
            _bytes = "";
        }
        
        public static Bundle Load(string path)
        {
            Bundle bundle = new Bundle();
            bundle.Open(path);
            return bundle;
        }
    }
    
    public struct BundleFile
    {
        public string FileName => _fileName;
        public uint Length => _size;
        public (ulong start, ulong end) Pointer => (_startPointer, _startPointer + _size);
        
        private string _fileName;
        private ulong _startPointer;
        private uint _size;

        public BundleFile(string fileName, ulong startPointer, uint size)
        {
            _fileName = fileName;
            _startPointer = startPointer;
            _size = size;
        }
    } 
}
