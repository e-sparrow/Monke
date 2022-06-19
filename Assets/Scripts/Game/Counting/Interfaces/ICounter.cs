using System;

namespace Game.Counting.Interfaces
{
    public interface ICounter
    {
        event Action<int> OnValueChanged;

        int Value
        {
            get;
            set;
        }
    }
}