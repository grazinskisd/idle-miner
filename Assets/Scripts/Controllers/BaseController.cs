using UnityEngine;

namespace IdleMiner
{
    public abstract class BaseController
    {
        protected abstract GameObject GetView();
        public abstract void Pause();
        public abstract void Unpause();

        public void SetParent(Transform parent, bool worldPositionStays = true)
        {
            GetView().transform.SetParent(parent, worldPositionStays);
        }
    }
}
