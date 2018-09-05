using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class IdleMinerMonoInstsaller : MonoInstaller<IdleMinerMonoInstsaller>
    {
        public MineView MineView;
        public MineParameters MineParameters;

        public override void InstallBindings()
        {
            Container.BindInstance(MineView);
            Container.BindInstance(MineParameters);
            Container.BindInterfacesAndSelfTo<CanvasController>().AsSingle();
            Container.BindInterfacesAndSelfTo<IdleMinerController>().AsSingle();
            Container.BindFactory<MineParameters, MineController, MineController.Factory>().AsSingle();
        }
    }
}