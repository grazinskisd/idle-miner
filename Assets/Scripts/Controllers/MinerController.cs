﻿using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class MinerController: BaseCollectorController
    {
        [Inject] private MinerView _minerView;

        protected override void Initialize(CollectorSettings settings)
        {
            base.Initialize(settings);
            _minerView = GameObject.Instantiate(_minerView);
        }

        protected override float GetDepositTime()
        {
            return 0;
        }

        protected override CollectorView GetCollectorView()
        {
            return _minerView;
        }

        // Need to define a new Factory class
        // because the old one would return BaseCollectorController
        public new class Factory: PlaceholderFactory<CollectorSettings, MinerController> { }
    }
}
