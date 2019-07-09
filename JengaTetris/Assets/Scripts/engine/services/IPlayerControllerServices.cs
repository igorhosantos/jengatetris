
namespace Assets.Scripts.engine.services
{
    public interface IPlayerControllerServices
    {
        void NotifyMoveLeft(string clientId);
        void NotifyMoveRight(string clientId);
        void NotifyMoveDown(string clientId);
        void NotifyRotateLeft(string clientId);
        void NotifyRotateRight(string clientId);
    }
}