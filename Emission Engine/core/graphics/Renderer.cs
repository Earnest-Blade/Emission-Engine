using System;
using System.Collections.Generic;
using Emission.IO;
using OpenTK.Graphics.OpenGL;

namespace Emission
{
    public class Renderer
    {
        /// <summary>
        /// Constant integer that represent texture coords location in shader layout.
        /// </summary>
        public const int SHADER_TEXTURE_COORDS_LOCATION = 1;
        
        /// <summary>
        /// Number of value in each vertex array.
        /// </summary>
        public const int STRIDE = 8;
        
        /// <summary>
        /// Check if a current instance of a Renderer already exists.
        /// </summary>
        public static bool HasInstance() => Instances.Renderer != null;

        private List<int> _loadedVao;
        private List<int> _loadedVbo;
        private List<int> _loadedEbo;
        private List<int> _loadedTid;

        public Renderer()
        {
            _loadedVao = new List<int>();
            _loadedVbo = new List<int>();
            _loadedEbo = new List<int>();
            _loadedTid = new List<int>();
        }

        /// <summary>
        /// Define, bind and generate a new Vertex Array and return it ID.
        /// Add it vertex array id to clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public int VertexArray()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            _loadedVao.Add(vao);
            return vao;
        }

        /// <summary>
        /// Load a dimensional buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// Add buffer ID the <see cref="Loaded_VBO"/>.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        /// <returns></returns>
        public int Buffer(int location, float[] data, int stride, int offset)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);

            _loadedVbo.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load element buffer with a int array as data.
        /// Add element id to <see cref="Loaded_EBO"/> and return element id.
        /// </summary>
        /// <param name="data">Data to store.</param>
        /// <returns>Element Buffer ID</returns>
        public int Indices(int[] data)
        {
            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * data.Length, data, BufferUsageHint.StaticDraw);
            
            _loadedEbo.Add(ebo);
            return ebo;
        }

        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public int Texture2D(string path, ref int width, ref int height,  int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE, PixelFormat format = PixelFormat.Rgb)
        {
            Image image = new Image(path);
            byte[] data = image.Bytes;
            width = image.Width;
            height = image.Height;
            return Texture2D(data, image.Width, image.Height);
        }
        
        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public int Texture2D(byte[] data, int width, int height, int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE, PixelFormat format = PixelFormat.Rgb)
        {
            int tid = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tid);
            
            // texture coords binding
            EnableVertexArray(location, stride, 3);
            if (data != null)
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, format, PixelType.UnsignedByte, data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.GenerateTextureMipmap(tid);
                
                // Define attributes
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                // cannot load texture
                throw new EmissionException(EmissionException.EmissionTextureException, "Cannot load Texture2D");
            }
            
            GL.ActiveTexture(TextureUnit.Texture0);
            
            _loadedTid.Add(tid);
            return tid;
        }

        /// <summary>
        /// Clear and delete buffer with a specific index.
        /// </summary>
        /// <param name="vao">Vao ID to delete</param>
        /// <param name="vbo">Vbo ID to delete</param>
        /// <param name="ebo">Ebo ID to delete</param>
        public void ClearId(int vao, int vbo, int ebo)
        {
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            _loadedVao.Remove(vao);
            _loadedVbo.Remove(vbo);
            _loadedEbo.Remove(ebo);
        }

        /// <summary>
        /// Clear and delete all Vaos, Vbos and Ebos.
        /// </summary>
        public void ClearAll()
        {
            foreach (int vao in _loadedVao) GL.DeleteVertexArray(vao);
            foreach (int vbo in _loadedVbo) GL.DeleteBuffer(vbo);
            foreach (int ebo in _loadedEbo) GL.DeleteBuffer(ebo);
            foreach (int tid in _loadedTid) GL.DeleteTexture(tid);
            
            _loadedVao.Clear();
            _loadedVbo.Clear();
            _loadedEbo.Clear();
            _loadedTid.Clear();
        }

        #region Static Methods
        /// <summary>
        /// Enable a vertex array using the location of this array.
        /// Define an array of generic vertex attribute data using a the location, a stride and an offset.
        /// The type of data is define as float.
        /// </summary>
        /// <param name="location">Index of vertex array</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        public static void EnableVertexArray(int location, int stride, int offset)
        {
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, 3, VertexAttribPointerType.Float, false, stride * sizeof(float), offset * sizeof(float));
        }

        /// <summary>
        /// Unbind current vertex array.
        /// </summary>
        public static void DisableVertexArray()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Overwrite data to an array buffer using his ID.
        /// Use this to write vertices, texture coords or normals data.
        /// </summary>
        /// <param name="buffer">Buffer's ID</param>
        /// <param name="data">Data to write to the buffer.</param>
        public static void WriteBuffer(int buffer, float[] data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.DynamicDraw);
        }
        
        /// <summary>
        /// Overwrite data to an element array buffer using his ID.
        /// Use this to write indices.
        /// </summary>
        /// <param name="buffer">Buffer's ID</param>
        /// <param name="data">Indices data to write to the buffer.</param>
        public static void WriteIndices(int ebo, int[] data)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * data.Length, data, BufferUsageHint.DynamicDraw);
        }
        
        #endregion
        
        #region Static Overload

        /// <summary>
        /// Define, bind and generate a new Vertex Array at an ID.
        /// Add it to vertex array clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public static int BindVertexArray() => Instances.Renderer.VertexArray();

        /// <summary>
        /// Load a generic buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        /// <returns></returns>
        public static int BindBuffer(int location, float[] data, int stride, int offset) 
            => Instances.Renderer.Buffer(location, data, stride, offset);

        /// <summary>
        /// Load a 2D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static int Bind2DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 2, 0);
        
        
        /// <summary>
        /// Load a 3D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static int Bind3DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 3, 0);
        
        
        /// <summary>
        /// Load a 4D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static int Bind4DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 4, 0);

        /// <summary>
        /// Load a 8D dimensional buffer with float array as data. This buffer is designed
        /// to be used as a vertices array buffer to put vertices data into a buffer in order to draw it.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static int BindVertexBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, STRIDE, 0);

        /// <summary>
        /// Load an element buffer with int array as data.
        /// Add buffer ID to element buffer list.
        /// </summary>
        /// <param name="data">Data to send width the buffer.</param>
        public static int BindIndices(int[] data) => Instances.Renderer.Indices(data);

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="path">Image's path</param>
        /// <param name="location">Location in shader layout</param>
        /// <param name="stride">Stride in vertex array</param>
        /// <returns></returns>
        public static int BindTexture2D(string path, ref int width, ref int height,
            int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE, PixelFormat format = PixelFormat.Rgb) 
            => Instances.Renderer.Texture2D(path, ref width, ref height, location, stride, format);
        

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="data">Image's data as bytes</param>
        /// <param name="location">Location in shader layout</param>
        /// <param name="stride">Stride in vertex array</param>
        /// <returns></returns>
        public static int BindTexture2D(byte[] data, int width, int height,
            int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE, PixelFormat format = PixelFormat.Rgb)
            => Instances.Renderer.Texture2D(data, width, height, location, stride, format);

        /// <summary>
        /// Clear all buffers and clear buffers list.
        /// </summary>
        public static void Clear() => Instances.Renderer.ClearAll();

        /// <summary>
        /// Clear buffers at specifics IDs, remove them from the list.
        /// </summary>
        /// <param name="vao">Vertex Array ID</param>
        /// <param name="vbo">Vertex Buffer ID</param>
        /// <param name="ebo">Element Buffer ID</param>
        public static void Clear(int vao, int vbo, int ebo) => Instances.Renderer.ClearId(vao, vbo, ebo);

        #endregion
    }
}
