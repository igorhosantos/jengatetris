using System;
using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.engine.piece;

namespace Assets.Scripts.engine.session
{
    public class Session
    {
        public const int MAX_TO_WIN = 10;
        public const int MAX_TO_LOSE = 5;

        private List<Piece> pieceTypes;
        private static Random random;

        public bool isOver { get; private set; }
        public bool isWinner { get; private set; }
        public int falls { get; private set; }
        public int stackeds { get; private set; }

        public Session()
        {
            stackeds = falls = 0;
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
            CheckLose();
        }

        private void CheckLose()
        {
            if (falls >= MAX_TO_LOSE) isOver = true;
        }

        public void SetFall()
        {
            falls++;
            CheckLose();
        }

        public void SetStacked()
        {
            stackeds++;
            if (stackeds >= MAX_TO_WIN) isWinner = isOver = true;
        }
    }
}
