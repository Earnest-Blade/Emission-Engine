using System;
using System.Collections.Generic;
using System.Globalization;

namespace Emission.MultiMeshLoader
{
    internal class WavefontLoader
    {
        private List<float> _vertices;
        private List<float> _textureCoords;
        private List<Tuple<int[], int[], int[]>> _faces;

        private string[] _lines;
        
        public WavefontLoader(string path)
        {
            _lines = Resources.GetAllLines(path);
            _vertices = new List<float>();
            _textureCoords = new List<float>();
            _faces = new List<Tuple<int[], int[], int[]>>();
        }

        public (float[], int[]) Load()
        {
            foreach (string line in _lines)
            {
                string[] sliced = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (sliced.Length > 0)
                {
                    switch (sliced[0])
                    {
                        case "v":
                            ParseVertex(sliced);
                            break;
                        case "vt":
                            ParseTextureCoordonates(sliced);
                            break;
                        case "f":
                            ParseFaces(sliced);
                            break;
                    }
                }
            }

            return ToModel();
        }
        
        private (float[], int[]) ToModel()
        {
            List<float> vertices = new List<float>();
            List<int> indices = new List<int>();
            
            for (int i = 0; i < _vertices.Count; i+=3)
            {
                // Load vertices
                vertices.AddRange(new []{_vertices[i], _vertices[i + 1], _vertices[i + 2], 0.0f, 0.0f});
            }

            foreach (var face in _faces)
            {
                if (face.Item1.Length == 3)
                {
                    // Load triangles indices
                    indices.AddRange(new []{face.Item1[0]-1, face.Item1[1]-1, face.Item1[2]-1}); // 0, 1, 2
                }
                if (face.Item1.Length == 4)
                {
                    // Load quads indices
                    indices.AddRange(new []
                    {
                        face.Item1[0]-1, face.Item1[1]-1, face.Item1[3]-1, // 0, 1, 3
                        face.Item1[1]-1, face.Item1[2]-1, face.Item1[3]-1 // 1, 2, 3
                    });
                }
            }
            
            return (vertices.ToArray(), indices.ToArray());
        }
        
        private void ParseVertex(string[] data)
        {
            bool success = float.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
            success &= float.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float y);
            success &= float.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out float z);
            
            if (!success)
            {
                Console.WriteLine("[ERROR] Cannot parse vertices : " + data);
                return;
            }
            
            _vertices.AddRange(new []{x,y,z});
        }

        private void ParseTextureCoordonates(string[] data)
        {
            bool success = float.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
            success &= float.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float y);

            if (!success) 
            {
                Console.WriteLine("[ERROR] Cannot parse texture coordinate : " + data);
                return;
            }
            
            _textureCoords.AddRange(new []{x,y});
        }

        private void ParseFaces(string[] data)
        {
            int[] vindex = new int[data.Length - 1];
            int[] vtindex = new int[data.Length - 1];
            int[] vnindex = new int[data.Length - 1];
            
            for (int i = 1; i < data.Length; i++)
            {
                string[] parse = data[i].Split('/');
                bool success = int.TryParse(parse[0], NumberStyles.Any, CultureInfo.InvariantCulture, out int x);
                success &= int.TryParse(parse[1], NumberStyles.Any, CultureInfo.InvariantCulture, out int y);
                success &= int.TryParse(parse[2], NumberStyles.Any, CultureInfo.InvariantCulture, out int z);
                
                if (!success) 
                {
                    Console.WriteLine("[ERROR] Cannot parse face : " + data);
                    return;
                }

                vindex[i - 1] = x;
                vtindex[i - 1] = y;
                vtindex[i - 1] = z;
            }
            
            _faces.Add(new Tuple<int[], int[], int[]>(vindex, vtindex, vnindex));
        }
    }
}