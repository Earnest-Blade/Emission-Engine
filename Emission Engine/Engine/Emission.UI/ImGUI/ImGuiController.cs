using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Emission.Graphics;
using ImGuiNET;

using Emission.Mathematics;
using Emission.Graphics.Shading;
using Emission.Natives.GL;
using static Emission.Natives.GL.Gl;

namespace Emission.UI
{
    public unsafe class ImGuiController : IDisposable
    {
        private bool _frameBegun;

        private VertexArrayBuffer _vertexArray;
        private VertexBufferObject _vertexBuffer;
        private int _vertexBufferSize;
        private ElementBufferObject _indexBuffer;
        private int _indexBufferSize;

        private uint _fontTexture;

        private Shader _shader;
        
        private readonly List<char> _pressedChars = new List<char>();

        private static bool KHRDebugAvailable = false;

        public ImGuiController()
        {
            int major, minor;
            glGetIntegerv(GL_MINOR_VERSION, &minor);
            glGetIntegerv(GL_MAJOR_VERSION, &major);
            
            KHRDebugAvailable = (major == 4 && minor >= 3) || IsExtensionSupported("KHR_debug");
            
            IntPtr ctx = ImGui.CreateContext();
            ImGui.SetCurrentContext(ctx);

            ImGuiIOPtr io = ImGui.GetIO();

            /* Loading fonts */
            io.Fonts.AddFontFromFileTTF("C:/Windows/Fonts/BRLNSR.TTF", 25f);

            io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;
            io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;

            CreateDeviceResources();
            SetKeyMappings();

            SetPerFrameImGuiData(1f / 60f);

            ImGui.NewFrame();
            _frameBegun = true;
        }

        public void CreateDeviceResources()
        {
            _vertexBufferSize = 10000;
            _indexBufferSize = 2000;

            _vertexArray = new VertexArrayBuffer();
            _vertexArray.Bind();
            LabelObject(GL_VERTEX_ARRAY, _vertexArray, "ImGui");

            _vertexBuffer = new VertexBufferObject();
            _vertexBuffer.Bind();
            LabelObject(GL_BUFFER, _vertexBuffer, "VBO: ImGui");
            _vertexBuffer.PushData(_vertexBufferSize, IntPtr.Zero, GL_DYNAMIC_DRAW);

            _indexBuffer = new ElementBufferObject();
            _indexBuffer.Bind();
            LabelObject(GL_BUFFER, _indexBuffer, "EBO: ImGui");
            _indexBuffer.PushData(_indexBufferSize, IntPtr.Zero, GL_DYNAMIC_DRAW);

            RecreateFontDeviceTexture();

            string VertexSource = @"#version 330 core
                                    uniform mat4 projection_matrix;
                                    layout(location = 0) in vec2 in_position;
                                    layout(location = 1) in vec2 in_texCoord;
                                    layout(location = 2) in vec4 in_color;
                                    out vec4 color;
                                    out vec2 texCoord;
                                    void main()
                                    {
                                        gl_Position = projection_matrix * vec4(in_position, 0, 1);
                                        color = in_color;
                                        texCoord = in_texCoord;
                                    }";

            string FragmentSource = @"#version 330 core
                                    uniform sampler2D in_fontTexture;
                                    in vec4 color;
                                    in vec2 texCoord;
                                    out vec4 outputColor;
                                    void main()
                                    {
                                        //texture(in_fontTexture, texCoord)
                                        outputColor = vec4(1, 1, 1, 1);
                                    }";

            _shader = new Shader(new ShaderLoader.ShaderStruct(VertexSource, FragmentSource), "ImGUIShader");

            int stride = Unsafe.SizeOf<ImDrawVert>();
            glVertexAttribPointer(0, 2, GL_FLOAT, false, stride, (void*)0);
            glVertexAttribPointer(1, 2, GL_FLOAT, false, stride, (void*)8);
            glVertexAttribPointer(2, 4, GL_UNSIGNED_BYTE, true, stride, (void*)16);

            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
            glEnableVertexAttribArray(2);

            glBindVertexArray(0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
        }

        public void RecreateFontDeviceTexture()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out int bytesPerPixel);

            int mips = (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

            int prevActiveTexture;
            glGetIntegerv(GL_ACTIVE_TEXTURE, &prevActiveTexture);
            glActiveTexture((int)TextureUnit.Texture0);
            int prevTexture2D;
            glGetIntegerv(GL_TEXTURE_BINDING_2D, &prevTexture2D);

            uint tid;
            glGenTextures(1, &tid);
            _fontTexture = tid;

            glBindTexture(GL_TEXTURE_2D, _fontTexture);
            glTexStorage2D(GL_TEXTURE_2D, mips, GL_RGBA8, width, height);
            LabelObject(GL_TEXTURE, tid, "ImGui Text Atlas");

            glTexSubImage2D(GL_TEXTURE_2D, 0, 0, 0, width, height, GL_BGRA, GL_UNSIGNED_BYTE, pixels.ToPointer());

            glGenerateMipmap(GL_TEXTURE_2D);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAX_LEVEL, mips - 1);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
            
            // Restore state
            glBindTexture(GL_TEXTURE_2D, (uint)prevTexture2D);
            glActiveTexture((uint)prevActiveTexture);

            io.Fonts.SetTexID((IntPtr)_fontTexture);

            io.Fonts.ClearTexData();
        }

        /// <summary>
        /// Renders the ImGui draw list data.
        /// </summary>
        public void Render()
        {
            if (_frameBegun)
            {
                _frameBegun = false;
                ImGui.Render();
                RenderImDrawData(ImGui.GetDrawData());
            }
        }

        /// <summary>
        /// Updates ImGui input and IO configuration state.
        /// </summary>
        public void Update()
        {
            if (_frameBegun)
            {
                ImGui.Render();
            }

            SetPerFrameImGuiData(Time.DeltaTime);
            UpdateImGuiInput();

            _frameBegun = true;
            ImGui.NewFrame();
        }

        public void Dispose()
        {
            _vertexArray.Delete();
            _vertexArray.Delete();
            _indexBuffer.Delete();

            uint tid = _fontTexture;
            glDeleteTextures(1, &tid);

            _shader.Dispose();
        }

        public static void LabelObject(uint objLabelIdent, uint glObject, string name)
        {
            GlUtils.StrToByteArrayPtr(name, out byte** ptr, out byte[] buffer);
            if (KHRDebugAvailable)
            {
                glObjectLabel(objLabelIdent, glObject, buffer.Length, ptr);
            }
        }

        /// <summary>
        /// Sets per-frame data based on the associated window.
        /// This is called by Update(float).
        /// </summary>
        private void SetPerFrameImGuiData(float delta)
        {
            ImGuiIOPtr io = ImGui.GetIO();
            Vector2 windowSize = GameInstance.Window.WindowSize;
            io.DisplaySize = new System.Numerics.Vector2(windowSize.X, windowSize.Y);
            io.DisplayFramebufferScale = System.Numerics.Vector2.One;
            io.DeltaTime = delta; // DeltaTime is in seconds.
        }


        private void UpdateImGuiInput()
        {
            ImGuiIOPtr io = ImGui.GetIO();

            io.MouseDown[0] = Input.IsMouseButtonDown(MouseButton.Left);
            io.MouseDown[1] = Input.IsMouseButtonDown(MouseButton.Right);
            io.MouseDown[2] = Input.IsMouseButtonDown(MouseButton.Middle);

            var screenPoint = Input.MousePosition;
            var point = screenPoint;//wnd.PointToClient(screenPoint);
            io.MousePos = new System.Numerics.Vector2(point.X, point.Y);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (key == Keys.Unknown)
                {
                    continue;
                }
                io.KeysDown[(int)key] = Input.IsKeyDown(key);
            }

            foreach (var c in _pressedChars)
            {
                io.AddInputCharacter(c);
            }
            _pressedChars.Clear();

            io.KeyCtrl = Input.IsKeyDown(Keys.LeftControl) || Input.IsKeyDown(Keys.RightControl);
            io.KeyAlt = Input.IsKeyDown(Keys.LeftAlt) || Input.IsKeyDown(Keys.RightAlt);
            io.KeyShift = Input.IsKeyDown(Keys.LeftShift) || Input.IsKeyDown(Keys.RightShift);
            io.KeySuper = Input.IsKeyDown(Keys.LeftSuper) || Input.IsKeyDown(Keys.RightSuper);
        }

        internal void PressChar(char keyChar)
        {
            _pressedChars.Add(keyChar);
        }

        internal void MouseScroll(Vector2 offset)
        {
            ImGuiIOPtr io = ImGui.GetIO();

            io.MouseWheel = offset.Y;
            io.MouseWheelH = offset.X;
        }

        private void RenderImDrawData(ImDrawDataPtr draw_data)
        {
            if (draw_data.CmdListsCount == 0) return;

            // Get intial state.
            //int prevVAO = glGetInteger(GL_VERTEX_ARRAY_BINDING);
            int prevArrayBuffer,
                prevBlendEquationRgb,
                prevBlendEquationAlpha,
                prevBlendFuncSrcRgb,
                prevBlendFuncSrcAlpha,
                prevBlendFuncDstRgb,
                prevBlendFuncDstAlpha,
                prevActiveTexture;
            bool prevBlendEnabled, 
                prevScissorTestEnabled, 
                prevCullFaceEnabled, 
                prevDepthTestEnabled;
            
            glGetIntegerv(GL_ARRAY_BUFFER_BINDING, &prevArrayBuffer);
            //int prevProgram = glGetInteger(GL_CURRENT_PROGRAM);
            glGetBooleanv(GL_BLEND, &prevBlendEnabled);
            glGetBooleanv(GL_SCISSOR_TEST, &prevScissorTestEnabled);
            glGetIntegerv(GL_BLEND_EQUATION_RGB, &prevBlendEquationRgb);
            glGetIntegerv(GL_BLEND_EQUATION_ALPHA, &prevBlendEquationAlpha);
            glGetIntegerv(GL_BLEND_SRC_RGB, &prevBlendFuncSrcRgb);
            glGetIntegerv(GL_BLEND_SRC_ALPHA, &prevBlendFuncSrcAlpha);
            glGetIntegerv(GL_BLEND_DST_RGB, &prevBlendFuncDstRgb);
            glGetIntegerv(GL_BLEND_DST_ALPHA, &prevBlendFuncDstAlpha);
            glGetBooleanv(GL_CULL_FACE, &prevCullFaceEnabled);
            glGetBooleanv(GL_DEPTH_TEST, &prevDepthTestEnabled);
            glGetIntegerv(GL_ACTIVE_TEXTURE, &prevActiveTexture);
            glActiveTexture((int)TextureUnit.Texture0);
            //int prevTexture2D = glGetInteger(GL_TEXTURE_BINDING_2D);
            Span<int> prevScissorBox = stackalloc int[4];
            fixed (int* iptr = &prevScissorBox[0])
            {
                glGetIntegeri_v(GL_SCISSOR_BOX, 0, iptr);
            }

            glBindVertexArray(_vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, _vertexBuffer);
            
            for (int i = 0; i < draw_data.CmdListsCount; i++)
            {
                ImDrawListPtr cmd_list = draw_data.CmdListsRange[i];

                int vertexSize = cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
                if (vertexSize > _vertexBufferSize)
                {
                    int newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);
                    glBufferData(GL_ARRAY_BUFFER, new IntPtr(newSize), IntPtr.Zero.ToPointer(), GL_DYNAMIC_DRAW);
                    _vertexBufferSize = newSize;

                    Debug.Warning($"[WARNING][ImGui] Resized dear ImGui vertex buffer to new size {_vertexBufferSize}");
                }

                int indexSize = cmd_list.IdxBuffer.Size * sizeof(ushort);
                if (indexSize > _indexBufferSize)
                {
                    int newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
                    glBufferData(GL_ELEMENT_ARRAY_BUFFER, new IntPtr(newSize), IntPtr.Zero.ToPointer(), GL_DYNAMIC_DRAW);
                    _indexBufferSize = newSize;

                    Debug.Warning($"[WARNING][ImGui] Resized dear ImGui index buffer to new size {_indexBufferSize}");
                }
            }

            // Setup orthographic projection matrix into our constant buffer
            ImGuiIOPtr io = ImGui.GetIO();
            Viewport viewport = GameInstance.Window.Viewport;
            Matrix4 mvp = Matrix4.Orthographic(viewport.Width, viewport.Height, 0.01f, 400.0f);
            
            _shader.Start();
            _shader.UseUniformProjectionMat4("projection_matrix", mvp);
            _shader.UseUniform1f("in_fontTexture", 0);

            //glBindVertexArray(_vertexArray);

            draw_data.ScaleClipRects(io.DisplayFramebufferScale);

            glEnable(GL_BLEND);
            glEnable(GL_SCISSOR_TEST);
            glBlendEquation(GL_FUNC_ADD);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
            glDisable(GL_CULL_FACE);
            glDisable(GL_DEPTH_TEST);

            int bufferSize;
            glGetIntegerv(GL_BUFFER_SIZE, &bufferSize);
            Debug.Log(bufferSize);
            
            // Render command lists
            for (int n = 0; n < draw_data.CmdListsCount; n++)
            {
                ImDrawListPtr cmdList  = draw_data.CmdListsRange[n];

                int offset = 0;
                int vtxBufferSize = cmdList.VtxBuffer.Size * Marshal.SizeOf<ImDrawVert>();
                Debug.Log(vtxBufferSize);
                glBufferSubData(GL_ARRAY_BUFFER, &offset, &vtxBufferSize, cmdList.VtxBuffer.Data.ToPointer());

                int idxBufferSize = cmdList.IdxBuffer.Size * sizeof(ushort);
                Debug.Log(idxBufferSize);
                glBufferSubData(GL_ELEMENT_ARRAY_BUFFER, &offset, &idxBufferSize, cmdList.IdxBuffer.Data.ToPointer());
                
                for (int cmd_i = 0; cmd_i < cmdList.CmdBuffer.Size; cmd_i++)
                {
                    ImDrawCmdPtr pcmd = cmdList.CmdBuffer[cmd_i];
                    if (pcmd.UserCallback != IntPtr.Zero) throw new NotImplementedException();

                    glActiveTexture((int)TextureUnit.Texture0);
                    glBindTexture(GL_TEXTURE_2D, (uint)pcmd.TextureId);
                    
                    var clip = pcmd.ClipRect;
                    glScissor((int)clip.X, (int)(GameInstance.Window.WindowSize.Y - clip.W), (int)(clip.Z - clip.X), (int)(clip.W - clip.Y));

                    if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                    {
                        glDrawElementsBaseVertex(GL_TRIANGLES, (int)pcmd.ElemCount, GL_UNSIGNED_SHORT, (void*)(pcmd.IdxOffset * sizeof(ushort)), unchecked((int)pcmd.VtxOffset));
                    }
                    else
                    {
                        glDrawElements(GL_TRIANGLES, (int)pcmd.ElemCount, GL_UNSIGNED_SHORT, (void*)(pcmd.IdxOffset * sizeof(ushort)));
                    }
                }
            }

            glDisable(GL_BLEND);
            glDisable(GL_SCISSOR_TEST);

            _shader.Stop();
            
            // Reset state
            glBindTexture(GL_TEXTURE_2D, 0);
            glActiveTexture((uint)prevActiveTexture);
            
            glBindVertexArray(0);
            glScissor(prevScissorBox[0], prevScissorBox[1], prevScissorBox[2], prevScissorBox[3]);
            glBindBuffer(GL_ARRAY_BUFFER, (uint)prevArrayBuffer);
            glBlendEquationSeparate((uint)prevBlendEquationRgb, (uint)prevBlendEquationAlpha);
            glBlendFuncSeparate(
                (uint)prevBlendFuncSrcRgb,
                (uint)prevBlendFuncDstRgb,
                (uint)prevBlendFuncSrcAlpha,
                (uint)prevBlendFuncDstAlpha);
            if (prevBlendEnabled) glEnable(GL_BLEND); else glDisable(GL_BLEND);
            if (prevDepthTestEnabled) glEnable(GL_DEPTH_TEST); else glDisable(GL_DEPTH_TEST);
            if (prevCullFaceEnabled) glEnable(GL_CULL_FACE); else glDisable(GL_CULL_FACE);
            if (prevScissorTestEnabled) glEnable(GL_SCISSOR_TEST); else glDisable(GL_SCISSOR_TEST);
        }

        private static void SetKeyMappings()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab;
            io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left;
            io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right;
            io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up;
            io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down;
            io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp;
            io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown;
            io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home;
            io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End;
            io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete;
            io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Backspace;
            io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter;
            io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape;
            io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A;
            io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C;
            io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V;
            io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X;
            io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y;
            io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z;
        }

        private static bool IsExtensionSupported(string name)
        {
            int n;
            glGetIntegerv(GL_NUM_EXTENSIONS, &n);
            
            for (uint i = 0; i < n; i++)
            {
                string extension = GlUtils.PtrToStringUTF8(glGetStringi(GL_EXTENSIONS, i));
                
                if (extension == name) return true;
            }

            return false;
        }
    }
}