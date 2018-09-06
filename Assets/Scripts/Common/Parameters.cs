using UnityEngine;

namespace IdleMiner
{
    [System.Serializable]
    public class Parameters
    {
        public int NextLevelPrice;
        public int Level;
        public int LoadCapacity;
        public float LoadSpeed;
        public float MoveSpeed;
        public int TransporterCount;

        private float _nextLevelPrice;
        private float _loadCapacity;
        private float _transporterCount;

        public float NextLevelPriceLevelMultiplier;
        public float LoadCapacityLevelMultiplier;
        public float LoadSpeedLevelMultiplier;
        public float MoveSpeedLevelMultiplier;
        public float TransporterCountLevelMultiplier;

        public void IncrementLevel()
        {
            SetTemporaryParameters();

            Level++;
            // Update using floats, to not loose fractions when casting back to int
            _nextLevelPrice += NextLevelPriceLevelMultiplier * _nextLevelPrice;
            _loadCapacity += LoadCapacityLevelMultiplier * _loadCapacity;
            _transporterCount += TransporterCountLevelMultiplier * _transporterCount;

            LoadSpeed += LoadSpeedLevelMultiplier * LoadSpeed;
            MoveSpeed += MoveSpeedLevelMultiplier * MoveSpeed;

            NextLevelPrice = (int)_nextLevelPrice;
            LoadCapacity = (int)_loadCapacity;
            TransporterCount = (int)_transporterCount;
        }

        private void SetTemporaryParameters()
        {
            if(_nextLevelPrice == 0)
            {
                _nextLevelPrice = NextLevelPrice;
            }

            if (_loadCapacity == 0)
            {
                _loadCapacity = LoadCapacity;
            }

            if (_transporterCount == 0)
            {
                _transporterCount = TransporterCount;
            }
        }
    }
}
