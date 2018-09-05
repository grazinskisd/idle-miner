using UnityEngine;

namespace IdleMiner
{
    public interface ICanvasController
    {
        void AddToCanvas(Transform child, bool worldPositionStays = true);
    }
}
