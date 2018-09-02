using UnityEngine;
using System;

namespace IdleMiner
{
    public class Miner : MonoBehaviour
    {
        public Destination[] MoveDestinations;
        public float MoveSpeed;
        public float MiningTime;
        public float DepositTime;

        private int _destinationIndex;
        private Vector3 _startLocation;
        private float _elapsedTime;

        private delegate void StateFunction();
        private StateFunction _state;

        private Destination Destination
        {
            get
            {
                return MoveDestinations[_destinationIndex];
            }
        }
        
        void Start()
        {
            _state = Move;
            _startLocation = transform.position;
            _destinationIndex = 0;
        }
        
        void Update()
        {
            _state();
        }

        private void Move()
        {
            _elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(_startLocation, Destination.Position, _elapsedTime * MoveSpeed);
            if (transform.position == Destination.Position)
            {
                _startLocation = transform.position;
                _destinationIndex = (_destinationIndex + 1) % MoveDestinations.Length;
                _elapsedTime = 0;
                if(Destination.Type == DestinationType.Collection)
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
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= DepositTime)
            {
                _elapsedTime = 0;
                _state = Move;
            }
        }

        private void Collect()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= MiningTime)
            {
                _elapsedTime = 0;
                _state = Move;
            }
        }
    }

    public enum MinerState
    {
        Move,
        Mine,
        Deposit
    }
}