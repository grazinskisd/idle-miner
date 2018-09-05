using Zenject;
using UnityEngine;

namespace IdleMiner
{
    public class LiftController: BaseController
    {
        [Inject] private LiftView _liftView;

        private Parameters _params;

        [Inject]
        private void Initialize(Parameters parameters)
        {
            _params = parameters;
            _liftView = GameObject.Instantiate(_liftView);
        }

        protected override GameObject GetView()
        {
            return _liftView.gameObject;
        }

        public class Factory: PlaceholderFactory<Parameters, LiftController> { }
    }
}
