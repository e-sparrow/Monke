using System;
using System.Collections.Generic;
using System.Linq;
using ESparrow.Utils.Extensions;
using Game.Constants;
using Game.Settings.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Coins
{
    public class CoinSystem
    {
        public CoinSystem(ICoinsSettings coinsSettings, IWallet wallet, IAudioSettings audioSettings, MemoryPool<AudioSource> audioSourcePool)
        {
            _coinsSettings = coinsSettings;
            _wallet = wallet;
            _audioSettings = audioSettings;
            _audioSourcePool = audioSourcePool;
        }

        private readonly ICoinsSettings _coinsSettings;
        private readonly IWallet _wallet;
        private readonly IAudioSettings _audioSettings;
        private readonly MemoryPool<AudioSource> _audioSourcePool;

        private readonly IDictionary<ICoinModel, Vector3> _coins = new Dictionary<ICoinModel, Vector3>();

        private int _coinIndex = 0;

        [Inject]
        private void Initialize()
        {
            var positions = _coinsSettings.SpawnCoinPositions.GetNonRepeatingWeighedRandom(_coinsSettings.InitialCoins, _ => 1);
            foreach (var position in positions)
            {
                CreateCoin(position);
            }
        }
        
        private void TakeCoin(ICoinModel coinModel)
        { 
            _wallet.Add(coinModel.Denomination);

            var audioSource = _audioSourcePool.Spawn();
            var clip = _audioSettings.AudioStorage[AudioConstants.TakeCoin];

            audioSource.pitch = Mathf.Lerp(1f, 1.5f, (_coinIndex % 4) / 4f);
            audioSource.PlayOneShot(clip);

            _coinIndex++;
            
            var position = _coins[coinModel];
            _coins.Remove(coinModel);
            coinModel.Take();

            var nextPosition = _coinsSettings.SpawnCoinPositions.Without(position).Without(_coins.Values.Select(value => value.ToVector2())).GetRandom();
            CreateCoin(nextPosition);
        }

        private void CreateCoin(Vector3 position)
        {
            var view = Object.Instantiate(_coinsSettings.CoinViewPrefab, position, Quaternion.identity);
            var model = new CoinModel(view.gameObject, 1);
            
            var presenter = new CoinPresenter(view, model);
            presenter.Initialize();

            view.OnCoinTake += () => TakeCoin(model);
            
            _coins[model] = position;
        }
    }
}