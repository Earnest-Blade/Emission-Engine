using System;
using System.Collections.Generic;
using System.Linq;
using Emission.Math;
using OpenTK.Mathematics;

namespace Emission
{
    public class MeshLoader
    {
        public static Mesh Load(string path)
        {
            string fileText = Resources.ReadFile(path).Replace('.', ',');

            List<float> vertices = new List<float>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

            foreach (string line in fileText.Split('\n'))
            {
                if (line.StartsWith("v "))
                {
                    String temp = line.Substring(2);

                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a vertex
                    {
                        String[] vertparts = temp.Split(' ');
 
                        // Attempt to parse each part of the vertice
                        bool success = float.TryParse(vertparts[0], out float x);
                        success &= float.TryParse(vertparts[1], out float y);
                        success &= float.TryParse(vertparts[2], out float z);
                        
                        // Dummy color/texture coordinates for now
                        //colors.Add(new Vector3((float) Mathf.Sin(vec.Z), (float) Mathf.Sin(vec.Z), (float) Mathf.Sin(vec.Z)));
                        //textCoords.Add(new Vector2((float) Mathf.Sin(vec.Z), (float) Mathf.Sin(vec.Z)));
                        
                        vertices.AddRange(new float[]{x, y, z, Mathf.Sin(z), Mathf.Sin(z)});
 
                        // If any of the parses failed, report the error
                        if (!success)
                        {
                            ApplicationConsole.PrintError("[ERROR] While parsing vertex at '" + line + "'");
                            Application.Current.Stop(-1);
                        }
                    }
                }
                
                else if (line.StartsWith("f ")) // Face definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);
 
                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);
 
                    if (temp.Count((char c) => c == ' ') == 2) // Check if there's enough elements for a face
                    {
                        String[] faceparts = temp.Split(' ');
 
                        int i1, i2, i3;
 
                        // Attempt to parse each part of the face
                        bool success = int.TryParse(faceparts[0], out i1);
                        success &= int.TryParse(faceparts[1], out i2);
                        success &= int.TryParse(faceparts[2], out i3);
 
                        // If any of the parses failed, report the error
                        if (!success)
                        {
                            ApplicationConsole.PrintError("[ERROR] While parsing face at '" + line + "'");
                        }
                        else
                        {
                            // Decrement to get zero-based vertex numbers
                            face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                            faces.Add(face);
                        }
                    }
                }
            }

            return new Mesh(vertices.ToArray(), LoadIndices(faces));
        }

        private static int[] LoadIndices(List<Tuple<int, int, int>> faces, int offset = 0)
        {
            List<int> indices = new List<int>();
            
            foreach (var face in faces)
            {
                indices.Add(face.Item1 + offset);
                indices.Add(face.Item2 + offset);
                indices.Add(face.Item3 + offset);
            }
 
            return indices.ToArray();
        }
    }
}