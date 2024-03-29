﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class MineController: IMineController
    {
        [Inject] private ICanvasController _canvas;
        [Inject] private MineView _mineView;
        [Inject] private MineshaftController.Factory _mineshaftFactory;
        [Inject] private LiftShaftController.Factory _liftFactory;
        [Inject] private WarehouseController.Factory _warehouseFactory;

        public event MineControllerEventHandler OnExit;

        private MineParameters _params;
        private List<MineshaftController> _mineshafts = new List<MineshaftController>();
        private LiftShaftController _lift;
        private RectTransform _liftFloorPrototype;
        private WarehouseController _warehouse;

        [Inject]
        private void Initialize(MineParameters parameters)
        {
            _params = parameters;
            CreateMineViewGameObject();
            InitializeWarehouse();
            InitializeLift();
            AddFloors();
            DeactivateMine();
        }

        private void InitializeWarehouse()
        {
            _warehouse = _warehouseFactory.Create(_params.WarehouseParams);
            _warehouse.SetParent(_mineView.WarehouseContainer, false);
            _warehouse.SetCollectionStorage(_mineView.LiftDepositSorage);
        }

        private void InitializeLift()
        {
            _lift = _liftFactory.Create(_params.LiftParams, _mineView.LiftShaft);
            _liftFloorPrototype = _mineView.LiftShaft.DepositDestination.GetComponent<RectTransform>();
        }

        private void AddFloors()
        {
            float floorHeight = _liftFloorPrototype.sizeDelta.y;
            for (int i = 0; i < _params.MineshaftParams.Length; i++)
            {
                CreateFloor(i, GetFloorPosition(i, floorHeight));
            }
        }

        private void CreateFloor(int i, Vector3 position)
        {
            CreateMineshaft(_params.MineshaftParams, i, position);
            CreateLiftFloor(i, position);
        }

        private void CreateLiftFloor(int i, Vector3 position)
        {
            var liftFloor = AddFloorPart(_liftFloorPrototype.gameObject, _mineView.LiftShaft.transform, position);
            var floorDestiantion = liftFloor.GetComponent<Destination>();
            floorDestiantion.Storage = _mineshafts[i].GetResourceStorage();
            _lift.AddLiftFloor(floorDestiantion);
        }

        private void CreateMineshaft(Parameters[] mineshafts, int i, Vector3 position)
        {
            var mineshaftPlace = AddFloorPart(_mineView.MineshaftPlaceholder, _mineView.Mineshafts, position);
            var mineshaft = _mineshaftFactory.Create(mineshafts[i]);
            mineshaft.SetParent(mineshaftPlace.transform, false);
            _mineshafts.Add(mineshaft);
        }

        private GameObject AddFloorPart(GameObject part, Transform parent, Vector3 localPosition)
        {
            var partGO = GameObject.Instantiate(part);
            partGO.transform.SetParent(parent.transform, false);
            partGO.transform.localPosition = localPosition;
            return partGO;
        }

        private Vector3 GetFloorPosition(int index, float floorHeight)
        {
            return new Vector3(0, -floorHeight * (index + 1), 0);
        }

        private void CreateMineViewGameObject()
        {
            _mineView = GameObject.Instantiate(_mineView);
            _canvas.AddToCanvas(_mineView.transform, false);
            _mineView.ExitButton.onClick.AddListener(ProcessExit);
        }

        private void ProcessExit()
        {
            DeactivateMine();
            if (OnExit != null)
            {
                OnExit();
            }
        }

        private void DeactivateMine()
        {
            PauseMine();
            _mineView.gameObject.SetActive(false);
        }

        private void PauseMine()
        {
            _lift.Pause();
            _warehouse.Pause();
            for (int i = 0; i < _mineshafts.Count; i++)
            {
                _mineshafts[i].Pause();
            }
        }

        public void EnterMine()
        {
            _mineView.gameObject.SetActive(true);
            UppauseMine();
        }

        private void UppauseMine()
        {
            _lift.Unpause();
            _warehouse.Unpause();
            for (int i = 0; i < _mineshafts.Count; i++)
            {
                _mineshafts[i].Unpause();
            }
        }

        public class Factory : PlaceholderFactory<MineParameters, MineController> { }
    }
}
