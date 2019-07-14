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
        
        public const int MAX_TO_WIN = 10;
        public const int MAX_TO_LOSE = 5;

        public int falls { get; private set; }
        public int stackeds { get; private set; }

        public Session()
        {
            falls = 0;
            stackeds = 0;
            PieceController.ME.StartPool();
            pieceTypes = PieceController.ME.GetAllPieces();
            random = new Random();
        }

        public Piece NextPiece()
        {
            int index = random.Next(pieceTypes.Count);
            return pieceTypes[index];
        }

        public void SetFail()
        {
            if (stackeds > 0) stackeds--;
            falls++;
        }

        public void SetFall()
        {
            if (falls > MAX_TO_LOSE) throw new Exception();
            falls++;
        }

        public void SetStacked()
        {
            if (stackeds > MAX_TO_WIN) throw new Exception();
            stackeds++;
        }

    }
}
