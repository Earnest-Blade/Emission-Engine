using System.Collections.Generic;
using Emission;
using Emission.IO;
using static Emission.Graphics.GL.GL;

namespace Emission.Graphics
{
    public unsafe class Renderer
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

        private List<uint> _loadedVao;
        private List<uint> _loadedVbo;
        private List<uint> _loadedEbo;
        private List<uint> _loadedTid;

        public Renderer()
        {
            _loadedVao = new List<uint>();
            _loadedVbo = new List<uint>();
            _loadedEbo = new List<uint>();
            _loadedTid = new List<uint>();
        }

        /// <summary>
        /// Define, bind and generate a new Vertex Array and return it ID.
        /// Add it vertex array id to clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public uint VertexArray()
        {
            uint vao = glGenVertexArray();
            glBindVertexArray(vao);

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
        public uint Buffer(int location, float[] data, int stride, int offset)
        {
            uint vbo = glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            fixed (float* v = &data[0])
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * data.Length, v, GL_STATIC_DRAW);
            

            _loadedVbo.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load element buffer with a int array as data.
        /// Add element id to <see cref="Loaded_EBO"/> and return element id.
        /// </summary>
        /// <param name="data">Data to store.</param>
        /// <returns>Element Buffer ID</returns>
        public uint Indices(int[] data)
        {
            uint ebo = glGenBuffer();
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
            
            fixed(int* v = &data[0])
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(int) * data.Length, v, GL_STATIC_DRAW);
            
            _loadedEbo.Add(ebo);
            return ebo;
        }

        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(string path, ref int width, ref int height,  int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE)
        {
            Sprite sprite = new Sprite(path);
            byte[] data = sprite.Bytes;
            width = sprite.Width;
            height = sprite.Height;
            return Texture2D(data, sprite.Width, sprite.Height);
        }
        
        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(byte[] data, int width, int height, uint location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE)
        {
            uint tid = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, tid);
            
            // texture coords binding
            EnableVertexArray(location, stride, 3);
            if (data != null)
            {
                fixed(byte* d = &data[0])
                    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, d);
                glGenerateMipmap(GL_TEXTURE_2D);
                
                // Define attributes
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
                
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            }
            else
            {
                // cannot load texture
                throw new EmissionException(Errors.EmissionTextureException, "Cannot load Texture2D");
            }
            
            glActiveTexture(GL_TEXTURE_2D);
            
            _loadedTid.Add(tid);
            return tid;
        }

        /// <summary>
        /// Clear and delete buffer with a specific index.
        /// </summary>
        /// <param name="vao">Vao ID to delete</param>
        /// <param name="vbo">Vbo ID to delete</param>
        /// <param name="ebo">Ebo ID to delete</param>
        public void ClearId(uint vao, uint vbo, uint ebo)
        {
            glDeleteVertexArray(vao);
            glDeleteBuffer(vbo);
            glDeleteBuffer(ebo);
            _loadedVao.Remove(vao);
            _loadedVbo.Remove(vbo);
            _loadedEbo.Remove(ebo);
        }

        /// <summary>
        /// Clear and delete all Vaos, Vbos and Ebos.
        /// </summary>
        public void ClearAll()
        {
            foreach (uint vao in _loadedVao) glDeleteVertexArray(vao);
            foreach (uint vbo in _loadedVbo) glDeleteBuffer(vbo);
            foreach (uint ebo in _loadedEbo) glDeleteBuffer(ebo);
            foreach (uint tid in _loadedTid) glDeleteTexture(tid);
            
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
        public static void EnableVertexArray(uint location, int stride, int offset)
        {
            glEnableVertexAttribArray(location);
            glVertexAttribPointer(location, 3, GL_FLOAT, false, stride * sizeof(float), (void*)(offset * sizeof(float)));
        }

        /// <summary>
        /// Unbind current vertex array.
        /// </summary>
        public static void DisableVertexArray()
        {
            glBindVertexArray(0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
        }

        /// <summary>
        /// Overwrite data to an array buffer using his ID.
        /// Use this to write vertices, texture coords or normals data.
        /// </summary>
        /// <param name="buffer">Buffer's ID</param>
        /// <param name="data">Data to write to the buffer.</param>
        public static void WriteBuffer(uint buffer, float[] data)
        {
            glBindBuffer(GL_ARRAY_BUFFER, buffer);
            fixed (float* v = &data[0])
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * data.Length, v, GL_DYNAMIC_DRAW);
        }
        
        /// <summary>
        /// Overwrite data to an element array buffer using his ID.
        /// Use this to write indices.
        /// </summary>
        /// <param name="buffer">Buffer's ID</param>
        /// <param name="data">Indices data to write to the buffer.</param>
        public static void WriteIndices(uint ebo, int[] data)
        {
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
            fixed(int* v = &data[0])
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(int) * data.Length, v, GL_DYNAMIC_DRAW);
        }
        
        #endregion
        
        #region Static Overload

        /// <summary>
        /// Define, bind and generate a new Vertex Array at an ID.
        /// Add it to vertex array clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public static uint BindVertexArray() => Instances.Renderer.VertexArray();

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
        public static uint BindBuffer(int location, float[] data, int stride, int offset) => Instances.Renderer.Buffer(location, data, stride, offset);

        /// <summary>
        /// Load a 2D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static uint Bind2DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 2, 0);
        
        /// <summary>
        /// Load a 3D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static uint Bind3DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 3, 0);
        
        /// <summary>
        /// Load a 4D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static uint Bind4DBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, 4, 0);

        /// <summary>
        /// Load a 8D dimensional buffer with float array as data. This buffer is designed
        /// to be used as a vertices array buffer to put vertices data into a buffer in order to draw it.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static uint BindVertexBuffer(int location, float[] data) => Instances.Renderer.Buffer(location, data, STRIDE, 0);

        /// <summary>
        /// Load an element buffer with int array as data.
        /// Add buffer ID to element buffer list.
        /// </summary>
        /// <param name="data">Data to send width the buffer.</param>
        public static uint BindIndices(int[] data) => Instances.Renderer.Indices(data);

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
        public static uint BindTexture2D(string path, ref int width, ref int height, int location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE) 
            => Instances.Renderer.Texture2D(path, ref width, ref height, location, stride);

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
        public static uint BindTexture2D(byte[] data, int width, int height, uint location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE)
            => Instances.Renderer.Texture2D(data, width, height, location, stride);

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
        public static void Clear(uint vao, uint vbo, uint ebo) => Instances.Renderer.ClearId(vao, vbo, ebo);

        #endregion
    }
}
