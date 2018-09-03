using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class Deposit: Destination
    {
        [SerializeField]
        private Text _loadDisplay;

        private int _currentLoad;

        public void DepositLoad(int load)
        {
            _currentLoad += load;
            _loadDisplay.text = _currentLoad.ToString();
        }
    }
}
