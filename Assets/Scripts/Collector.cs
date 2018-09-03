using System.Collections.Generic;
using UnityEngine;

namespace IdleMiner
{
    public delegate void CollectorStateFunction();

    public class Collector: MonoBehaviour
    {
        public List<Destination> CollectionDestinations;
        public Destination DepositDestination;
        public Parameters Parameters;

        protected int _load;
        protected Vector3 _startLocation;
        protected float _elapsedTime;
        protected CollectorStateFunction _state;
        protected Destination _currentDestination;
        protected int _collectionDestinationIndex;

        void Start()
        {
            _state = Move;
            _startLocation = transform.localPosition;
            _collectionDestinationIndex = 0;
            _currentDestination = CollectionDestinations[0];
        }

        void Update()
        {
            _state();
        }

        protected virtual void Move()
        {
            _elapsedTime += Time.deltaTime;
            transform.localPosition = NextPosition();
            if (transform.localPosition == _currentDestination.Position)
            {
                _elapsedTime = 0;
                _startLocation = transform.localPosition;
                if (_currentDestination == DepositDestination)
                {
                    _state = Deposit;
                }
                else
                {
                    _state = Collect;
                }
            }
        }

        protected virtual void Deposit()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= GetDepositTime())
            {
                _elapsedTime = 0;
                _currentDestination.DepositLoad(_load);
                _collectionDestinationIndex = 0;
                _currentDestination = CollectionDestinations[0];
                _state = Move;
            }
        }

        protected virtual float GetDepositTime()
        {
            return _load / Parameters.LoadSpeed;
        }

        protected virtual void Collect()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= Parameters.LoadCapacity / Parameters.LoadSpeed)
            {
                _elapsedTime = 0;
                _state = Move;
                _load = _currentDestination.WithdrawLoad(Parameters.LoadCapacity);
                _currentDestination = GetNextDestinationAfterCollection();
            }
        }

        private Destination GetNextDestinationAfterCollection()
        {
            _collectionDestinationIndex = (_collectionDestinationIndex + 1) % CollectionDestinations.Count;
            if(_collectionDestinationIndex == 0)
            {
                return DepositDestination;
            }
            else
            {
                return CollectionDestinations[_collectionDestinationIndex];
            }
        }

        private Vector3 NextPosition()
        {
            return Vector3.Lerp(_startLocation, _currentDestination.Position, _elapsedTime * Parameters.MoveSpeed);
        }
    }
}
