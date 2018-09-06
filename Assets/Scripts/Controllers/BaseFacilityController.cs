using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public abstract class BaseFacilityController : BaseController
    {
        private const string LVL_BUTTON_FORMAT = "Lvl {0}";

        [Inject] private ITickManager _tick;
        [Inject] protected IWalletController _walletController;

        protected abstract List<BaseCollectorController> GetCollectors();
        protected abstract FacilityView GetFacilityView();
        protected abstract void AddCollector();
        protected abstract void CreateFacilityView();

        protected Parameters _params;

        [Inject]
        protected virtual void Initialize(Parameters parameters)
        {
            _params = parameters;
            CreateFacilityView();
            CreateCollectors();
            HookLevelupButton();
            _tick.OnTick += Update;
        }

        private void CreateCollectors()
        {
            for (int i = 0; i < _params.TransporterCount; i++)
            {
                AddCollector();
            }
        }

        private void HookLevelupButton()
        {
            GetFacilityView().LevelUpButton.onClick.AddListener(LevelUp);
            GetFacilityView().NextLevelPriceText.text = _params.NextLevelPrice.ToString();
        }

        private void Update()
        {
            bool canLevelUp = _walletController.WalletStorage.GetCurrentLoad() >= _params.NextLevelPrice;
            GetFacilityView().LevelUpButton.interactable = canLevelUp;
        }

        private void LevelUp()
        {
            _walletController.WalletStorage.WithdrawLoad(_params.NextLevelPrice);
            _params.IncrementLevel();
            GetFacilityView().LevelUpButtonText.text = string.Format(LVL_BUTTON_FORMAT, _params.Level);
            GetFacilityView().NextLevelPriceText.text = _params.NextLevelPrice.ToString();
            if (GetCollectors().Count < _params.TransporterCount)
            {
                AddCollector();
            }
        }

        public override void Pause()
        {
            var collectors = GetCollectors();
            for (int i = 0; i < collectors.Count; i++)
            {
                collectors[i].Pause();
            }
        }

        public override void Unpause()
        {
            var collectors = GetCollectors();
            for (int i = 0; i < collectors.Count; i++)
            {
                collectors[i].Unpause();
            }
        }

        protected override GameObject GetView()
        {
            return GetFacilityView().gameObject;
        }
    }
}