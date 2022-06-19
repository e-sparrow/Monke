using System;
using Game.Grabbing.Interfaces;
using Game.Interactions.Enums;
using Game.Interactions.Interfaces;
using UnityEngine;

namespace Game.Grabbing
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        public event Action<IInteractor> OnHit = _ => { };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent<IInteractor>(out var interactor))
            {
                OnHit.Invoke(interactor);
            }
        }
    }
}