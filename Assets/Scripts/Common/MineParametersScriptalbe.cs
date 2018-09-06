using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/MineParameters")]
    public class MineParametersScriptalbe: ScriptableObject
    {
        public string MineName;
        public int UnlockPrice;
        public ParametersScriptable WarehouseParams;
        public ParametersScriptable LiftParams;
        public ParametersScriptable[] MineshaftParams;

        public MineParameters ToMinePrametersObject()
        {
            MineParameters result = new MineParameters();

            result.MineName = MineName;
            result.UnlockPrice = UnlockPrice;
            result.WarehouseParams = WarehouseParams.ToParametersObjct();
            result.LiftParams = LiftParams.ToParametersObjct();
            result.MineshaftParams = new Parameters[MineshaftParams.Length];
            for (int i = 0; i < MineshaftParams.Length; i++)
            {
                result.MineshaftParams[i] = MineshaftParams[i].ToParametersObjct();
            }

            return result;
        }
    }
}
