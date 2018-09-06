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
            int load = GetPossibleWithdrawal(capacity);
            _currentLoad -= load;
            UpdateTextDisplay();
            return load;
        }

        public virtual int GetPossibleWithdrawal(int capacity)
        {
            return capacity >= _currentLoad ? _currentLoad : capacity;
        }

        public int GetCurrentLoad()
        {
            return _currentLoad;
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
