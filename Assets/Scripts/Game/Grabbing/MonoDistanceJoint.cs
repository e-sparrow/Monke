using Game.Grabbing.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Grabbing
{
    public class MonoDistanceJoint : MonoBehaviour, IJoint
    {
        [SerializeField] private Rigidbody2D jointRigidbody;

        private IJointSystem _jointSystem;

        [Inject]
        private void Construct(IJointSystem jointSystem)
        {
            _jointSystem = jointSystem;
        }

        private void OnEnable()
        {
            _jointSystem.Register(this);
        }

        private void OnDisable()
        {
            _jointSystem.Unregister(this);
        }

        public Rigidbody2D Rigidbody => jointRigidbody;
        public Vector3 Position => jointRigidbody.position;
    }
}