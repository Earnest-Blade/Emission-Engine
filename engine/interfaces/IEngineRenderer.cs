using Emission.Math;

namespace Emission
{
    public interface IEngineRenderer
    {
        void Initialize();
        void Update(ref Transform transform);
        void PreRender();
        void Render();
        void PostRender();
        void Dispose();

        virtual void Subdivide() {}
        virtual void Subdivide(int n) {}
    }
}
