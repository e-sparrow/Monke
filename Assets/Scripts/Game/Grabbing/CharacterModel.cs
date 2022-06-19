using System;
using ESparrow.Utils.Extensions;
using Game.Grabbing.Interfaces;
using UnityEngine;

namespace Game.Grabbing
{
    [Serializable]
    public class CharacterModel : ICharacterModel
    {
        [SerializeField] private GameObject self;
        [SerializeField] private DistanceJoint2D distanceJoint;
        
        public void Grab(IJoint joint)
        {
            distanceJoint.enabled = true;
            distanceJoint.connectedBody = joint.Rigidbody;
            
            var position = distanceJoint.transform.position.ToVector2() + distanceJoint.anchor;
            var delta = joint.Position.ToVector2() - position;
            distanceJoint.distance = delta.magnitude;
        }

        public void Release()
        {
            distanceJoint.enabled = false;
        }

        [field: SerializeField]
        public Rigidbody2D Rigidbody
        {
            get;
            private set;
        }
        
        public Vector3 Position => self.transform.position;
    }
}