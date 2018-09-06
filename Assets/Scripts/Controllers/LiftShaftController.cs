using System.Collections.Generic;
using Zenject;

namespace IdleMiner
{
    public class LiftShaftController : BaseFacilityController
    {
        [Inject] private LiftController.Factory _liftFactory;
        [Inject] private LiftShaftView _liftShaftView;

        private List<BaseCollectorController> _collectors = new List<BaseCollectorController>();
        private LiftController _lift;

        protected override void AddCollector()
        {
            var settings = new CollectorSettings(_liftShaftView.DepositDestination, _params);
            _lift = _liftFactory.Create(settings);
            _lift.SetParent(_liftShaftView.transform, false);
            _collectors.Add(_lift);
        }

        protected override void CreateFacilityView()
        {
            // Facility view comes as a factory parameter
        }

        protected override List<BaseCollectorController> GetCollectors()
        {
            return _collectors;
        }

        protected override FacilityView GetFacilityView()
        {
            return _liftShaftView;
        }

        public void AddLiftFloor(Destination floorDestiantion)
        {
            _lift.AddLiftFloor(floorDestiantion);
        }

        public class Factory : PlaceholderFactory<Parameters, LiftShaftView, LiftShaftController> { }
    }
}