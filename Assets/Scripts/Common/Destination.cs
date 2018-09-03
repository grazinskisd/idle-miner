using UnityEngine;

namespace IdleMiner
{
    public class Destination: MonoBehaviour
    {
        public Storage Storage;

        public Vector3 Position
        {
            get { return transform.localPosition; }
        }
    }
}
