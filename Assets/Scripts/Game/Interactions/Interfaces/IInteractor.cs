using Game.Interactions.Enums;

namespace Game.Interactions.Interfaces
{
    public interface IInteractor
    {
        EInteractorType Type
        {
            get;
        }
    }
}