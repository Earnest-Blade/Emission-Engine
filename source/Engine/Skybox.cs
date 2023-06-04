using Emission.Annotations;
using Emission.Assets;
using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Engine.Page;
using Emission.Graphics;
using Emission.Graphics.GeometricPrimitives;

using static Emission.Natives.GL.Gl;

namespace Emission.Engine
{
    [LonelyActor]
    public unsafe class Skybox : Actor
    {
        private const string SKYBOX_TEXTURE_NAME = "cubemapTexture";
        
        private string _path;
        private Mesh _mesh;
        private Shader _shader;
        private uint _texture;
        
        public Skybox(string path, int size)
        {
            _mesh = GeometricPrimitive.PrimitiveCube(new Vector3(size)).GetMeshes()[0];
            _path = path;

            const string vert = @"
                #version 450 core
                
                layout(location = 0) in vec3 Position;
                
                uniform mat4 uView;
                
                out vec3 texCoord;
                
                void main() {
                    vec4 wvp_position = uView * vec4(Position, 1.0);
                    gl_Position = wvp_position.xyww;
                    texCoord = Position;
                ";
            
            const string frag = @"
            #version 450 core
            
            in vec3 texCoord;
            
            out vec4 fragColor;
            
            uniform sampler2D cubemapTexture;
            
            void main(){
                fragColor = texture(cubemapTexture, TexCoord);
            }
            ";
            
            _shader = Shader.FromVertexFragment(vert, frag);
        }

        public override void Enable()
        {
            base.Enable();

            int width, height;
            byte[] data = Image.LoadImageFromPath(_path, &width, &height);

            _texture = Renderer.BindTexture2D(data, width, height);
        }

        public override void Render()
        {
            base.Render();

            _shader.Start();
            _shader.UseUniformMat4("uView", PageManager.ActiveCamera!.View);
            
            glActiveTexture(GL_TEXTURE0);
            glBindTexture(GL_TEXTURE_2D, _texture);
            _shader.UseUniform1(SKYBOX_TEXTURE_NAME, 0);

            _mesh.Draw(_shader);

            glBindTexture(GL_TEXTURE_2D, 0);

            _shader.Stop();
        }

        public override void Disable()
        {
            base.Disable();
            
            if (_texture == 0) return;
            
            uint texId = _texture;
            glDeleteTextures(1, &texId);

            Renderer.RemoveTextureId(texId);
            _texture = 0;
        }
    }
}