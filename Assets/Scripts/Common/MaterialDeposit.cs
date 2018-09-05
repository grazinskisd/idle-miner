namespace IdleMiner
{
    public class MaterialDeposit: Storage
    {
        // Infinite load on material deposits, like coal mine
        public override int WithdrawLoad(int capacity)
        {
            return capacity;
        }

        public override int GetPossibleWithdrawal(int capacity)
        {
            return capacity;
        }
    }
}
