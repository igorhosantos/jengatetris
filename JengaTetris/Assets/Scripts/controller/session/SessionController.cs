using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.engine.session;

namespace Assets.Scripts.controller.session
{
    public class SessionController:Singleton<SessionController>
    {
        private Session session;
        private IGameServices services;

        public void StartSession(IGameServices services)
        {
            this.services = services;
            session = new Session();
            services.NotifyStartSession();
            CheckNewPiece();
        }

        private void CheckNewPiece()
        {
            if(session.isOver) services.NotifyEndGame(session.isWinner);
            else services.NotifyNextPiece(session.NextPiece());
        }
    }
}
