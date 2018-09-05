
namespace IdleMiner
{
    public delegate void TickEventHandler();

    public interface ITickManager
    {
        event TickEventHandler OnTick;
    }
}
