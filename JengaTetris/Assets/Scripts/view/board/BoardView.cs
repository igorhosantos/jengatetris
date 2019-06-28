using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.engine.piece;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.view.board
{
    public class BoardView:GameComponent
    {
        private HighLightView highLight;
        private List<PieceView> pieces;
        public PieceView currentPiece { get; private set; }

        void Awake()
        {
            pieces = new List<PieceView>();
        }

        public PieceView DropNewPiece(Piece piece)
        {
            PieceView p = InstantiateAndAdd<PieceView>(transform, PieceController.ME.RetrievePiece(piece));
            pieces.Add(p);
            currentPiece = p;
            return p;
        }

        public void MovePieceLeft()
        {
            currentPiece.transform.DOMoveX(currentPosition.x - 0.4f, 0);
        }

        public void MovePieceRight()
        {
            currentPiece.transform.DOMoveX(currentPosition.x + 0.4f, 0);
        }
        public void MovePieceDown()
        {
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y-0.1f, currentPosition.z);
        }
        public void RotatePieceLeft()
        {
            currentPiece.transform.DORotate(new Vector3(currentRotation.x,currentRotation.y,currentRotation.z + 90), 0);
        }
        public void RotatePieceRight()
        {
            currentPiece.transform.DORotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z-90), 0);
        }


        private Vector3 currentPosition => currentPiece.transform.localPosition;
        private Vector3 currentRotation => currentPiece.transform.rotation.eulerAngles;
    }
}
