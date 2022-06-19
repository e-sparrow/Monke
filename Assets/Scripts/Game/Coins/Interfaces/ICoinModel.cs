namespace Game.Coins
{
    public interface ICoinModel
    {
        void Take();
        
        int Denomination
        {
            get;
        }
    }
}