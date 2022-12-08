using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using ImGuiNET;

using Emission.Mathematics;
using Emission.Graphics.Shading;
using static Emission.Graphics.GL.GL;

namespace Emission.UI
{
    public unsafe class ImGuiController : IDisposable
    {
        private bool _frameBegun;

        private uint _vertexArray;
        private uint _vertexBuffer;
        private int _vertexBufferSize;
        private uint _indexBuffer;
        private int _indexBufferSize;

        private uint _fontTexture;

        private Shader _shader;

        public ImGuiController()
        {
            int major = glGetInteger(GL_MAJOR_VERSION);
            int minor = glGetInteger(GL_MINOR_VERSION);

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

            uint prevVAO = (uint)glGetInteger(GL_VERTEX_ARRAY_BINDING);
            uint prevArrayBuffer = (uint)glGetInteger(GL_ARRAY_BUFFER_BINDING);

            _vertexArray = glGenVertexArray();
            glBindVertexArray(_vertexArray);

            _vertexBuffer = glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, _vertexBuffer);
            glBufferData(GL_ARRAY_BUFFER, _vertexBufferSize, IntPtr.Zero, GL_DYNAMIC_DRAW);

            _indexBuffer = glGenBuffer();
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _indexBuffer);
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indexBufferSize, IntPtr.Zero, GL_DYNAMIC_DRAW);

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
                // * texture(in_fontTexture, texCoord)
                outputColor = color;
            }";

            _shader = new Shader(VertexSource, FragmentSource);

            int stride = Unsafe.SizeOf<ImDrawVert>();
            glVertexAttribPointer(0, 2, GL_FLOAT, false, stride, (void*)0);
            glVertexAttribPointer(1, 2, GL_FLOAT, false, stride, (void*)8);
            glVertexAttribPointer(2, 4, GL_UNSIGNED_BYTE, true, stride, (void*)16);

            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
            glEnableVertexAttribArray(2);

            glBindVertexArray(prevVAO);
            glBindBuffer(GL_ARRAY_BUFFER, prevArrayBuffer);
        }

        public void RecreateFontDeviceTexture()
        {
            ImGuiIOPtr io = ImGui.GetIO();
            io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out int bytesPerPixel);

            int mips = (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

            int prevActiveTexture = glGetInteger(GL_ACTIVE_TEXTURE);
            glActiveTexture((int)TextureUnit.Texture0);
            uint prevTexture2D = (uint)glGetInteger(GL_TEXTURE_BINDING_2D);

            _fontTexture = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, _fontTexture);
            //glTexStor(GL_TEXTURE_2D, mips, GL_RGBA8, width, height);

            glTexSubImage2D(GL_TEXTURE_2D, 0, 0, 0, width, height, GL_BGRA, GL_UNSIGNED_BYTE, pixels);

            glGenerateMipmap(GL_TEXTURE_2D);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAX_LEVEL, mips - 1);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);

            // Restore state
            glBindTexture(GL_TEXTURE_2D, prevTexture2D);
            glActiveTexture(prevActiveTexture);

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
        public void Update(float deltaSeconds)
        {
            if (_frameBegun)
            {
                ImGui.Render();
            }

            SetPerFrameImGuiData(deltaSeconds);
            UpdateImGuiInput();

            _frameBegun = true;
            ImGui.NewFrame();
        }

        public void Dispose()
        {
            glDeleteVertexArray(_vertexArray);
            glDeleteBuffer(_vertexBuffer);
            glDeleteBuffer(_indexBuffer);

            glDeleteTexture(_fontTexture);

            _shader.Dispose();
        }

        /// <summary>
        /// Sets per-frame data based on the associated window.
        /// This is called by Update(float).
        /// </summary>
        private void SetPerFrameImGuiData(float deltaSeconds)
        {
            ImGuiIOPtr io = ImGui.GetIO();
            Vector2 windowSize = GameInstance.Window.WindowSize;
            io.DisplaySize = new System.Numerics.Vector2(windowSize.X, windowSize.Y);
            io.DisplayFramebufferScale = System.Numerics.Vector2.One;
            io.DeltaTime = deltaSeconds; // DeltaTime is in seconds.
        }

        readonly List<char> PressedChars = new List<char>();

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

            foreach (var c in PressedChars)
            {
                io.AddInputCharacter(c);
            }
            PressedChars.Clear();

            io.KeyCtrl = Input.IsKeyDown(Keys.LeftControl) || Input.IsKeyDown(Keys.RightControl);
            io.KeyAlt = Input.IsKeyDown(Keys.LeftAlt) || Input.IsKeyDown(Keys.RightAlt);
            io.KeyShift = Input.IsKeyDown(Keys.LeftShift) || Input.IsKeyDown(Keys.RightShift);
            io.KeySuper = Input.IsKeyDown(Keys.LeftSuper) || Input.IsKeyDown(Keys.RightSuper);
        }

        internal void PressChar(char keyChar)
        {
            PressedChars.Add(keyChar);
        }

        internal void MouseScroll(Vector2 offset)
        {
            ImGuiIOPtr io = ImGui.GetIO();

            io.MouseWheel = offset.Y;
            io.MouseWheelH = offset.X;
        }

        private void RenderImDrawData(ImDrawDataPtr draw_data)
        {
            if (draw_data.CmdListsCount == 0)
            {
                return;
            }

            // Get intial state.
            int prevVAO = glGetInteger(GL_VERTEX_ARRAY_BINDING);
            int prevArrayBuffer = glGetInteger(GL_ARRAY_BUFFER_BINDING);
            int prevProgram = glGetInteger(GL_CURRENT_PROGRAM);
            bool prevBlendEnabled = glGetBoolean(GL_BLEND);
            bool prevScissorTestEnabled = glGetBoolean(GL_SCISSOR_TEST);
            int prevBlendEquationRgb = glGetInteger(GL_BLEND_EQUATION_RGB);
            int prevBlendEquationAlpha = glGetInteger(GL_BLEND_EQUATION_ALPHA);
            int prevBlendFuncSrcRgb = glGetInteger(GL_BLEND_SRC_RGB);
            int prevBlendFuncSrcAlpha = glGetInteger(GL_BLEND_SRC_ALPHA);
            int prevBlendFuncDstRgb = glGetInteger(GL_BLEND_DST_RGB);
            int prevBlendFuncDstAlpha = glGetInteger(GL_BLEND_DST_ALPHA);
            bool prevCullFaceEnabled = glGetBoolean(GL_CULL_FACE);
            bool prevDepthTestEnabled = glGetBoolean(GL_DEPTH_TEST);
            int prevActiveTexture = glGetInteger(GL_ACTIVE_TEXTURE);
            glActiveTexture((int)TextureUnit.Texture0);
            int prevTexture2D = glGetInteger(GL_TEXTURE_BINDING_2D);
            Span<int> prevScissorBox = stackalloc int[4];
            

            // Bind the element buffer (thru the VAO) so that we can resize it.
            glBindVertexArray(_vertexArray);
            // Bind the vertex buffer so that we can resize it.
            glBindBuffer(GL_ARRAY_BUFFER, _vertexBuffer);
            for (int i = 0; i < draw_data.CmdListsCount; i++)
            {
                ImDrawListPtr cmd_list = draw_data.CmdListsRange[i];

                int vertexSize = cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
                if (vertexSize > _vertexBufferSize)
                {
                    int newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);

                    glBufferData(GL_ARRAY_BUFFER, newSize, IntPtr.Zero, GL_DYNAMIC_DRAW);
                    _vertexBufferSize = newSize;

                    Console.WriteLine($"Resized dear imgui vertex buffer to new size {_vertexBufferSize}");
                }

                int indexSize = cmd_list.IdxBuffer.Size * sizeof(ushort);
                if (indexSize > _indexBufferSize)
                {
                    int newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
                    glBufferData(GL_ELEMENT_ARRAY_BUFFER, newSize, IntPtr.Zero, GL_DYNAMIC_DRAW);
                    _indexBufferSize = newSize;

                    Console.WriteLine($"Resized dear imgui index buffer to new size {_indexBufferSize}");
                }
            }

            // Setup orthographic projection matrix into our constant buffer
            ImGuiIOPtr io = ImGui.GetIO();
            Matrix4 mvp = Matrix4.OrthographicOffCenter(0.0f, io.DisplaySize.X, io.DisplaySize.Y, 0.0f, -1.0f, 1.0f);

            _shader.Start();
            _shader.UseUniformProjectionMat4("projection_matrix", mvp);
            _shader.UseUniform1f("in_fontTexture", 0);

            glBindVertexArray(_vertexArray);

            draw_data.ScaleClipRects(io.DisplayFramebufferScale);

            glEnable(GL_BLEND);
            glEnable(GL_SCISSOR_TEST);
            glBlendEquation(GL_FUNC_ADD);
            glBlendFunc(GL_BLEND_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
            glDisable(GL_CULL_FACE);
            glDisable(GL_DEPTH_TEST);

            // Render command lists
            for (int n = 0; n < draw_data.CmdListsCount; n++)
            {
                ImDrawListPtr cmd_list = draw_data.CmdListsRange[n];

                glBufferSubData(GL_ARRAY_BUFFER, 0, cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(), cmd_list.VtxBuffer.Data);

                glBufferSubData(GL_ELEMENT_ARRAY_BUFFER, 0, cmd_list.IdxBuffer.Size * sizeof(ushort), cmd_list.IdxBuffer.Data);

                for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
                {
                    ImDrawCmdPtr pcmd = cmd_list.CmdBuffer[cmd_i];
                    if (pcmd.UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        glActiveTexture((int)TextureUnit.Texture0);
                        glBindTexture(GL_TEXTURE_2D, (uint)pcmd.TextureId);

                        // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has flipped Y when it comes to these coordinates
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
            }

            glDisable(GL_BLEND);
            glDisable(GL_SCISSOR_TEST);

            // Reset state
            glBindTexture(GL_TEXTURE_2D, (uint)prevTexture2D);
            glActiveTexture(prevActiveTexture);
            glUseProgram((uint)prevProgram);
            glBindVertexArray((uint)prevVAO);
            glScissor(prevScissorBox[0], prevScissorBox[1], prevScissorBox[2], prevScissorBox[3]);
            glBindBuffer(GL_ARRAY_BUFFER, (uint)prevArrayBuffer);
            glBlendEquationSeparate(prevBlendEquationRgb, prevBlendEquationAlpha);
            glBlendFuncSeparate(
                prevBlendFuncSrcRgb,
                prevBlendFuncDstRgb,
                prevBlendFuncSrcAlpha,
                prevBlendFuncDstAlpha);
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
    }
}