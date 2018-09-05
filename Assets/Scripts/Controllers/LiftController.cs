using Zenject;
using UnityEngine;

namespace IdleMiner
{
    public class LiftController: BaseCollectorController
    {
        [Inject] private LiftView _liftView;

        protected override void Initialize(CollectorSettings settings)
        {
            base.Initialize(settings);
            _liftView = GameObject.Instantiate(_liftView);
        }

        public void AddLiftFloor(Destination floorDestination)
        {
            _settings.CollectionDestinations.Add(floorDestination);
        }

        protected override CollectorView GetCollectorView()
        {
            return _liftView;
        }

        public class Factory: PlaceholderFactory<CollectorSettings, LiftController> { }
    }
}
