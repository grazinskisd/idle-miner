using Zenject;
using UnityEngine;

namespace IdleMiner
{
    public class IdleMinerController : IInitializable
    {
        [Inject] private MineController.Factory _mineFactory;
        [Inject] private MineParameters _mineParameters;

        private MineController _mine;

        public void Initialize()
        {
            _mine = _mineFactory.Create(_mineParameters);
        }
    }
}
