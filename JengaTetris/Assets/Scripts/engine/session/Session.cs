using System;
using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.engine.piece;

namespace Assets.Scripts.engine.session
{
    public class Session
    {
        private List<Piece> pieceTypes;
        public bool isOver => false;
        public bool isWinner => false;
        private static Random random;
        public Session()
        {
            PieceController.ME.StartPool();
            pieceTypes = PieceController.ME.GetAllPieces();
            random = new Random();
        }

        public Piece NextPiece()
        {
            int index = random.Next(pieceTypes.Count);
            return pieceTypes[index];
        }
    }
}
