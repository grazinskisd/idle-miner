namespace IdleMiner
{
    public class MiningDestination: Destination
    {
        public override int WithdrawLoad(int capacity)
        {
            return capacity;
        }
    }
}
