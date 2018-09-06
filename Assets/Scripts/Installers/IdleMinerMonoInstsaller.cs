using Zenject;

namespace IdleMiner
{
    public class IdleMinerMonoInstsaller : MonoInstaller<IdleMinerMonoInstsaller>
    {
        public MineshaftView MineshaftView;
        public MineView MineView;
        public LiftView LiftView;
        public MinerView MinerView;
        public WorkerView WorkerView;
        public WarehouseView WarehouseView;
        public GameSettingsScriptable GameParameters;
        public MineSelectionWindowView MineSelectionWindowView;
        public MineSelectionOptionView MineSelectionOptionView;

        public override void InstallBindings()
        {
            BindInstances();

            Container.BindInterfacesAndSelfTo<TickManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CanvasController>().AsSingle();
            Container.BindInterfacesAndSelfTo<IdleMinerGameController>().AsSingle();

            Container.BindFactory<MineParameters, MineController, MineController.Factory>().AsSingle();
            Container.BindFactory<Parameters, MineshaftController, MineshaftController.Factory>().AsSingle();
            Container.BindFactory<Parameters, WarehouseController, WarehouseController.Factory>().AsSingle();

            Container.BindFactory<CollectorSettings, LiftController, LiftController.Factory>().AsSingle();
            Container.BindFactory<CollectorSettings, WorkerController, WorkerController.Factory>().AsSingle();
            Container.BindFactory<CollectorSettings, MinerController, MinerController.Factory>().AsSingle();
        }

        private void BindInstances()
        {
            Container.BindInstance(MineView);
            Container.BindInstance(GameParameters);
            Container.BindInstance(MineshaftView);
            Container.BindInstance(LiftView);
            Container.BindInstance(MinerView);
            Container.BindInstance(WarehouseView);
            Container.BindInstance(WorkerView);
            Container.BindInstance(MineSelectionWindowView);
            Container.BindInstance(MineSelectionOptionView);
        }
    }
}