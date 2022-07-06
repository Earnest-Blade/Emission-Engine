using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

namespace Emission
{
    static class GraphicAllocator
    {
        /// <summary>
        /// Constant integer that represent position's location in shader layout.
        /// </summary>
        public const int SHADER_POSITION_LOCATION = 0;
        
        /// <summary>
        /// Constant integer that represent texture coords location in shader layout.
        /// </summary>
        public const int SHADER_TEXTURE_COORDS_LOCATION = 1;

        public const int STRIDE = 8;

        /// <summary>
        /// Return a tuple with values of all buffer index.
        /// <see cref="Loaded_VAO"/>,
        /// <see cref="Loaded_VBO"/>,
        /// <see cref="Loaded_EBO"/>,
        /// <see cref="Loaded_TID"/>.
        /// Is readonly.
        /// </summary>
        public static (int[], int[], int[], int[]) LoadedBuffers
        {
            get => (Loaded_VAO.ToArray(), Loaded_VBO.ToArray(), Loaded_EBO.ToArray(), Loaded_TID.ToArray());
        }
        
        // Loaded buffers lists.
        private static readonly List<int> Loaded_VAO = new List<int>();
        private static readonly List<int> Loaded_VBO = new List<int>();
        private static readonly List<int> Loaded_EBO = new List<int>();
        private static readonly List<int> Loaded_TID = new List<int>();

        /// <summary>
        /// Define, bind and generate a new Vertex Array at an ID.
        /// Add it to <see cref="Loaded_VAO"/>. 
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public static int BindVertexArray()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            Loaded_VAO.Add(vao);
            return vao;
        }
        
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
        public static void UnbindVertexArray()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        
        /// <summary>
        /// Load a 2 dimensional buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location.
        /// Add buffer ID the <see cref="Loaded_VBO"/>.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        /// <returns>Buffer ID</returns>
        public static int Bind2DBuffer(int location, float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, 2, VertexAttribPointerType.Float, false, 0, 0);
            
            Loaded_VBO.Add(vbo);
            return vbo;
        }
        
        
        /// <summary>
        /// Load a 3 dimensional buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// Add buffer ID the <see cref="Loaded_VBO"/>.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        /// <returns></returns>
        public static int Bind3DBuffer(int location, float[] data, int stride = STRIDE, int offset = 0)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            
            EnableVertexArray(location, stride, offset);
            
            Loaded_VBO.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load element buffer with a int array as data.
        /// Add element id to <see cref="Loaded_EBO"/> and return element id.
        /// </summary>
        /// <param name="data">Data to store.</param>
        /// <returns>Element Buffer ID</returns>
        public static int BindIndices(int[] data)
        {
            int ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * data.Length, data, BufferUsageHint.StaticDraw);
            
            Loaded_EBO.Add(ebo);
            return ebo;
        }

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
        public static unsafe int BindTexture2D(string path, int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE, PixelFormat format = PixelFormat.Rgb)
        {
            int tid = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tid);
            
            // texture coords binding
            EnableVertexArray(location, stride, 3);
            
            int width, height;
            byte[] data = Resources.ReadImageBytes(path, &width, &height);
            if (data != null)
            {
                // Define attributes
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, format, PixelType.UnsignedByte, data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else
            {
                // cannot load texture
                Debug.LogError("[ERROR] Cannot load Texture from '" + path + "'!");
                return 0;
            }
            
            Loaded_TID.Add(tid);
            return tid;
        }

        /// <summary>
        /// Clear and delete buffer with a specific index.
        /// </summary>
        /// <param name="vao">Vao ID to delete</param>
        /// <param name="vbo">Vbo ID to delete</param>
        /// <param name="ebo">Ebo ID to delete</param>
        public static void Clear(int vao, int vbo, int ebo)
        {
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
        }
        
        /// <summary>
        /// Clear and delete all Vaos, Vbos and Ebos.
        /// </summary>
        public static void ClearAll()
        {
            foreach (int vao in Loaded_VAO) GL.DeleteVertexArray(vao);
            foreach (int vbo in Loaded_VBO) GL.DeleteBuffer(vbo);
            foreach (int ebo in Loaded_EBO) GL.DeleteBuffer(ebo);
            foreach (int tid in Loaded_TID) GL.DeleteTexture(tid);
        }
    }
}
