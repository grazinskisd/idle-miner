using UnityEngine;

namespace IdleMiner
{
    public class Mine : MonoBehaviour
    {
        public CollectionDestination CollectionDestination;
        public Deposit DepositDestination;
        public Transform Miner;
        public MineParameters Parameters;

        private int _load;
        private Vector3 _startLocation;
        private float _elapsedTime;

        private delegate void StateFunction();
        private StateFunction _state;

        private Destination _currentDestination;

        void Start()
        {
            _state = Move;
            _startLocation = Miner.position;
            _currentDestination = CollectionDestination;
        }

        void Update()
        {
            _state();
        }

        private void Move()
        {
            _elapsedTime += Time.deltaTime;
            Miner.position = Vector3.Lerp(_startLocation, _currentDestination.Position, _elapsedTime * Parameters.WalkingSpeed);
            if (Miner.position == _currentDestination.Position)
            {
                _elapsedTime = 0;
                _startLocation = Miner.position;
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

        private void Deposit()
        {
            ((Deposit)_currentDestination).DepositLoad(_load);
            _currentDestination = CollectionDestination;
            _state = Move;
        }

        private void Collect()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= Parameters.WalkerCapacity / Parameters.MiningSpeed)
            {
                _elapsedTime = 0;
                _state = Move;
                _load = Parameters.WalkerCapacity;
                _currentDestination = DepositDestination;
            }
        }
    }
}