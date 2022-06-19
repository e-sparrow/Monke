using System.Collections.Generic;
using System.Linq;
using Game.Grabbing.Interfaces;
using UnityEngine;

namespace Game.Grabbing
{
    public class JointSystem : IJointSystem
    {
        private readonly IList<IJoint> _joints = new List<IJoint>();

        public void Register(IJoint joint)
        {
            _joints.Add(joint);
        }

        public void Unregister(IJoint joint)
        {
            _joints.Remove(joint);
        }

        public IJoint FindNearestJoint(Vector3 position)
        {
            var sorted = _joints.OrderBy(value => (value.Position - position).magnitude);
            var target = sorted.First();

            return target;
        }
    }
}