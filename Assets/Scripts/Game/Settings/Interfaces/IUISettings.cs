using System.Collections.Generic;
using Game.UI.Interfaces;

namespace Game.Settings.Interfaces
{
    public interface IUISettings
    {
        IEnumerable<IClickablePanelView> ClickablePanelViews
        {
            get;
        }
    }
}