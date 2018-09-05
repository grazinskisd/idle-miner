using System;
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

        [Inject]
        private void Initialize(MineParameters parameters)
        {
            _params = parameters;
            CreateMineViewGameObject();
            AddFloors();
        }

        private void AddFloors()
        {
            _lift = _liftFactory.Create(_params.LiftParams);
            _lift.SetParent(_mineView.LiftShaft, false);

            Parameters[] mineshafts = _params.MineshaftParams;
            float floorHeight = _mineView.LiftFloor.GetComponent<RectTransform>().sizeDelta.y;
            for (int i = 0; i < mineshafts.Length; i++)
            {
                var position = GetFloorPosition(i, floorHeight);
                var liftFloor = AddFloorPart(_mineView.LiftFloor.gameObject, _mineView.LiftShaft, position);

                var mineshaftPlace = AddFloorPart(_mineView.MineshaftPlaceholder, _mineView.Mineshafts, position);
                _mineshafts[i] = _mineshaftFactory.Create(mineshafts[i]);
                _mineshafts[i].SetParent(mineshaftPlace.transform, false);
            }
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
