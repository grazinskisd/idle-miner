using UnityEngine;

namespace IdleMiner
{
    public delegate void MinerStateFunction();

    public class Miner: MonoBehaviour
    {
        public CollectionDestination CollectionDestination;
        public Deposit DepositDestination;
        public Parameters Parameters;

        private int _load;
        private Vector3 _startLocation;
        private float _elapsedTime;

        private MinerStateFunction _state;

        private Destination _currentDestination;

        void Start()
        {
            _state = Move;
            _startLocation = transform.localPosition;
            _currentDestination = CollectionDestination;
        }

        void Update()
        {
            _state();
        }

        private void Move()
        {
            _elapsedTime += Time.deltaTime;
            transform.localPosition = NextPosition();
            if (transform.localPosition == _currentDestination.Position)
            {
                _elapsedTime = 0;
                _startLocation = transform.localPosition;
                if (_currentDestination is CollectionDestination)
                {
                    _state = Collect;
                }
                else
                {
                    _state = Deposit;
                }
            }
        }

        private Vector3 NextPosition()
        {
            return Vector3.Lerp(_startLocation, _currentDestination.Position, _elapsedTime * Parameters.MoveSpeed);
        }

        private void Deposit()
        {
            ((Deposit)_currentDestination).DepositLoad(_load);
            _currentDestination = CollectionDestination;
            _state = Move;
        }

        private void Collect()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= Parameters.LoadCapacity / Parameters.LoadSpeed)
            {
                _elapsedTime = 0;
                _state = Move;
                _load = Parameters.LoadCapacity;
                _currentDestination = DepositDestination;
            }
        }
    }
}
