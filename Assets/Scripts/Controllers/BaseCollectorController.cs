using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public abstract class BaseCollectorController : BaseController
    {
        [Inject] private ITickManager _tick;

        private delegate void CollectorStateFunction();

        private int _load;
        private int _loadInProgress;
        private float _elapsedTime;
        private CollectorStateFunction _state;
        private Destination _currentDestination;
        private int _collectionDestinationIndex;
        private CollectorSettings _settings;

        private Transform CollectorTransform
        {
            get { return GetCollectorView().transform; }
        }

        [Inject]
        protected virtual void Initialize(CollectorSettings settings)
        {
            _settings = settings;
            _state = Move;
            _collectionDestinationIndex = 0;
            _currentDestination = settings.CollectionDestinations[0];
            _tick.OnTick += Update;
        }

        private void Update()
        {
            _state();
        }

        private void Move()
        {
            CollectorTransform.localPosition = NextPosition();
            if (CollectorTransform.localPosition == _currentDestination.Position)
            {
                if (_currentDestination == _settings.DepositDestination)
                {
                    _state = Deposit;
                }
                else
                {
                    _state = Collect;
                    var remainingCapacity = _settings.Parameters.LoadCapacity - _load;
                    _loadInProgress = _currentDestination.Storage.GetPossibleWithdrawal(remainingCapacity);
                    Debug.Log(GetCollectorView().name + " " + _loadInProgress);
                }
            }
        }

        private void Deposit()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= GetDepositTime())
            {
                _elapsedTime = 0;
                _currentDestination.Storage.DepositLoad(_load);
                _load = 0;
                _collectionDestinationIndex = 0;
                _currentDestination = _settings.CollectionDestinations[0];
                _state = Move;
            }
        }

        private void Collect()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= (_loadInProgress / _settings.Parameters.LoadSpeed))
            {
                _elapsedTime = 0;
                _state = Move;
                _load += _currentDestination.Storage.WithdrawLoad(_loadInProgress);
                _currentDestination = GetNextDestinationAfterCollection();
            }
        }

        private Destination GetNextDestinationAfterCollection()
        {
            _collectionDestinationIndex = (_collectionDestinationIndex + 1) % _settings.CollectionDestinations.Count;
            if (_collectionDestinationIndex == 0 || _load == _settings.Parameters.LoadCapacity)
            {
                return _settings.DepositDestination;
            }
            else
            {
                return _settings.CollectionDestinations[_collectionDestinationIndex];
            }
        }

        private Vector3 NextPosition()
        {
            return Vector3.MoveTowards(CollectorTransform.localPosition, _currentDestination.Position, Time.deltaTime * _settings.Parameters.MoveSpeed);
        }

        protected virtual float GetDepositTime()
        {
            return _load / _settings.Parameters.LoadSpeed;
        }

        protected override GameObject GetView()
        {
            return GetCollectorView().gameObject;
        }

        protected abstract CollectorView GetCollectorView();
    }

    public abstract class CollectorState
    {
        public abstract void Enter();
        public abstract void Update();
    }

    public class CollectorSettings
    {
        public List<Destination> CollectionDestinations;
        public Destination DepositDestination;
        public Parameters Parameters;
    }
}
