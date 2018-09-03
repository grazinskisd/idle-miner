using System;
using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class Mineshaft : MonoBehaviour
    {
        public Destination CollectionDestination;
        public Destination DepositDestination;
        public Parameters Parameters;
        public Miner Miner;
        public Button LevelupButton;
        public Text LevelupButtonText;

        private const string LVL_BUTTON_TEXT = "Lvl {0}";
        private int _level = 1;

        private void Start()
        {
            UpdateLvlButtonText();
            LevelupButton.onClick.AddListener(IncrementLevel);
        }

        private void IncrementLevel()
        {
            _level++;
            UpdateLvlButtonText();
            Miner.Parameters = Parameters.GetParametersForLevel(_level);
        }

        private void UpdateLvlButtonText()
        {
            LevelupButtonText.text = string.Format(LVL_BUTTON_TEXT, _level);
        }

        private void Update()
        {
        }
    }
}