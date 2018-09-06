using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class WarehouseController : BaseFacilityController
    {
        [Inject] private WarehouseView _warehouseView;
        [Inject] private WorkerController.Factory _workerFactory;

        private List<BaseCollectorController> _workers = new List<BaseCollectorController>();

        protected override List<BaseCollectorController> GetCollectors()
        {
            return _workers;
        }

        protected override FacilityView GetFacilityView()
        {
            return _warehouseView;
        }

        protected override void AddCollector()
        {
            var settings = new CollectorSettings(_warehouseView.DepositDestination, _warehouseView.CollectionDestination, _params);
            var miner = _workerFactory.Create(settings);
            miner.SetParent(_warehouseView.transform);
            _workers.Add(miner);
        }

        protected override void CreateFacilityView()
        {
            _warehouseView = GameObject.Instantiate(_warehouseView);
            _warehouseView.DepositDestination.Storage = _walletController.WalletStorage;
        }

        public void SetCollectionStorage(Storage storage)
        {
            _warehouseView.CollectionDestination.Storage = storage;
        }

        public class Factory: PlaceholderFactory<Parameters, WarehouseController> { }
    }
}
