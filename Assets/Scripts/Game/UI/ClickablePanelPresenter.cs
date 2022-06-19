using System;
using Game.UI.Interfaces;
using Zenject;

namespace Game.UI
{
    public class ClickablePanelPresenter : IInitializable, IDisposable
    {
        public ClickablePanelPresenter(IClickablePanelModel model, IClickablePanelView view)
        {
            _model = model;
            _view = view;
        }

        private readonly IClickablePanelModel _model;
        private readonly IClickablePanelView _view;
        
        public void Initialize()
        {
            _view.OnClick += _model.Click;
        }
        
        public void Dispose()
        {
            _view.OnClick -= _model.Click;
        }
    }
}