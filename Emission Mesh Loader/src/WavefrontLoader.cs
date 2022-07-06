using System;
using System.Collections.Generic;
using System.Globalization;

namespace Emission.MultiMeshLoader
{
    internal class WavefontLoader
    {
        private List<float> _vertices;
        private List<float> _textureCoords;
        private List<float> _normals;
        private List<Tuple<int[], int[], int[]>> _faces;

        private string[] _lines;
        
        public WavefontLoader(string path)
        {
            _lines = Resources.GetAllLines(path);
            _vertices = new List<float>();
            _textureCoords = new List<float>();
            _normals = new List<float>();
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
                            Parse3DData(sliced, ref _vertices);
                            break;
                        case "vt":
                            Parse2DData(sliced, ref _textureCoords);
                            break;
                        case "vn":
                            Parse3DData(sliced, ref _normals);
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

            foreach (var face in _faces)
            {
                if (face.Item1.Length == 3)
                {
                    for (int v = 0; v < face.Item1.Length; v++)
                    {
                        vertices.AddRange(new []
                        {
                            _vertices[face.Item1[v] - 1],
                            _vertices[face.Item1[v]],
                            _vertices[face.Item1[v] + 1],
                            _textureCoords[face.Item2[v]],
                            _textureCoords[face.Item2[v] + 1],
                            _normals[face.Item3[v] - 1],
                            _normals[face.Item3[v]],
                            _normals[face.Item3[v] + 1],
                        });
                    }
                }
                else if (face.Item1.Length == 4)
                {
                    float[,] vert = new float[4, 8];
                    for (int v = 0; v < face.Item1.Length; v++)
                    {
                        vert[v, 0] = _vertices[face.Item1[v] * 3 - 3];      // x
                        vert[v, 1] = _vertices[face.Item1[v] * 3 - 2];      // y
                        vert[v, 2] = _vertices[face.Item1[v] * 3 - 1];      // z
                        vert[v, 3] = _textureCoords[face.Item2[v] * 2 - 2]; // tx
                        vert[v, 4] = _textureCoords[face.Item2[v] * 2 - 1]; // ty
                        vert[v, 5] = _normals[face.Item3[v] * 3 - 3];       // nx
                        vert[v, 6] = _normals[face.Item3[v] * 3 -2];        // ny
                        vert[v, 7] = _normals[face.Item3[v] * 3 - 1];       // nz
                    }
                    
                    vertices.AddRange(new []
                    {
                        vert[0, 0], vert[0, 1], vert[0, 2], vert[0, 3], vert[0, 4], vert[0, 5], vert[0, 6], vert[0, 7],
                        vert[1, 0], vert[1, 1], vert[1, 2], vert[1, 3], vert[1, 4], vert[1, 5], vert[1, 6], vert[1, 7],
                        vert[3, 0], vert[3, 1], vert[3, 2], vert[3, 3], vert[3, 4], vert[3, 5], vert[3, 6], vert[3, 7],
                        
                        vert[1, 0], vert[1, 1], vert[1, 2], vert[1, 3], vert[1, 4], vert[1, 5], vert[1, 6], vert[1, 7],
                        vert[2, 0], vert[2, 1], vert[2, 2], vert[2, 3], vert[2, 4], vert[2, 5], vert[2, 6], vert[2, 7],
                        vert[3, 0], vert[3, 1], vert[3, 2], vert[3, 3], vert[3, 4], vert[3, 5], vert[3, 6], vert[3, 7],
                    });
                }
            }
            
            // generate indices
            int[] indices = new int[(vertices.Count) / 8 + 1];
            for (int i = 0; i < (vertices.Count) / 8 + 1; i++) indices[i] = i;
            
            return (vertices.ToArray(), indices);
        }
        
        private void Parse3DData(string[] data, ref List<float> list)
        {
            bool success = float.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
            success &= float.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float y);
            success &= float.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out float z);
            
            if (!success)
            {
                Console.WriteLine("[ERROR] Cannot parse vertices : " + data);
                return;
            }
            
            list.AddRange(new []{x,y,z});
        }

        private void Parse2DData(string[] data, ref List<float> list)
        {
            bool success = float.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
            success &= float.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float y);

            if (!success) 
            {
                Console.WriteLine("[ERROR] Cannot parse texture coordinate : " + data);
                return;
            }
            
            list.AddRange(new []{x,y});
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
                vnindex[i - 1] = z;
            }
            
            _faces.Add(new Tuple<int[], int[], int[]>(vindex, vtindex, vnindex));
        }
    }
}