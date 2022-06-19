using UnityEngine;

namespace Game.Grabbing
{
    public interface IJoint
    {
        Rigidbody2D Rigidbody
        {
            get;
        }

        Vector3 Position
        {
            get;
        }
    }
}