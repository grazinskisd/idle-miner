using System;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class MineController
    {
        [Inject] private ICanvasController _canvas;
        [Inject] private MineView _mineView;
        private MineParameters _params;

        [Inject]
        private void Initialize(MineParameters parameters)
        {
            _params = parameters;
            CreateMineViewGameObject();
        }

        private void CreateMineViewGameObject()
        {
            _mineView = GameObject.Instantiate(_mineView);
            _canvas.AddToCanvas(_mineView.transform);
            _mineView.transform.localScale = Vector3.one;
            var rectTransform = _mineView.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        public class Factory : PlaceholderFactory<MineParameters, MineController> { }
    }
}
