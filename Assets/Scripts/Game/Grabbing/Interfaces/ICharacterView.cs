using System;
using Game.Interactions.Interfaces;

namespace Game.Grabbing.Interfaces
{
    public interface ICharacterView
    {
        event Action<IInteractor> OnHit;
    }
}