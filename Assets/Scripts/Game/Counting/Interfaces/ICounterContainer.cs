using Game.Counting.Interfaces.Enums;

namespace Game.Counting.Interfaces
{
    public interface ICounterContainer
    {
        ICounter GetCounter(ECountType type);

        void AddCounter(ICounter counter, ECountType type);
        void RemoveCounter(ECountType type);
    }
}