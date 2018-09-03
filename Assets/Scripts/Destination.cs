using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class Destination: MonoBehaviour
    {
        [SerializeField]
        private Text _loadDisplay;

        private int _currentLoad;

        public void DepositLoad(int load)
        {
            _currentLoad += load;
            UpdateTextDisplay();
        }

        public virtual int WithdrawLoad(int capacity)
        {
            int load = _currentLoad - _currentLoad % capacity;
            _currentLoad -= load;
            UpdateTextDisplay();
            return load;
        }

        public Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
        }

        private void UpdateTextDisplay()
        {
            if (_loadDisplay != null)
            {
                _loadDisplay.text = _currentLoad.ToString();
            }
        }
    }
}
