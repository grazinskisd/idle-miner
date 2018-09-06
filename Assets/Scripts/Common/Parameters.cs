using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/Parameters")]
    public class Parameters : ScriptableObject
    {
        public int InstanceID;
        public int Level;
        public int LoadCapacity;
        public float LoadSpeed;
        public float MoveSpeed;
        public int TransporterCount;

        public float LoadCapacityLevelMultiplier;
        public float LoadSpeedLevelMultiplier;
        public float MoveSpeedLevelMultiplier;
        public float TransporterCountLevelMultiplier;

        public void UpdateToLevel(int level)
        {
            Level = level;
            LoadCapacity = (int)(LoadCapacity * LoadCapacityLevelMultiplier * level);
            LoadSpeed = LoadSpeed * LoadSpeedLevelMultiplier * level;
            MoveSpeed = MoveSpeed * MoveSpeedLevelMultiplier * level;
            TransporterCount = (int)(TransporterCount * TransporterCountLevelMultiplier * level);
        }

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
