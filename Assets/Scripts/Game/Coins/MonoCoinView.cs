using System;
using Game.Interactions.Enums;
using Game.Interactions.Interfaces;
using UnityEngine;

namespace Game.Coins
{
    public class MonoCoinView : MonoBehaviour, ICoinView
    {
        public event Action OnCoinTake = () => { };

        [SerializeField] private ParticleSystem destructionEffect;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IInteractor>(out var interactor))
            {
                if (interactor.Type == EInteractorType.Player)
                {
                    OnCoinTake.Invoke();
                }
            }      
        }

        private void OnDestroy()
        {
            destructionEffect.transform.SetParent(null);
            
            destructionEffect.Play();
            var duration = destructionEffect.main.startLifetime.constant;
            
            Destroy(destructionEffect, duration);
        }
    }
}