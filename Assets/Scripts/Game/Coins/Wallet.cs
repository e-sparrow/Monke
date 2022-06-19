using System;
using Game.Counting;
using Game.Counting.Interfaces;
using Game.Counting.Interfaces.Enums;
using Zenject;

namespace Game.Coins
{
    public class Wallet : IWallet, IInitializable, IDisposable
    {
        public Wallet(ICounterContainer counterContainer)
        {
            _counterContainer = counterContainer;
        }

        private ICounterContainer _counterContainer;
        
        private readonly ICounter _moneyCounter = new Counter();
        
        private int _balance = 0;

        [Inject]
        public void Initialize()
        {
            _counterContainer.AddCounter(_moneyCounter, ECountType.Money);
        }

        public void Dispose()
        {
            _counterContainer.RemoveCounter(ECountType.Money);
        }

        public void Add(int amount)
        {
            Balance += amount;
        }

        private int Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                _moneyCounter.Value = _balance;
            }
        }
    }
}