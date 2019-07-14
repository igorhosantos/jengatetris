using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.controller.piece;
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

        public int falls => session.falls;
        public int stackeds => session.stackeds;
        public bool gameOver => session!=null && session.isOver;

        public void CheckNewPiece()
        {
            if(session.isOver) services.NotifyEndGame(session.isWinner);
            else{ services.NotifyNextPiece(session.NextPiece());}
        }

        public void SetFail()
        {
            session.SetFail();
            if (session.isOver) services.NotifyEndGame(session.isWinner);
        }

        public void SetStacked()
        {
            session.SetStacked();
            if (session.isOver) services.NotifyEndGame(session.isWinner);
        }

        public void SetFall()
        {
            session.SetFall();
            if (session.isOver) services.NotifyEndGame(session.isWinner);
        } 

    }
}
