using System;
using System.Collections.Generic;
using Game.Coins;
using Game.Settings.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableCoinsSettings : ICoinsSettings
    {
        [SerializeField] private List<Vector2> spawnCoinPositions;

        [field: SerializeField]
        public MonoCoinView CoinViewPrefab
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int InitialCoins
        {
            get;
            private set;
        }
        
        public IEnumerable<Vector2> SpawnCoinPositions => spawnCoinPositions;
    }
}