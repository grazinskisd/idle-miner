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

        public void Initialize()
        {
            CreateSelectionWindow();
            PopulateWindowWithOptions();
        }

        private void PopulateWindowWithOptions()
        {
            for (int i = 0; i < _gameSettings.Mines.Length; i++)
            {
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

        private void LaunchMine(int index)
        {
            _mineFactory.Create(_gameSettings.Mines[index].ToMinePrametersObject());
            HideSelectionView();
        }

        private void HideSelectionView()
        {
            _selectionWindowView.gameObject.SetActive(false);
        }
    }
}
