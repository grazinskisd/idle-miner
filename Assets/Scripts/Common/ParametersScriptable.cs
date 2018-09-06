using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/Parameters")]
    public class ParametersScriptable : ScriptableObject
    {
        public int Level;
        public int LoadCapacity;
        public float LoadSpeed;
        public float MoveSpeed;
        public int TransporterCount;

        public float LoadCapacityLevelMultiplier;
        public float LoadSpeedLevelMultiplier;
        public float MoveSpeedLevelMultiplier;
        public float TransporterCountLevelMultiplier;

        public Parameters ToParametersObjct()
        {
            Parameters result = new Parameters();

            result.Level = Level;
            result.LoadCapacity = LoadCapacity;
            result.LoadSpeed = LoadSpeed;
            result.MoveSpeed = MoveSpeed;
            result.TransporterCount = TransporterCount;
            result.LoadCapacityLevelMultiplier = LoadCapacityLevelMultiplier;
            result.LoadSpeedLevelMultiplier = LoadSpeedLevelMultiplier;
            result.MoveSpeedLevelMultiplier = MoveSpeedLevelMultiplier;
            result.TransporterCountLevelMultiplier = TransporterCountLevelMultiplier;

            return result;
        }
    }
}
