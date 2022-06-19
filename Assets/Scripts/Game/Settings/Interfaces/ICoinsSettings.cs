using System.Collections.Generic;
using Game.Coins;
using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface ICoinsSettings
    {
        int InitialCoins
        {
            get;
        }

        MonoCoinView CoinViewPrefab
        {
            get;
        }
        
        IEnumerable<Vector2> SpawnCoinPositions
        {
            get;
        }
    }
}