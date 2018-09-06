namespace IdleMiner
{
    public delegate void MineControllerEventHandler();
    public interface IMineController
    {
        event MineControllerEventHandler OnExit;
        void EnterMine();
    }
}
