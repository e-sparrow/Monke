using System;
using System.Collections.Generic;
using Game.Camera.Interfaces;
using Game.UI.Interfaces;

namespace Game.UI
{
    public abstract class ClickablePanelSystemBase : IClickablePanelSystem
    {
        protected ClickablePanelSystemBase(ICameraTarget cameraTarget)
        {
            _cameraTarget = cameraTarget;
        }

        private readonly ICameraTarget _cameraTarget;
        
        private readonly IDictionary<IClickablePanelView, Tuple<IClickablePanelModel, ClickablePanelPresenter>> _panels 
            = new Dictionary<IClickablePanelView, Tuple<IClickablePanelModel, ClickablePanelPresenter>>();

        protected abstract IClickablePanelModel CreateModel(IClickablePanelView view, ICameraTarget cameraTarget);

        public void Register(IClickablePanelView view)
        {
            var model = CreateModel(view, _cameraTarget);
            
            var presenter = new ClickablePanelPresenter(model, view);
            presenter.Initialize();

            _panels.Add(view, new Tuple<IClickablePanelModel, ClickablePanelPresenter>(model, presenter));
        }

        public void Unregister(IClickablePanelView view)
        {
            _panels.Remove(view);
        }
    }
}