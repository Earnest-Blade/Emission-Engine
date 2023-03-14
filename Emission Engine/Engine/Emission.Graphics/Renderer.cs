using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emission;
using Emission.IO;
using Emission.Mathematics;
using Emission.Natives.GL;
using static Emission.Natives.GL.Gl;

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
        public static bool HasInstance() => GameInstance.Renderer != null;

        public RenderConfig.RenderConfig RenderConfig;

        private List<VertexArrayBuffer> _loadedVao;
        private List<VertexBufferObject> _loadedVbo;
        private List<ElementBufferObject> _loadedEbo;
        private List<uint> _loadedTid;

        private uint _currentDrawUsage;

        public Renderer(RenderConfig.RenderConfig config)
        {
            RenderConfig = config;
            
            _loadedVao = new List<VertexArrayBuffer>();
            _loadedVbo = new List<VertexBufferObject>();
            _loadedEbo = new List<ElementBufferObject>();
            _loadedTid = new List<uint>();

            _currentDrawUsage = GL_STATIC_DRAW;
        }

        /// <summary>
        /// Initialize renderer's functionalities and opengl.
        /// </summary>
        public void Initialize()
        {
            glEnable(GL_BLEND);
            glEnable(GL_DEPTH_TEST);
            glEnable(GL_TEXTURE_2D);
            
            glDepthFunc(GL_LESS);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

            Vector2 size = GameInstance.Window.WindowSize;
            glViewport(0, 0, (int)size.X, (int)size.Y);

            // Enable OpenGL debug output
            if (GameInstance.EngineSettings.Debug)
            {
                glEnable(GL_DEBUG_OUTPUT);
                glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);
                glDebugMessageCallback(GlMessage.MessageCallback, NULL);
                
                Debug.Log("[INFO] Enable OpenGL Debug Callback");
            }
            
            Debug.Log($"[INFO] Using OpenGL {GlLoader.Version}");
            Debug.Log($"[INFO] Using GLSL {GlUtils.PtrToStringUTF8(glGetString(GL_SHADING_LANGUAGE_VERSION))}");
            Debug.Log($"[INFO] Running with OpenGL Vendor {GlUtils.PtrToStringUTF8(glGetString(GL_VENDOR))}");
            Debug.Log($"[INFO] Running with OpenGL Renderer {GlUtils.PtrToStringUTF8(glGetString(GL_RENDERER))}");

            GameInstance.Window.ClearColor = RenderConfig.ClearColor;
        }

        /// <summary>
        /// Define, bind and generate a new Vertex Array and return it ID.
        /// Add it vertex array id to clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public VertexArrayBuffer VertexArray()
        {
            VertexArrayBuffer vao = new VertexArrayBuffer();
            vao.Bind();

            _loadedVao.Add(vao);
            return vao;
        }

        /// <summary>
        /// Load and Create a new buffer.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public VertexBufferObject Buffer(int size, IntPtr data)
        {
            VertexBufferObject vbo = new VertexBufferObject();
            vbo.Bind();
            vbo.PushData(size, data, _currentDrawUsage);

            _loadedVbo.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load a dimensional buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// Add buffer ID the <see cref="_loadedVbo"/>.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        /// <param name="stride">Stride of data attribute</param>
        /// <param name="offset">Offset of data attribute</param>
        /// <returns></returns>
        public VertexBufferObject Buffer(int location, float[] data, int stride, int offset)
        {
            VertexBufferObject vbo = new VertexBufferObject();
            vbo.Bind();

            fixed (float* v = &data[0])
                vbo.PushData(sizeof(float) * data.Length, new IntPtr(v), _currentDrawUsage);
            
            _loadedVbo.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load a buffer based on a struct.
        /// </summary>
        /// <param name="data">Struct data to push to the buffer.</param>
        /// <typeparam name="T">Struct type to push to the buffer.</typeparam>
        /// <returns></returns>
        public VertexBufferObject BufferStruct<T>(T[] data) where T : struct
        {
            VertexBufferObject buffer = new VertexBufferObject();
            buffer.Bind();

            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            buffer.PushData(Marshal.SizeOf(default(T)) * data.Length, handle.AddrOfPinnedObject(), _currentDrawUsage);
            handle.Free();
            
            _loadedVbo.Add(buffer);
            return buffer;
        }

        /// <summary>
        /// Load element buffer with a int array as data.
        /// Add element id to <see cref="Loaded_EBO"/> and return element id.
        /// </summary>
        /// <param name="data">Data to store.</param>
        /// <returns>Element Buffer ID</returns>
        public ElementBufferObject Indices(uint[] data)
        {
            ElementBufferObject ebo = new ElementBufferObject();
            ebo.Bind();
            
            fixed(uint* v = &data[0])
                ebo.PushData(sizeof(uint) * data.Length, v, _currentDrawUsage);
            
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
            fixed(byte* v = &data[0]) 
                return Texture2D(v, width, height, location, stride); 
        }

        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(void* data, int width, int height, uint location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE)
        {
            uint tid;
            glGenTextures(1, &tid);
            glBindTexture(GL_TEXTURE_2D, tid);
            
            // Define attributes
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
                
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            
            // texture coords binding
            //EnableVertexArrayAttrib(location, stride, 3);
            if (data != null)
            {
                glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
                glGenerateMipmap(GL_TEXTURE_2D);
            }
            else
            {
                // cannot load texture
                throw new EmissionException(EmissionErrors.EmissionOpenGlException, "Cannot load Texture2D");
            }

            glBindTexture(GL_TEXTURE_2D, 0);
            
            _loadedTid.Add(tid);
            return tid;
        }

        /// <summary>
        /// Clear and delete buffer with a specific index.
        /// </summary>
        /// <param name="vao">Vao ID to delete</param>
        /// <param name="vbo">Vbo ID to delete</param>
        /// <param name="ebo">Ebo ID to delete</param>
        public void ClearId(VertexArrayBuffer vao, VertexBufferObject vbo, ElementBufferObject ebo)
        {
            vao.Delete();
            vbo.Delete();
            ebo.Delete();
            _loadedVao.Remove(vao);
            _loadedVbo.Remove(vbo);
            _loadedEbo.Remove(ebo);
        }

        /// <summary>
        /// Clear and delete all Vaos, Vbos and Ebos.
        /// </summary>
        public void ClearAll()
        {
            foreach (VertexArrayBuffer vao in _loadedVao) vao.Delete();
            foreach (VertexBufferObject vbo in _loadedVbo) vbo.Delete();
            foreach (ElementBufferObject ebo in _loadedEbo) ebo.Delete();
            foreach (uint tid in _loadedTid) glDeleteTextures(1, &tid);
            
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
        
        #endregion
        
        #region Static Overload

        /// <summary>
        /// Define, bind and generate a new Vertex Array at an ID.
        /// Add it to vertex array clearing list.
        /// </summary>
        /// <returns>Vertex Array ID</returns>
        public static VertexArrayBuffer BindVertexArray() => GameInstance.Renderer.VertexArray();

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
        public static VertexBufferObject BindBuffer(int location, float[] data, int stride, int offset) => GameInstance.Renderer.Buffer(location, data, stride, offset);

        /// <summary>
        /// Load and create a new buffer using a size and data to bind.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VertexBufferObject BindBuffer(int size, IntPtr data) => GameInstance.Renderer.Buffer(size, data);

        /// <summary>
        /// Load a 2D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind2DBuffer(int location, float[] data) => GameInstance.Renderer.Buffer(location, data, 2, 0);
        
        /// <summary>
        /// Load a 3D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind3DBuffer(int location, float[] data) => GameInstance.Renderer.Buffer(location, data, 3, 0);
        
        /// <summary>
        /// Load a 4D dimensional buffer with float array as data.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject Bind4DBuffer(int location, float[] data) => GameInstance.Renderer.Buffer(location, data, 4, 0);

        /// <summary>
        /// Load a 8D dimensional buffer with float array as data. This buffer is designed
        /// to be used as a vertices array buffer to put vertices data into a buffer in order to draw it.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <param name="location">Location of the buffer.</param>
        /// <param name="data">Data to send width the buffer.</param>
        public static VertexBufferObject BindVertexBuffer(int location, float[] data) => GameInstance.Renderer.Buffer(location, data, STRIDE, 0);

        /// <summary>
        /// Load a <see cref="struct"/> array as buffer.
        /// Add buffer ID to buffer list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static VertexBufferObject BindStructBuffer<T>(T[] data) where T : struct => GameInstance.Renderer.BufferStruct(data);
        
        /// <summary>
        /// Load an element buffer with int array as data.
        /// Add buffer ID to element buffer list.
        /// </summary>
        /// <param name="data">Data to send width the buffer.</param>
        public static ElementBufferObject BindIndices(uint[] data) => GameInstance.Renderer.Indices(data);

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
            => GameInstance.Renderer.Texture2D(path, ref width, ref height, location, stride);

        /// <summary>
        /// Load a 2 dimensional texture buffer with an image load with a path.
        /// The location use in the shader and the stride of vertex data can be change but by default it's <see cref="SHADER_TEXTURE_COORDS_LOCATION"/>
        /// and <see cref="STRIDE"/>.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// </summary>
        /// <param name="data">Pointer to image's data</param>
        /// <param name="location">Location in shader layout</param>
        /// <param name="stride">Stride in vertex array</param>
        /// <returns></returns>
        public static uint BindTexture2D(IntPtr data, int width, int height, uint location = SHADER_TEXTURE_COORDS_LOCATION, int stride = STRIDE)
            => GameInstance.Renderer.Texture2D(data.ToPointer(), width, height, location, stride);

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
            => GameInstance.Renderer.Texture2D(data, width, height, location, stride);

        /// <summary>
        /// Clear all buffers and clear buffers list.
        /// </summary>
        public static void Clear() => GameInstance.Renderer.ClearAll();

        /// <summary>
        /// Clear buffers at specifics IDs, remove them from the list.
        /// </summary>
        /// <param name="vao">Vertex Array ID</param>
        /// <param name="vbo">Vertex Buffer ID</param>
        /// <param name="ebo">Element Buffer ID</param>
        public static void Clear(VertexArrayBuffer vao, VertexBufferObject vbo, ElementBufferObject ebo) => GameInstance.Renderer.ClearId(vao, vbo, ebo);

        /// <summary>
        /// Specifies the expected usage pattern of the renderer.
        /// The symbolic constant must be GL_STREAM_DRAW, GL_STREAM_READ, GL_STREAM_COPY, GL_STATIC_DRAW, GL_STATIC_READ, GL_STATIC_COPY, GL_DYNAMIC_DRAW, GL_DYNAMIC_READ, or GL_DYNAMIC_COPY
        /// </summary>
        public static void SetDrawUsage(uint usage)
        {
            // TODO: Change the way that it check if it's a valid value.
            if (usage != GL_STREAM_DRAW && usage != GL_STREAM_COPY && usage != GL_STREAM_READ &&
                usage != GL_STATIC_DRAW && usage != GL_STATIC_READ && usage != GL_STATIC_COPY &&
                usage != GL_DYNAMIC_DRAW && usage != GL_DYNAMIC_READ && usage != GL_DYNAMIC_COPY)
                throw new EmissionException(EmissionErrors.EmissionOpenGlException,
                    $"{usage} is not an available draw usage !");
            
            GameInstance.Renderer._currentDrawUsage = usage;
        }

        /// <summary>
        /// Return the expected usage pattern of the renderer.
        /// </summary>
        public static uint GetDrawUsage() => GameInstance.Renderer._currentDrawUsage;

        #endregion
    }
}
