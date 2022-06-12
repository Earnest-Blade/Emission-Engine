using System;
using System.Drawing.Imaging;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

namespace Emission
{
    static class GraphicAllocator
    {
        public static readonly List<int> Loaded_VAO = new List<int>();
        public static readonly List<int> Loaded_VBO = new List<int>();
        public static readonly List<int> Loaded_EBO = new List<int>();
        public static readonly List<int> Loaded_TID = new List<int>();

        public static unsafe int BindVertexArray(float[] vertices)
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            Loaded_VAO.Add(vao);
            return vao;
        }

        public static unsafe int Bind3DBuffer(int location, float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(location, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            Loaded_VBO.Add(vbo);
            return vbo;
        }

        public static unsafe int Bind2DBuffer(int location, float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            Loaded_VBO.Add(vbo);
            return vbo;
        }

        public static unsafe int BindIndices(int[] data)
        {
            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * data.Length, data, BufferUsageHint.StaticDraw);
            
            Loaded_EBO.Add(ebo);
            return ebo;
        }

        public static unsafe int BindTexture2D(string path)
        {
            int tid = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tid);

            GL.EnableVertexAttribArray(tid);
            GL.VertexAttribPointer(tid, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            int width, height;
            BitmapData data = Resources.ReadTexture(path, &width, &height);
            if (data != null)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, 
                                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else
            {
                ApplicationConsole.PrintError("[ERROR] Cannot load Texture from '" + path + "'!");
            }

            Loaded_TID.Add(tid);
            return tid;
        }

        public static void Clear()
        {
            foreach (int vao in Loaded_VAO) GL.DeleteVertexArray(vao);
            foreach (int vbo in Loaded_VBO) GL.DeleteBuffer(vbo);
            foreach (int ebo in Loaded_EBO) GL.DeleteBuffer(ebo);
            foreach (int tid in Loaded_TID) GL.DeleteTexture(tid);
        }

        public static void Clear(int vao, int vbo, int ebo, int tid)
        {
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteTexture(tid);
        }
    }
}
