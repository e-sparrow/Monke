using UnityEngine;

namespace Game.Grabbing.Interfaces
{
    public interface IJointSystem
    {
        void Register(IJoint joint);
        void Unregister(IJoint joint);

        IJoint FindNearestJoint(Vector3 position);
    }
}