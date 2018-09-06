﻿using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace IdleMiner
{
    public class MineshaftController: BaseController
    {
        private const string LVL_BUTTON_FORMAT = "Lvl {0}";

        [Inject] private MineshaftView _mineshaftView;
        [Inject] private MinerController.Factory _minerFactory;
        [Inject] private IWalletController _walletController;
        [Inject] private ITickManager _tick;

        private Parameters _params;
        private List<MinerController> _miners;

        [Inject]
        private void Initialize(Parameters parameters)
        {
            _params = parameters;
            _miners = new List<MinerController>();
            CreateMineshaftGameObject();
            CreteMiners();
            _tick.OnTick += Update;
        }

        private void Update()
        {
            bool canLevelUp = _walletController.WalletStorage.GetCurrentLoad() >= _params.NextLevelPrice;
            _mineshaftView.LevelUpButton.enabled = canLevelUp;
        }

        public Storage GetResourceStorage()
        {
            return _mineshaftView.DepositDestination.Storage;
        }

        private void CreteMiners()
        {
            for (int i = 0; i < _params.TransporterCount; i++)
            {
                AddMiner();
            }
        }

        private void AddMiner()
        {
            var settings = new CollectorSettings(_mineshaftView.DepositDestination, _mineshaftView.MiningDestination, _params);
            var miner = _minerFactory.Create(settings);
            miner.SetParent(_mineshaftView.transform, false);
            _miners.Add(miner);
        }

        private void CreateMineshaftGameObject()
        {
            _mineshaftView = GameObject.Instantiate(_mineshaftView);
            _mineshaftView.LevelUpButton.onClick.AddListener(LevelUp);
        }

        private void LevelUp()
        {
            _walletController.WalletStorage.WithdrawLoad(_params.NextLevelPrice);
            _params.IncrementLevel();
            _mineshaftView.LevelUpButtonText.text = string.Format(LVL_BUTTON_FORMAT, _params.Level);
            if(_miners.Count < _params.TransporterCount)
            {
                AddMiner();
            }
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
