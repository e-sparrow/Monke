using System;

namespace Game.Coins
{
    public interface ICoinView
    {
        event Action OnCoinTake;
    }
}