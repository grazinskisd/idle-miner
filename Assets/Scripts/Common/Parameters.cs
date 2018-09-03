using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/Parameters")]
    public class Parameters : ScriptableObject
    {
        public int LoadCapacity;
        public float LoadSpeed;
        public float MoveSpeed;
        public int TransporterCount;

        public float LoadCapacityLevelMultiplier;
        public float LoadSpeedLevelMultiplier;
        public float MoveSpeedLevelMultiplier;
        public float TransporterCountLevelMultiplier;

        public Parameters GetParametersForLevel(int level)
        {
            Parameters newParamms = ScriptableObject.CreateInstance<Parameters>();
            newParamms.LoadCapacity = (int)(LoadCapacity * LoadCapacityLevelMultiplier * level);
            newParamms.LoadSpeed = LoadSpeed * LoadSpeedLevelMultiplier * level;
            newParamms.MoveSpeed = MoveSpeed * MoveSpeedLevelMultiplier * level;
            newParamms.TransporterCount = (int)(TransporterCount * TransporterCountLevelMultiplier *level);
            return newParamms;
        }
    }
}
