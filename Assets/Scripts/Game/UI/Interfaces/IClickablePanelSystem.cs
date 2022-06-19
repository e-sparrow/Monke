namespace Game.UI.Interfaces
{
    public interface IClickablePanelSystem
    {
        void Register(IClickablePanelView view);
        void Unregister(IClickablePanelView view);
    }
}