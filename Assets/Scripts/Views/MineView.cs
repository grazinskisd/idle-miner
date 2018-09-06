using UnityEngine;
using UnityEngine.UI;

namespace IdleMiner
{
    public class MineView: MonoBehaviour
    {
        public LiftShaftView LiftShaft;
        public Transform Mineshafts;
        public GameObject MineshaftPlaceholder;
        public Transform WarehouseContainer;
        public Button ExitButton;
        public Storage LiftDepositSorage;
    }
}
