using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.view.board
{
    public class BoardView:GameComponent
    {
        private HighLightView highLight;
        private List<PieceView> droppedPieces;
        public PieceView currentPiece { get; private set; }

        //board detection
        private BoardBottom bottom;
        public BoardSide leftSide { get; private set; }
        public BoardSide rightSide { get; private set; }

        //board actions
        public UnityAction OnPieceFall;
        public UnityAction OnPieceFailed;
        public UnityAction OnPieceSucceed;
       
        private float speedDrop = 0.01f;

        void Awake()
        {
            bottom = FindAndAdd<BoardBottom>(transform, "Bottom");
            leftSide = FindAndAdd<BoardSide>(transform, "Left");
            rightSide = FindAndAdd<BoardSide>(transform, "Right");

            leftSide.OnPieceCollision = rightSide.OnPieceCollision = OnPieceCollision;

            droppedPieces = new List<PieceView>();
        }

        private void OnPieceCollision(PieceView p)
        {
            if (droppedPieces.Contains(p))
            {
                SessionController.ME.SetFail();
                OnPieceFailed.Invoke();
                p.RemovePhysics();
                droppedPieces.Remove(p);
            }

            SessionController.ME.SetFall();
            OnPieceFall.Invoke();

            Debug.Log("ON PIECE COLLISION ON WALLS: " + p);
        }

        public void OnMovementComplete(PieceView p)
        {
            droppedPieces.Add(p);
            Debug.LogWarning("OnMovementComplete");
            SessionController.ME.SetStacked();
            SessionController.ME.CheckNewPiece();
            OnPieceSucceed.Invoke();
        }
        
        public PieceView DropNewPiece(Piece piece)
        {
            PieceView p = InstantiateAndAdd<PieceView>(transform, PieceController.ME.RetrievePiece(piece)).Initiate(piece);
            currentPiece = p;
            return p;
        }

        public void MovePieceLeft()
        {
            currentPiece.transform.DOMoveX(currentPiece.currentPosition.x - 0.1f, 0);
        }

        public void MovePieceRight()
        {
            currentPiece.transform.DOMoveX(currentPosition.x + 0.1f, 0);
        }
        public void MovePieceDown()
        {
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y-0.1f, currentPosition.z);
        }
        public void RotatePieceLeft()
        {
            if(currentPiece.applyRotation)return;
            LockRotate();
            currentPiece.transform.DORotate(new Vector3(currentRotation.x,currentRotation.y,currentRotation.z + 90), 0.2f).OnComplete(UnLockRotate);
        }
        public void RotatePieceRight()
        {
            if (currentPiece.applyRotation) return;
            LockRotate();
            currentPiece.transform.DORotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z-90), 0.2f).OnComplete(UnLockRotate);
        }

        private void LockRotate()
        {
            currentPiece.applyRotation = true;
        }

        private void UnLockRotate()
        {
            currentPiece.applyRotation = false;
        }

        public void PauseGame(bool isPaused)
        {
            if(isPaused)
                droppedPieces.ForEach(x=>x.Sleep());
            else
                droppedPieces.ForEach(x => x.WakeUp());
        }


        void Update()
        {
            if (currentPiece.pieceDropped || UserGameController.ME.gamePaused) return;
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y-speedDrop, currentPosition.z);
        }

        public Vector3 currentPosition => currentPiece.currentPosition;
        public Vector3 currentRotation => currentPiece.currentRotation;

    }
}
