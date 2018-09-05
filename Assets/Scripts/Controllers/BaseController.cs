using UnityEngine;

namespace IdleMiner
{
    public abstract class BaseController
    {
        protected abstract GameObject GetView();

        public void SetParent(Transform parent, bool worldPositionStays = true)
        {
            GetView().transform.SetParent(parent, worldPositionStays);
        }
    }
}
