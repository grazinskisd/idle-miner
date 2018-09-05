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
            InitializeLift();
            AddFloors();
        }

        private void InitializeLift()
        {
            _liftFloorViewPrototype = _mineView.LiftDepositFloor;
            var settings = new CollectorSettings(_mineView.LiftDepositFloor, _params.LiftParams);
            _lift = _liftFactory.Create(settings);
            _lift.SetParent(_mineView.LiftShaft, false);
        }

        private void AddFloors()
        {
            float floorHeight = _liftFloorViewPrototype.GetComponent<RectTransform>().sizeDelta.y;
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
            var liftFloor = AddFloorPart(_liftFloorViewPrototype.gameObject, _mineView.LiftShaft, position);
            var floorDestiantion = liftFloor.GetComponent<Destination>();
            floorDestiantion.Storage = _mineshafts[i].GetResourceStorage();
            _lift.AddLiftFloor(floorDestiantion);
        }

        private void CreateMineshaft(Parameters[] mineshafts, int i, Vector3 position)
        {
            var mineshaftPlace = AddFloorPart(_mineView.MineshaftPlaceholder, _mineView.Mineshafts, position);
            _mineshafts[i] = _mineshaftFactory.Create(mineshafts[i]);
            _mineshafts[i].SetParent(mineshaftPlace.transform, false);
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
