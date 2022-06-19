using System;
using Game.Camera.Interfaces;
using Game.Settings.Interfaces;
using Game.UI.Enums;
using Game.UI.Interfaces;

namespace Game.UI
{
    public class ClickablePanelSystem : ClickablePanelSystemBase
    {
        public ClickablePanelSystem(ICameraTarget cameraTarget) : base(cameraTarget)
        {
            
        }

        protected override IClickablePanelModel CreateModel(IClickablePanelView view, ICameraTarget cameraTarget)
        {
            if (view.Type == EClickablePanelType.CameraTarget)
            {
                return new CameraTargetClickablePanelModel(view.CameraTargetType, view.Position, cameraTarget);
            }

            throw new ArgumentException();
        }
    }
}