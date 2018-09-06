namespace IdleMiner
{
    [System.Serializable]
    public class Parameters
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
    }
}
