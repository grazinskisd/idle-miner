using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class IdleMinerGameController : IInitializable
    {
        [Inject] private ICanvasController _canvas;
        [Inject] private MineController.Factory _mineFactory;
        [Inject] private GameSettingsScriptable _gameSettings;
        [Inject] private MineSelectionWindowView _selectionWindowView;
        [Inject] private MineSelectionOptionView _selectionOptionView;

        private List<MineController> _mines = new List<MineController>();

        public void Initialize()
        {
            CreateSelectionWindow();
            PopulateWindowWithOptions();
        }

        private void PopulateWindowWithOptions()
        {
            for (int i = 0; i < _gameSettings.Mines.Length; i++)
            {
                InitializeMine(i);
                AddMineOption(i);
            }
        }

        private void CreateSelectionWindow()
        {
            _selectionWindowView = GameObject.Instantiate(_selectionWindowView);
            _canvas.AddToCanvas(_selectionWindowView.transform, false);
        }

        private void AddMineOption(int index)
        {
            var option = GameObject.Instantiate(_selectionOptionView);
            option.transform.SetParent(_selectionWindowView.OptionsContainer, false);
            option.Text.text = _gameSettings.Mines[index].MineName;
            option.Button.onClick.AddListener(() => LaunchMine(index));
        }

        private void InitializeMine(int index)
        {
            var mine = _mineFactory.Create(_gameSettings.Mines[index].ToMinePrametersObject());
            mine.OnExit += ShowSelectionView;
            _mines.Add(mine);
        }

        private void LaunchMine(int index)
        {
            _mines[index].EnterMine();
            HideSelectionView();
        }

        private void HideSelectionView()
        {
            _selectionWindowView.gameObject.SetActive(false);
        }

        private void ShowSelectionView()
        {
            _selectionWindowView.gameObject.SetActive(true);
        }
    }
}
