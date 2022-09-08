using System;
using System.Globalization;
using System.Collections.Generic;

using Emission.GFX;

namespace Emission.IO
{
    internal class OBJLoader
    {
        private List<float> _vertices;
        private List<float> _textureCoords;
        private List<float> _normals;
        private List<(int[] vertices, int[] textureCoords, int[] normals)> _faces;

        private string[] _lines;
        private string _path;
        
        public OBJLoader(string path)
        {
            _path = path;
            _lines = Resources.GetAllLines(path);
            _vertices = new List<float>();
            _textureCoords = new List<float>();
            _normals = new List<float>();
            _faces = new List<(int[] vertices, int[] textureCoords, int[] normals)>();
        }
        
        public Model Load()
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
        
        private Model ToModel()
        {
            Model model = new Model(_faces.Count);

            foreach(var face in _faces)
            {
                Vertex[] vertices = new Vertex[face.vertices.Length];
                for (int v = 0; v < vertices.Length; v++)
                {
                    vertices[v] = new Vertex(
                        _vertices[face.Item1[v] * 3 - 3],
                        _vertices[face.Item1[v] * 3 - 2],
                        _vertices[face.Item1[v] * 3 - 1],
                        _textureCoords[face.Item2[v] * 2 - 2],
                        _textureCoords[face.Item2[v] * 2 - 1],
                        _normals[face.Item3[v] * 3 - 3],
                        _normals[face.Item3[v] * 3 - 2],
                        _normals[face.Item3[v] * 3 - 1]
                    );
                }

                switch (face.vertices.Length)
                {
                    // Add indices for a new simple triangle face
                    case 3:
                        model.AddFace(vertices, Face.TRIANGLES_INDICES);
                        break;
                    
                    // Add indices for a quad face
                    case 4:
                        model.AddFace(vertices, Face.QUAD_INDICES);
                        break;
                }
            }
            
            return model;
        }
        
        private void Parse3DData(string[] data, ref List<float> list)
        {
            bool success = float.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
            success &= float.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float y);
            success &= float.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out float z);
            
            if (!success)
            {
                Debug.LogError("[ERROR] Cannot parse vertices : " + data);
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
                Debug.LogError("[ERROR] Cannot parse texture coordinate : " + data);
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
                
                if (!success && _textureCoords.Count != 0) 
                {
                    Debug.LogError("[ERROR] Cannot parse face : " + data);
                    return;
                }
                
                vindex[i - 1] = x;
                vtindex[i - 1] = y;
                vnindex[i - 1] = z;
            }
            
            _faces.Add((vindex, vtindex, vnindex));
        }
    }
}