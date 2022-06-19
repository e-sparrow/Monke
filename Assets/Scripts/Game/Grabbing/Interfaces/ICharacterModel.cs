using System;
using UnityEngine;

namespace Game.Grabbing.Interfaces
{
    public interface ICharacterModel
    {
        void Grab(IJoint joint);
        void Release();

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