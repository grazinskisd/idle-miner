using Zenject;

namespace IdleMiner
{
    public class TickManager : ITickable, ITickManager
    {
        public event TickEventHandler OnTick;

        public void Tick()
        {
            if(OnTick != null)
            {
                OnTick();
            }
        }
    }
}
