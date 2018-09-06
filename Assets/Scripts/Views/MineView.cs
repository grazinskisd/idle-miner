using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class MineView: MonoBehaviour
    {
        public Transform LiftShaft;
        public Destination LiftDepositFloor;
        public Transform Mineshafts;
        public GameObject MineshaftPlaceholder;
        public Transform WarehouseContainer;
        public Button ExitButton;
        public Storage LiftDepositSorage;
    }
}
