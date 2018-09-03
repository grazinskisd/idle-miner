using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class Storage: MonoBehaviour
    {
        [SerializeField]
        private Text _loadDisplay;

        private int _currentLoad = 0;

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

        private void UpdateTextDisplay()
        {
            if (_loadDisplay != null)
            {
                _loadDisplay.text = _currentLoad.ToString();
            }
        }
    }
}
