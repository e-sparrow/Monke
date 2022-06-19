using System.Collections.Generic;
using Game.Counting.Interfaces;
using Game.Counting.Interfaces.Enums;

namespace Game.Counting
{
    public class CounterContainer : ICounterContainer
    {
        private readonly IDictionary<ECountType, ICounter> _counters = new Dictionary<ECountType, ICounter>();

        public ICounter GetCounter(ECountType type)
        {
            return _counters[type];
        }

        public void AddCounter(ICounter counter, ECountType type)
        {
            _counters[type] = counter;
        }

        public void RemoveCounter(ECountType type)
        {
            _counters.Remove(type);
        }
    }
}