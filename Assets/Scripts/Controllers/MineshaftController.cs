using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace IdleMiner
{
    public class MineshaftController: BaseFacilityController
    {
        [Inject] private MineshaftView _mineshaftView;
        [Inject] private MinerController.Factory _minerFactory;

        private List<BaseCollectorController> _miners = new List<BaseCollectorController>();

        public Storage GetResourceStorage()
        {
            return _mineshaftView.DepositDestination.Storage;
        }

        protected override void AddCollector()
        {
            var settings = new CollectorSettings(_mineshaftView.DepositDestination, _mineshaftView.CollectionDestination, _params);
            var miner = _minerFactory.Create(settings);
            miner.SetParent(_mineshaftView.transform, false);
            _miners.Add(miner);
        }

        protected override void CreateFacilityView()
        {
            _mineshaftView = GameObject.Instantiate(_mineshaftView);
        }

        protected override List<BaseCollectorController> GetCollectors()
        {
            return _miners;
        }

        protected override FacilityView GetFacilityView()
        {
            return _mineshaftView;
        }

        public class Factory: PlaceholderFactory<Parameters, MineshaftController> { }
    }
}
