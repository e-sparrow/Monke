using System;
using Zenject;

namespace Game.Coins
{
    public class CoinPresenter : IInitializable, IDisposable
    {
        public CoinPresenter(ICoinView view, ICoinModel model)
        {
            _view = view;
            _model = model;
        }

        private readonly ICoinView _view;
        private readonly ICoinModel _model;
        
        public void Initialize()
        {
            _view.OnCoinTake += _model.Take;
        }

        public void Dispose()
        {
            _view.OnCoinTake -= _model.Take;
        }
    }
}