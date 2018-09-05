using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class WorkerController: BaseCollectorController
    {
        [Inject] private WorkerView _workerView;

        protected override void Initialize(CollectorSettings settings)
        {
            base.Initialize(settings);
            _workerView = GameObject.Instantiate(_workerView);
        }

        protected override CollectorView GetCollectorView()
        {
            return _workerView;
        }

        public class Factory: PlaceholderFactory<CollectorSettings, WorkerController> { }
    }
}
