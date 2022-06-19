using System;
using Game.Counting.Interfaces;

namespace Game.Counting
{
    public class Counter : ICounter
    {
        public event Action<int> OnValueChanged = _ => { };
        
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged.Invoke(_value);
            }
        }
    }
}