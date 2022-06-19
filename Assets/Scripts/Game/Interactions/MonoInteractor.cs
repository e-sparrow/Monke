using Game.Interactions.Enums;
using Game.Interactions.Interfaces;
using UnityEngine;

namespace Game.Interactions
{
    public class MonoInteractor : MonoBehaviour, IInteractor
    {
        [field: SerializeField]
        public EInteractorType Type
        {
            get;
            private set;
        }
    }
}