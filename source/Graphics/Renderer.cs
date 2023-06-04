using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emission.Core;
using Emission.Assets;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe partial class Renderer
    {
        /// <summary>
        /// Number of value in each vertex array.
        /// </summary>
        public const int STRIDE = 8;
        
        /// <summary>
        /// Check if a current instance of a Renderer already exists.
        /// </summary>
        public static bool HasInstance() => Application.HasInstance() && _instance != null;

        private static Renderer? _instance;
        
        public RenderConfig.RenderConfig RenderConfig;

        internal readonly List<VertexArrayBuffer> LoadedVao;
        internal readonly List<VertexBufferObject> LoadedVbo;
        internal readonly List<ElementBufferObject> LoadedEbo;
        internal readonly List<uint> LoadedTid;

        private uint _currentDrawUsage;

        public Renderer(RenderConfig.RenderConfig config)
        {
            RenderConfig = config;
            
            LoadedVao = new List<VertexArrayBuffer>();
            LoadedVbo = new List<VertexBufferObject>();
            LoadedEbo = new List<ElementBufferObject>();
            LoadedTid = new List<uint>();

            _currentDrawUsage = GL_STATIC_DRAW;
        }

        /// <summary>
        /// Initialize renderer's functionalities and opengl.
        /// </summary>
        public void Initialize()
        {
            RenderConfig.InitializeRenderer();
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

            LoadedVao.Add(vao);
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

            LoadedVbo.Add(vbo);
            return vbo;
        }

        /// <summary>
        /// Load a dimensional buffer with float array as data.
        /// Bind data and buffer then Define an array of generic vertex attribute data using the the location, a stride and an offset.
        /// Add buffer ID the <see cref="LoadedVbo"/>.
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
            
            LoadedVbo.Add(vbo);
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
            
            LoadedVbo.Add(buffer);
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
            
            LoadedEbo.Add(ebo);
            return ebo;
        }

        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(string path, ref int width, ref int height)
        {
            int _width, _height;
            byte[] data = Image.LoadImageFromPath(path, &_width, &_height);

            width = _width;
            height = _height;
            
            return Texture2D(data, _width, _height);
        }
        
        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(byte[] data, int width, int height)
        {
            fixed(byte* v = &data[0]) 
                return Texture2D(v, width, height); 
        }

        /// <summary>
        /// Load 2D texture. See static for more information.
        /// </summary>
        /// <returns></returns>
        public uint Texture2D(void* data, int width, int height)
        {
            uint tid;
            glGenTextures(1, &tid);
            glBindTexture(GL_TEXTURE_2D, tid);
            
            // Define attributes
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);	
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
                
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            
            if (data != NULL)
            {
                glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
                glGenerateMipmap(GL_TEXTURE_2D);
            }
            else
            {
                // cannot load texture
                throw new EmissionException(EmissionException.ERR_TEXTURE, "Cannot load Texture2D");
            }

            glBindTexture(GL_TEXTURE_2D, 0);
            
            LoadedTid.Add(tid);
            
            return tid;
        }

        /// <summary>
        /// Load a cubemap texture. See static for more informations
        /// </summary>
        /// <returns></returns>
        public uint TextureCubeMap(void* data, int width, int height, int target)
        {
            uint tid;
            glGenTextures(1, &tid);
            glBindTexture(GL_TEXTURE_CUBE_MAP, tid);
            
            glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
            glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            
            glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
            glTexParameteri(GL_TEXTURE_CUBE_MAP, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);

            if (data != NULL)
            {
                glTexImage2D(target, 0, GL_RGBA, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
            }
            else
            {
                throw new EmissionException(EmissionException.ERR_TEXTURE, "Cannot load Cubemap");
            }

            glBindTexture(GL_TEXTURE_CUBE_MAP, 0);
            
            LoadedTid.Add(tid);

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
            LoadedVao.Remove(vao);
            LoadedVbo.Remove(vbo);
            LoadedEbo.Remove(ebo);
        }

        /// <summary>
        /// Clear and delete all Vaos, Vbos and Ebos.
        /// </summary>
        public void ClearAll()
        {
            foreach (VertexArrayBuffer vao in LoadedVao) vao.Delete();
            foreach (VertexBufferObject vbo in LoadedVbo) vbo.Delete();
            foreach (ElementBufferObject ebo in LoadedEbo) ebo.Delete();
            foreach (uint tid in LoadedTid) glDeleteTextures(1, &tid);
            
            LoadedVao.Clear();
            LoadedVbo.Clear();
            LoadedEbo.Clear();
            LoadedTid.Clear();
        }
    }
}
