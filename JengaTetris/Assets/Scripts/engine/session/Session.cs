using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.engine.piece;

namespace Assets.Scripts.engine.session
{
    public class Session
    {
        public bool isOver => false;
        public bool isWinner => false;

        public Piece NextPiece()
        {
            return new Piece();
        }
    }
}
