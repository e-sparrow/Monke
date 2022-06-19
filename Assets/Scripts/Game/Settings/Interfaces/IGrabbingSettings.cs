using Game.Grabbing;
using UnityEngine;

namespace Game.Settings.Interfaces
{
    public interface IGrabbingSettings
    {
        CharacterModel CharacterModel
        {
            get;
        }

        CharacterView CharacterView
        {
            get;
        }

        LineRenderer LineRenderer
        {
            get;
        }
    }
}