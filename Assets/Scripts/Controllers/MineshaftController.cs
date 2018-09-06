using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace IdleMiner
{
    public class MineshaftController: BaseController
    {
        [Inject] private MineshaftView _mineshaftView;
        [Inject] private MinerController.Factory _minerFactory;

        private Parameters _params;
        private List<MinerController> _miners;

        [Inject]
        private void Initialize(Parameters parameters)
        {
            _params = parameters;
            _miners = new List<MinerController>();
            CreateMineshaftGameObject();
            CreteMiners();
        }

        public Storage GetResourceStorage()
        {
            return _mineshaftView.DepositDestination.Storage;
        }

        private void CreteMiners()
        {
            for (int i = 0; i < _params.TransporterCount; i++)
            {
                var settings = new CollectorSettings(_mineshaftView.DepositDestination, _mineshaftView.MiningDestination, _params);
                var miner = _minerFactory.Create(settings);
                miner.SetParent(_mineshaftView.transform);
                _miners.Add(miner);
            }
        }

        private void CreateMineshaftGameObject()
        {
            _mineshaftView = GameObject.Instantiate(_mineshaftView);
        }

        protected override GameObject GetView()
        {
            return _mineshaftView.gameObject;
        }

        public override void Pause()
        {
            for (int i = 0; i < _miners.Count; i++)
            {
                _miners[i].Pause();
            }
        }

        public override void Unpause()
        {
            for (int i = 0; i < _miners.Count; i++)
            {
                _miners[i].Unpause();
            }
        }

        public class Factory: PlaceholderFactory<Parameters, MineshaftController> { }
    }
}
