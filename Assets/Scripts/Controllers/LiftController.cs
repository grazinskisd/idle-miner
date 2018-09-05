using Zenject;
using UnityEngine;

namespace IdleMiner
{
    public class LiftController: BaseCollectorController
    {
        [Inject] private LiftView _liftView;

        private Parameters _params;

        protected override void Initialize(CollectorSettings settings)
        {
            base.Initialize(settings);
            _liftView = GameObject.Instantiate(_liftView);
        }

        protected override CollectorView GetCollectorView()
        {
            return _liftView;
        }

        public class Factory: PlaceholderFactory<CollectorSettings, LiftController> { }
    }
}
