using System;
using Game.Camera.Enums;
using Game.UI.Enums;
using UnityEngine;

namespace Game.UI.Interfaces
{
    public interface IClickablePanelView
    {
        event Action OnClick;

        Vector3 Position
        {
            get;
        }

        EClickablePanelType Type
        {
            get;
        }

        ECameraTargetType CameraTargetType
        {
            get;
        }
    }
}