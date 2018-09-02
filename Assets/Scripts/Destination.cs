using UnityEngine;

namespace IdleMiner
{
    public class Destination: MonoBehaviour
    {
        [SerializeField]
        private DestinationType _type;
        private Vector3 _position;

        private void Start()
        {
            _position = transform.position;
        }

        public Vector3 Position
        {
            get { return _position; }
        }

        public DestinationType Type
        {
            get { return _type; }
        }
    }

    public enum DestinationType
    {
        Deposit,
        Collection
    }
}
