using System;
using System.Collections.Generic;
using Game.Settings.Interfaces;
using Game.UI;
using Game.UI.Interfaces;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class SerializableUISettings : IUISettings
    {
        [SerializeField] private List<MonoCameraTargetClickablePanelView> clickablePanelViews;

        public IEnumerable<IClickablePanelView> ClickablePanelViews => clickablePanelViews;
    }
}