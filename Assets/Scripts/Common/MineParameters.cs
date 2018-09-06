namespace IdleMiner
{
    [System.Serializable]
    public class MineParameters
    {
        public string MineName;
        public int UnlockPrice;
        public Parameters WarehouseParams;
        public Parameters LiftParams;
        public Parameters[] MineshaftParams;
    }
}
