using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class WarehouseController : BaseController
    {
        [Inject] private WarehouseView _warehouseView;
        [Inject] private WorkerController.Factory _workerFactory;

        private Parameters _params;
        private List<WorkerController> _workers;

        [Inject]
        private void Initialize(Parameters parameters)
        {
            _params = parameters;
            _workers = new List<WorkerController>();
            CreateWarehouseGameObject();
            CreateWorkers();
        }

        private void CreateWorkers()
        {
            for (int i = 0; i < _params.TransporterCount; i++)
            {
                var settings = new CollectorSettings(_warehouseView.DepositDestination, _warehouseView.CollectionDestination, _params);
                var miner = _workerFactory.Create(settings);
                miner.SetParent(_warehouseView.transform);
                _workers.Add(miner);
            }
        }

        public Storage GetResourceStorage()
        {
            return _warehouseView.CollectionDestination.Storage;
        }

        private void CreateWarehouseGameObject()
        {
            _warehouseView = GameObject.Instantiate(_warehouseView);
        }

        protected override GameObject GetView()
        {
            return _warehouseView.gameObject;
        }

        public class Factory: PlaceholderFactory<Parameters, WarehouseController> { }
    }
}
