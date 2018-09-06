using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class WalletController : IInitializable, IWalletController
    {
        [Inject] private ICanvasController _canvas;
        [Inject] private WalletView _walletView;

        public Storage WalletStorage { get; private set; }

        public void Initialize()
        {
            _walletView = GameObject.Instantiate(_walletView);
            _canvas.AddToCanvas(_walletView.transform, false);
            WalletStorage = _walletView.Storage;
        }
    }
}
