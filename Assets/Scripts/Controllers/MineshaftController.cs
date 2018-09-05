using Zenject;
using UnityEngine;

namespace IdleMiner
{
    public class MineshaftController: BaseController
    {
        [Inject] private MineshaftView _mineshaftView;

        private Parameters _params;

        [Inject]
        public void Initialize(Parameters parameters)
        {
            _params = parameters;
            CreateMinesjaftGameOBject();
        }

        private void CreateMinesjaftGameOBject()
        {
            _mineshaftView = GameObject.Instantiate(_mineshaftView);
        }

        protected override GameObject GetView()
        {
            return _mineshaftView.gameObject;
        }

        public class Factory: PlaceholderFactory<Parameters, MineshaftController> { }
    }
}
