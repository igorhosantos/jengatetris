using Assets.Scripts.engine.session;

namespace Assets.Scripts.controller.session
{
    public class SessionController:Singleton<SessionController>
    {
        private Session session;
        private IGameServices services;
        private short clientId;
        public void StartSession(IGameServices services, short clientId)
        {
            this.services = services;
            this.clientId = clientId;

            session = new Session();
            services.NotifyStartSession();
            CheckNewPiece();
        }

        public void CheckNewPiece()
        {
            if(session.isOver) services.NotifyEndGame(clientId,session.isWinner);
            else services.NotifyNextPiece(clientId,session.NextPiece());
        }
    }
}
