using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class CanvasController : IInitializable, ICanvasController
    {
        private Canvas _canvas;

        public void Initialize()
        {
            _canvas = GameObject.FindObjectOfType<Canvas>();
        }

        public void AddToCanvas(Transform child, bool worldPositionStays = true)
        {
            child.SetParent(_canvas.transform);
        }
    }
}
