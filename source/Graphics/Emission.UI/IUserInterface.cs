namespace Emission.Graphics.UI
{
    public interface IUserInterface
    {
        UIContext Context { get; }
        
        public void RenderGUI();
    }
}