using System;
using System.Diagnostics;
using Emission.Lighting;
using OpenTK.Graphics.OpenGL;

using Emission.Math;
using Emission.Shading;
using Emission.Toolbox;
using OpenTK.Mathematics;

namespace Emission
{
    public class Mesh : IVirtualEngineBehavior, IDisposable, IGuiSubmitable
    {
        // Engine Behavior variables
        public int ID { get; private set; }
        public string Name { get; private set; }
        
        public Material Material { get; }
        public Transform Transform;

        protected int _vaoID;
        protected int _vboID;
        protected int _eboID;

        protected float[] _vertices;
        protected int[] _indices;
        
        public Mesh(Material material, (float[], int[]) tuple) : this(material, tuple.Item1, tuple.Item2) { }

        public Mesh(Material material, float[] vertices, int[] indices)
        {
            Transform = new Transform();
            Material = material;

            _vertices = vertices;
            _indices = indices;
            
            Load();
        }

        /// <summary>
        /// Call after construct object, load VAOs, VBOs and EBOs to Ram and bind Textures.
        /// Also define a name to the mesh.
        /// </summary>
        public virtual void Load()
        {
            _vaoID = GraphicAllocator.BindVertexArray();
            _vboID = GraphicAllocator.Bind3DBuffer(0, _vertices);
            _eboID = GraphicAllocator.BindIndices(_indices);
            
            // enable normals
            GraphicAllocator.EnableVertexArray(2, GraphicAllocator.STRIDE, 5);
            
            Material.BindTextures();
            
            GraphicAllocator.UnbindVertexArray();

            ID = int.Parse(_vaoID + "" + _vboID + "" + _eboID);
            Name = $"mesh{ID}";

            Debug.Log("[INFO] " + Name + " initialized!");
        }

        /// <summary>
        /// Call every frame, use to update state and variables of the object.
        /// </summary>
        public virtual void Update()
        {
            Material.Start();
            
            Material.Shader.UseUniform1f("alpha", Transform.Alpha);
            
            // Light
            
            
            Material.Update();
            
            // Transformation
            Material.Shader.UseUniformMat4("uTransform", Transform.TransformMatrix);
            Material.Shader.UseUniformMat4("uTransformizedTransform", Matrix4.Transpose(Matrix4.Invert(Transform.TransformMatrix)));
            Material.Shader.UseUniformMat4("uView", Camera.Main.ViewMatrix());
            Material.Shader.UseUniformProjectionMat4("uProjection", Camera.Main.ProjectionMatrix());
            
            Material.Stop();
        }
    
        /// <summary>
        /// Bind and Start Shader and Material to getting ready for rendering object.
        /// </summary>
        public virtual void PreRender()
        {
            Material.Start();

            GL.BindVertexArray(_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
                
            Material.UseTextures();
        }
        
        /// <summary>
        /// Call every frame, when object need to be render. Call Draw Element method in order to
        /// create element.
        /// Check if mesh is visible or not using transform.
        /// </summary>
        public virtual void Render()
        {
            if (Transform.IsVisible)
            {
                PreRender();
                
                GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
                
                PostRender();
            }
        }

        /// <summary>
        /// Call after transform.
        /// Disable Vertex Array 
        /// </summary>
        public virtual void PostRender()
        {
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
            
            Material.Stop();
        }

        /// <summary>
        /// Destroy object and clear it.
        /// </summary>
        public virtual void Dispose()
        {
            Material.Dispose();
            GraphicAllocator.Clear(_vaoID, _vboID, _eboID);
        }

        /// <summary>
        /// Make mesh visible.
        /// </summary>
        public virtual void Show()
        {
            Transform.IsVisible = true;
        }

        /// <summary>
        /// Make mesh invisible.
        /// </summary>
        public virtual void Hide()
        {
            Transform.IsVisible = false;
        }
        
        public void SubmitImGui()
        {
            ImGuiNET.ImGui.TextColored(ColorHelper.RGB(255, 236, 0), $"Object {Name}"); 
            ImGuiNET.ImGui.SliderFloat($"{Name} Position X", ref Transform.Position.X, -10, 10);
            ImGuiNET.ImGui.SliderFloat($"{Name} Position Y", ref Transform.Position.Y, -10, 10);
            ImGuiNET.ImGui.SliderFloat($"{Name} Position Z", ref Transform.Position.Z, -10, 10);
            
            Vector3 currentRotation = Transform.Rotation;
            ImGuiNET.ImGui.SliderFloat($"{Name} Rotation X", ref currentRotation.X, -180, 180);
            ImGuiNET.ImGui.SliderFloat($"{Name} Rotation Y", ref currentRotation.Y, -180, 180);
            ImGuiNET.ImGui.SliderFloat($"{Name} Rotation Z", ref currentRotation.Z, -180, 180);
            Transform.Rotation = currentRotation;

        }

        public override string ToString()
        {
            return string.Format("[{0}\n{1}]", string.Join(", ", _vertices), string.Join(", ", _indices));
        }
    }
}
