using System;
using System.Diagnostics.CodeAnalysis;
using Emission.Math;

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
        [SuppressMessage("ReSharper", "EventUnsubscriptionViaAnonymousDelegate")]
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
    internal interface IInternalEngineBehavior
    {
        public int ID { get; }
        public string Name { get; }
        
        void Initialize();
        void Update();
        void Render();
        void Dispose();
    }
}