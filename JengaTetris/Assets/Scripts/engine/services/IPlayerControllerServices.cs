
namespace Assets.Scripts.engine.services
{
    public interface IPlayerControllerServices
    {
        void NotifyMoveLeft();
        void NotifyMoveRight();
        void NotifyMoveDown();
        void NotifyRotateLeft();
        void NotifyRotateRight();
        void NotifyPause(bool gamePaused);
    }

}