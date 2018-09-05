using UnityEngine;
using Zenject;

namespace IdleMiner
{
    public class IdleMinerMonoInstsaller : MonoInstaller<IdleMinerMonoInstsaller>
    {
        public MineshaftView MineshaftView;
        public MineView MineView;
        public LiftView LiftView;
        public MinerView MinerView;
        public MineParameters MineParameters;

        public override void InstallBindings()
        {
            Container.BindInstance(MineView);
            Container.BindInstance(MineParameters);
            Container.BindInstance(MineshaftView);
            Container.BindInstance(LiftView);
            Container.BindInstance(MinerView);

            Container.BindInterfacesAndSelfTo<TickManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CanvasController>().AsSingle();
            Container.BindInterfacesAndSelfTo<IdleMinerController>().AsSingle();

            Container.BindFactory<MineParameters, MineController, MineController.Factory>().AsSingle();
            Container.BindFactory<Parameters, MineshaftController, MineshaftController.Factory>().AsSingle();
            Container.BindFactory<Parameters, LiftController, LiftController.Factory>().AsSingle();
            Container.BindFactory<CollectorSettings, MinerController, MinerController.Factory>().AsSingle();
        }
    }
}