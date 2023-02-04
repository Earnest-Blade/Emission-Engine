using System;

using Emission.IO;
using Emission.Graphics;
using Emission.Mathematics;
using Emission.Graphics.Shading;
using static Emission.Natives.GL.Gl;

namespace Emission.UI
{
    public unsafe class Text
    {
        public readonly Font Font;
        public readonly Shader Shader;

        public Transform Transform;

        private Matrix4 _projection;

        private VertexArrayBuffer _vao;
        private VertexBufferObject _vbo;

        public Text(Font font, Shader shader)
        {
            Font = font;
            Shader = shader;
            Transform = Transform.Zero;

            Viewport viewport = GameInstance.Window.Viewport;
            _projection = Matrix4.Orthographic(viewport.Width, viewport.Height, viewport.NearDepth, viewport.FarDepth);

            Initialize();
        }

        private void Initialize()
        {
            _vao = Renderer.BindVertexArray();
            _vbo = Renderer.BindBuffer(sizeof(float) * 6 * 4, IntPtr.Zero);

            glEnableVertexAttribArray(0);
            glVertexAttribPointer(0, 4, GL_FLOAT, false, 4 * sizeof(float), (void*)0);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        public void Draw(string text, float x, float y, float scale, ColorRgb color)
        {
            Shader.Start();

            Shader.UseUniformVec3("color", new Vector3(color.R, color.G, color.B));

            Shader.UseUniformProjectionMat4(Shader.UNIFORM_PROJECTION, ICamera.GetCurrent().Projection);

            glActiveTexture(GL_TEXTURE0);
            glBindVertexArray(_vao.Id);

            foreach (char c in text)
            {
                Character ch = Font.Characters[c];

                float xpos = x + ch.Bearing.X * scale;
                float ypos = y - (ch.Size.X - ch.Bearing.Y) * scale;

                float w = ch.Size.X * scale;
                float h = ch.Size.Y * scale;

                float[,] vertices = {
                    { xpos,     ypos + h,   0.0f, 0.0f },
                    { xpos,     ypos,       0.0f, 1.0f },
                    { xpos + w, ypos,       1.0f, 1.0f },

                    { xpos,     ypos + h,   0.0f, 0.0f },
                    { xpos + w, ypos,       1.0f, 1.0f },
                    { xpos + w, ypos + h,   1.0f, 0.0f }
                };

                glBindTexture(GL_TEXTURE_2D, ch.TextureID);

                glBindBuffer(GL_ARRAY_BUFFER, _vbo.Id);

                int size = sizeof(float) * vertices.Length;
                fixed (float* f = &vertices[0, 0])
                    glBufferSubData(GL_ARRAY_BUFFER, (int*)0, &size, f);

                glDrawArrays(GL_TRIANGLES, 0, 6);

                x += (ch.Advance >> 6) * scale;
            }

            glBindVertexArray(_vao.Id);
            glBindTexture(GL_TEXTURE_2D, 0);
            
            Shader.Stop();
        }
    }
}
