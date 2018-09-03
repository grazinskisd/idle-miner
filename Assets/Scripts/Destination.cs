using UnityEngine;

namespace IdleMiner
{
    public class Destination: MonoBehaviour
    {
        public Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
        }
    }
}
