using System;

using Emission.IO;
using Emission.Math;
using Emission.Shading;
using Emission.GFX;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.UI;

public class Text : IInternalEngineBehavior, IDisposable
{
    public int ID { get; }
    public string Name { get; }

    public Transform Transform;
    public Shader Shader;
    public Vector3 Color;
    public Font Font;
    
    public string TextContent;

    private int _vaoID;
    private int _vboID;

    public Text(Font font, Vector3 color, string content, string shaderPath="assets/shader/color.glsl")
    {
        Font = font;
        TextContent = content;
        Color = color;
        Shader = new Shader(shaderPath);
        Transform = new Transform();
        
        Initialize();
    }
    
    public void Initialize()
    {
        _vaoID = Renderer.BindVertexArray();
        //_vboID = Renderer.Bind4DBuffer(0, new float[]{}, sizeof(float) * 6 * 4, BufferUsageHint.DynamicDraw);
        _vboID = Renderer.Bind4DBuffer(0, new float[]{});
        Renderer.DisableVertexArray();
    }

    public void Update()
    {
        Shader.Start();
        Shader.UseUniformVec3("uTextColor", Color);
        Shader.UseUniformProjectionMat4("uProjection", Camera.Main.ViewMatrix * Camera.Main.ProjectionMatrix);
        Shader.Stop();
    }

    public void PreRender()
    {
        Shader.Start();
        
        GL.ActiveTexture(TextureUnit.Texture0);
        
        GL.BindVertexArray(_vaoID);
        GL.EnableVertexAttribArray(0);
    }

    public void Render()
    {
        if (Transform.IsVisible)
        {
            float x = Transform.Position.X;
            PreRender();

            for (int i = 0; i < TextContent.Length; i++)
            {
                FontCharacter character = Font.Characters[TextContent[i]];

                float xpos = x + character.Bearing.X * Transform.Scale.X;
                float ypos = Transform.Position.Y - (character.Size.Y - character.Bearing.Y) * Transform.Scale.Y;

                float w = character.Size.X * Transform.Scale.X;
                float h = character.Size.Y * Transform.Scale.Y;
                
                float[] vertices = new float[]
                {
                    xpos    , ypos + h, 0.0f, 0.0f,
                    xpos    , ypos    , 0.0f, 1.0f,
                    xpos + w, ypos    , 1.0f, 1.0f,

                    xpos    , ypos + h, 0.0f, 0.0f,
                    xpos + w, ypos    , 1.0f, 1.0f,
                    xpos + w, ypos + h, 1.0f, 0.0f
                };
                
                GL.BindTexture(TextureTarget.Texture2D, character.TextureID);
                
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vboID);
                GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, sizeof(float) * 6 * 4, vertices);
                //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

                x += (character.Advance >> 6) * Transform.Scale.X;
            }
            
            PostRender();
        }
    }

    public void PostRender()
    {
        GL.BindVertexArray(0);
        Shader.Stop();
    }

    public void Dispose()
    {
        
    }

    public void Show()
    {
        Transform.IsVisible = true;
    }

    public void Hide()
    {
        Transform.IsVisible = false;
    }
}
