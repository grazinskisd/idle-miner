using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/MineParameters")]
    public class MineParameters: ScriptableObject
    {
        public Parameters WarehouseParams;
        public Parameters LiftParams;
        public Parameters MineshaftParams;
    }
}
