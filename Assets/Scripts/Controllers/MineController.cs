using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class MineController
    {
        [Inject] private ICanvasController _canvas;
        [Inject] private MineView _mineView;
        [Inject] private MineshaftController.Factory _mineshaftFactory;
        [Inject] private LiftController.Factory _liftFactory;

        private MineParameters _params;
        private MineshaftController[] _mineshafts = new MineshaftController[35];

        private LiftController _lift;
        private Destination _liftFloorViewPrototype;

        [Inject]
        private void Initialize(MineParameters parameters)
        {
            _params = parameters;
            CreateMineViewGameObject();
            _liftFloorViewPrototype = _mineView.LiftDepositFloor;
            AddFloors();
        }

        private void AddFloors()
        {
            var _liftFloors = new List<Destination>();

            Parameters[] mineshafts = _params.MineshaftParams;
            float floorHeight = _liftFloorViewPrototype.GetComponent<RectTransform>().sizeDelta.y;
            for (int i = 0; i < mineshafts.Length; i++)
            {
                var position = GetFloorPosition(i, floorHeight);

                var mineshaftPlace = AddFloorPart(_mineView.MineshaftPlaceholder, _mineView.Mineshafts, position);
                _mineshafts[i] = _mineshaftFactory.Create(mineshafts[i]);
                _mineshafts[i].SetParent(mineshaftPlace.transform, false);

                var liftFloor = AddFloorPart(_liftFloorViewPrototype.gameObject, _mineView.LiftShaft, position);
                var floorDestiantion = liftFloor.GetComponent<Destination>();
                floorDestiantion.Storage = _mineshafts[i].GetResourceStorage();
                _liftFloors.Add(floorDestiantion);
            }

            var settings = new CollectorSettings();
            settings.CollectionDestinations = _liftFloors;
            settings.DepositDestination = _mineView.LiftDepositFloor;
            settings.Parameters = _params.LiftParams;
            _lift = _liftFactory.Create(settings);
            _lift.SetParent(_mineView.LiftShaft, false);
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
            _mineView.transform.localScale = Vector3.one;
            var rectTransform = _mineView.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        public class Factory : PlaceholderFactory<MineParameters, MineController> { }
    }
}
