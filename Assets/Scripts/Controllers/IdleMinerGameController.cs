using Zenject;

namespace IdleMiner
{
    public class IdleMinerGameController : IInitializable
    {
        [Inject] private MineController.Factory _mineFactory;
        [Inject] private GameSettingsScriptable _gameSettings;

        public void Initialize()
        {
            _mineFactory.Create(_gameSettings.Mines[0].ToMinePrametersObject());
        }

    }
}
