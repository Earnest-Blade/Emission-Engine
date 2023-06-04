using System.Runtime.InteropServices;

using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe partial class Renderer
    {
        /// <summary>
        /// Enable a vertex array using the location of this array.
        /// Define an array of generic vertex attribute data using a the location, a stride and an offset.
        /// The type of data is define as float.
        /// </summary>
        /// <param name="location">Index of vertex array</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        public static void EnableVertexArrayAttrib(uint location, int stride, int offset)
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
        /// Overwrite data to an array buffer using VertexBufferObject.
        /// Use this to write vertices, texture coords or normals data.
        /// </summary>
        /// <param name="buffer">Buffer's ID</param>
        /// <param name="data">Data to write to the buffer.</param>
        public static void WriteBuffer(VertexBufferObject buffer, float[] data)
        {
            buffer.Bind();
            
            fixed (float* v = &data[0])
                buffer.PushData(sizeof(float) * data.Length, new IntPtr(v), GetDrawUsage());
        }

        /// <summary>
        /// Overwrite data to an array buffer using VertexBufferObject.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static void WriteBuffer<T>(VertexBufferObject buffer, T[] data)
        {
            buffer.Bind();
            
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            buffer.PushData(Marshal.SizeOf(default(T)) * data.Length, handle.AddrOfPinnedObject(), GetDrawUsage());
            handle.Free();
        }

        /// <summary>
        /// Overwrite data to an element array buffer using his ID.
        /// Use this to write indices.
        /// </summary>
        /// <param name="ebo"></param>
        /// <param name="data">Indices data to write to the buffer.</param>
        public static void WriteIndices(ElementBufferObject ebo, uint[] data)
        {
            ebo.Bind();
            
            fixed(uint* v = &data[0])
                ebo.PushData(sizeof(int) * data.Length, v, GL_DYNAMIC_DRAW);
        }

        /// <summary>
        /// Define, bind and generate a new Vertex Array at an ID.
        /// Add it to vertex array clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public static VertexArrayBuffer BindVertexArray() => _instance.VertexArray();

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
        public static VertexBufferObject BindBuffer(int location, float[] data, int stride, int offset) => _instance.Buffer(location, data, stride, offset);

        /// <summary>
        /// Load and create a new buffer using a size and data to bind.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VertexBufferObject BindBuffer(int size, IntPtr data) => _instance.Buffer(size, data);

        /// <summary>
        /// Load a 2D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind2DBuffer(int location, float[] data) => _instance.Buffer(location, data, 2, 0);
        
        /// <summary>
        /// Load a 3D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind3DBuffer(int location, float[] data) => _instance.Buffer(location, data, 3, 0);
        
        /// <summary>
        /// Load a 4D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind4DBuffer(int location, float[] data) => _instance.Buffer(location, data, 4, 0);

        /// <summary>
        /// Load a 8D dimensional buffer with float array as data. This buffer is designed
        /// to be used as a vertices array buffer to put vertices data into a buffer in order to draw it.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject BindVertexBuffer(int location, float[] data) => _instance.Buffer(location, data, STRIDE, 0);

        /// <summary>
        /// Load a <see cref="struct"/> array as buffer.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VertexBufferObject BindStructBuffer<T>(T[] data) where T : struct => _instance.BufferStruct(data);
        
        /// <summary>
        /// Load an element buffer with int array as data.
        /// Add buffer ID to element buffer list.
        /// </summary>
        /// <param name="data">Data to send width the buffer.</param>
        public static ElementBufferObject BindIndices(uint[] data) => _instance.Indices(data);

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="path">Image's path</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <returns></returns>
        public static uint BindTexture2D(string path, ref int width, ref int height) => _instance.Texture2D(path, ref width, ref height);

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="data">Pointer to image's data</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <returns></returns>
        public static uint BindTexture2D(IntPtr data, int width, int height) => _instance.Texture2D(data.ToPointer(), width, height);

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="data">Image's data as bytes</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <returns></returns>
        public static uint BindTexture2D(byte[] data, int width, int height) => _instance.Texture2D(data, width, height);

        /// <summary>
        /// Load a CubeMap texture buffer using an array of image's bytes.
        /// </summary>
        /// <param name="data">Image's data as bytes</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <returns></returns>
        public static uint BindTextureCubeMap(byte[] data, int width, int height, CubeMapTarget target)
        {
            fixed (byte* ptr = &data[0])
                return _instance.TextureCubeMap(ptr, width, height, (int)target);
        }
        
        /// <summary>
        /// Load a CubeMap texture buffer using a pointer from image's data.
        /// </summary>
        /// <param name="ptr">Image's data pointer</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <param name="target">Define OpenGl Cubemap target in TexImage</param>
        /// <returns></returns>
        public static uint BindTextureCubeMap(IntPtr ptr, int width, int height, CubeMapTarget target) => _instance.TextureCubeMap(ptr.ToPointer(), width, height, (int)target);
        
        /// <summary>
        /// Load a CubeMap texture buffer using an pointer.
        /// </summary>
        /// <param name="data">Image's data pointer</param>
        /// <param name="width">Image's width</param>
        /// <param name="height">Image's height</param>
        /// <returns></returns>
        public static uint BindTextureCubeMap(void* data, int width, int height, CubeMapTarget target) => _instance.TextureCubeMap(data, width, height, (int)target);

        /// <summary>
        /// Clear all buffers and clear buffers list.
        /// </summary>
        public static void Clear() => _instance.ClearAll();

        /// <summary>
        /// Clear buffers at specifics IDs, remove them from the list.
        /// </summary>
        /// <param name="vao">Vertex Array ID</param>
        /// <param name="vbo">Vertex Buffer ID</param>
        /// <param name="ebo">Element Buffer ID</param>
        public static void Clear(VertexArrayBuffer vao, VertexBufferObject vbo, ElementBufferObject ebo) => _instance.ClearId(vao, vbo, ebo);

        /// <summary>
        /// Remove a vertex array buffer from current loaded vao array.
        /// WARNING: This function does not unload the element !
        /// </summary>
        /// <param name="vao">element to remove</param>
        public static void RemoveVertexArray(VertexArrayBuffer vao)
        {
            if (HasInstance()) _instance.LoadedVao.Remove(vao);
        }
        
        /// <summary>
        /// Remove a vertex buffer object from current loaded vbo array.
        /// WARNING: This function does not unload the element !
        /// </summary>
        /// <param name="vbo">element to remove</param>
        public static void RemoveVertexBuffer(VertexBufferObject vbo)
        {
            if (HasInstance()) _instance.LoadedVbo.Remove(vbo);
        }
        
        /// <summary>
        /// Remove an element buffer object from current loaded ebo array.
        /// WARNING: This function does not unload the element !
        /// </summary>
        /// <param name="ebo">element to remove</param>
        public static void RemoveElementBuffer(ElementBufferObject ebo)
        {
            if (HasInstance()) _instance.LoadedEbo.Remove(ebo);
        }
        
        /// <summary>
        /// Remove a texture from current loaded tex array.
        /// WARNING: This function does not unload the element !
        /// </summary>
        /// <param name="texId">element to remove</param>
        public static void RemoveTextureId(uint texId)
        {
            if (HasInstance()) _instance.LoadedTid.Remove(texId);
        }

        /// <summary>
        /// Specifies the expected usage pattern of the renderer.
        /// The symbolic constant must be GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY
        /// </summary>
        public static void SetDrawUsage(uint usage)
        {
            if(usage < GL_STREAM_DRAW || usage > GL_DYNAMIC_COPY)
                throw new EmissionException(EmissionException.ERR_OPEN_GL, $"{usage} is not an available draw usage !");
            
            _instance._currentDrawUsage = usage;
        }

        /// <summary>
        /// Return the expected usage pattern of the renderer.
        /// </summary>
        public static uint GetDrawUsage() => _instance._currentDrawUsage;

        public static void SetRendererInstance(Renderer renderer) => _instance = renderer;
    }
}
