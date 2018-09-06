using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public abstract class BaseCollectorController : BaseController
    {
        private const int DEPOSIT_INDEX = 0;

        [Inject] private ITickManager _tick;

        private int _load;
        private int _loadInProgress;
        private float _elapsedTime;
        private State _state;
        private State _lastState;
        private int _collectionDestinationIndex;
        protected CollectorSettings _settings;

        private Destination CurrentDestination
        {
            get { return _settings.CollectionDestinations[_collectionDestinationIndex]; }
        }

        private Transform CollectorTransform
        {
            get { return GetCollectorView().transform; }
        }

        [Inject]
        protected virtual void Initialize(CollectorSettings settings)
        {
            _settings = settings;
            _state = State.Move;
            _collectionDestinationIndex = 1;
            _tick.OnTick += Update;
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            switch (_state)
            {
                case State.Move:
                    Move();
                    break;
                case State.Collect:
                    CollectNextLoad();
                    break;
                case State.Deposit:
                    DepositLoad();
                    break;
                default:
                    break;
            }
        }

        public override void Pause()
        {
            _lastState = _state;
            _state = State.Pause;
        }

        public override void Unpause()
        {
            _state = _lastState;
        }

        private void Move()
        {
            CollectorTransform.localPosition = NextPosition();
            if (CollectorTransform.localPosition == CurrentDestination.Position)
            {
                if (_collectionDestinationIndex == DEPOSIT_INDEX)
                {
                    UpdateStateTo(State.Deposit);
                }
                else
                {
                    SetLoadInProgress();
                    UpdateStateTo(State.Collect);
                }
            }
        }

        private void SetLoadInProgress()
        {
            var remainingCapacity = _settings.Parameters.LoadCapacity - _load;
            _loadInProgress = CurrentDestination.Storage.GetPossibleWithdrawal(remainingCapacity);
        }

        private void DepositLoad()
        {
            if (_elapsedTime >= GetDepositTime())
            {
                DepositLoadToStorage();
                GotToMoveState();
            }
        }

        private void DepositLoadToStorage()
        {
            CurrentDestination.Storage.DepositLoad(_load);
            _load = 0;
        }

        private void CollectNextLoad()
        {
            if (_elapsedTime >= GetCollectionTime())
            {
                CollectLoadInProgress();
                GotToMoveState();
            }
        }

        private void GotToMoveState()
        {
            UpdateDestinationIndex();
            UpdateStateTo(State.Move);
        }

        private float GetCollectionTime()
        {
            return _loadInProgress / _settings.Parameters.LoadSpeed;
        }

        private void CollectLoadInProgress()
        {
            _load += CurrentDestination.Storage.WithdrawLoad(_loadInProgress);
        }

        private void UpdateStateTo(State newState)
        {
            _elapsedTime = 0;
            _state = newState;
        }

        private void UpdateDestinationIndex()
        {
            _collectionDestinationIndex = IsCollectorFullyLoaded() ? DEPOSIT_INDEX : GetNextWrappedIndex();
        }

        private int GetNextWrappedIndex()
        {
            return (_collectionDestinationIndex + 1) % _settings.CollectionDestinations.Count;
        }

        private bool IsCollectorFullyLoaded()
        {
            return _load == _settings.Parameters.LoadCapacity;
        }

        private Vector3 NextPosition()
        {
            return Vector3.MoveTowards(CollectorTransform.localPosition, CurrentDestination.Position, Time.deltaTime * _settings.Parameters.MoveSpeed);
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

        private enum State
        {
            Move, Collect, Deposit, Pause
        }
    }
}
