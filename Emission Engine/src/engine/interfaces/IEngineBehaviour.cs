using System;

namespace Emission
{
    /// <summary>
    /// Interface that define engine component structure.
    /// </summary>
    public interface IEngineBehaviour
    {
        IEngineBehaviour Behaviour {get;}
        
        void Initialize();
        void Start();
        void Update();
        void PreRender();
        void Render();
        void PostRender();
        void Dispose();

        /// <summary>
        /// Make behaviour follow all callbacks.
        /// </summary>
        public void BindCallbacks()
        {
            Application.Singleton.InitializeCallback += (o, e) => Initialize();
            Application.Singleton.StartCallback += (o, e) => Start();
            Application.Singleton.UpdateCallback += (o, e) => Update();
            Application.Singleton.PreRenderCallback += (o, e) => PreRender();
            Application.Singleton.RenderCallback += (o, e) => Render();
            Application.Singleton.PostRenderCallback += (o, e) => PostRender();
            Application.Singleton.StopEvent += (o, e) => Dispose();
        }
        
        /// <summary>
        /// Make behaviour unfollow all callbacks.
        /// </summary>
        public void UnbindCallbacks()
        {
            Application.Singleton.InitializeCallback -= (o, e) => Initialize();
            Application.Singleton.StartCallback -= (o, e) => Start();
            Application.Singleton.UpdateCallback -= (o, e) => Update();
            Application.Singleton.PreRenderCallback -= (o, e) => PreRender();
            Application.Singleton.RenderCallback -= (o, e) => Render();
            Application.Singleton.PostRenderCallback -= (o, e) => PostRender();
            Application.Singleton.StopEvent -= (o, e) => Dispose();
        }
    }

    /// <summary>
    /// Interface that define internal engine components structure.
    /// </summary>
    internal interface IVirtualEngineBehavior
    {
        public int ID { get; }
        public string Name { get; }
        
        void Load();
        void Update();
        void PreRender();
        void Render();
        void PostRender();
        void Dispose();

        void Show();
        void Hide();
    }
}