using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Coins
{
    public class CoinModel : ICoinModel
    {
        public CoinModel(GameObject root, int denomination)
        {
            _root = root;
            Denomination = denomination;
        }

        private readonly GameObject _root;
        
        public void Take()
        {
            Object.Destroy(_root);
        }

        public int Denomination
        {
            get;
        }
    }
}