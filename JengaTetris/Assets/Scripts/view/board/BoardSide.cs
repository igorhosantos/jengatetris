using UnityEngine;
using System.Collections;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using UnityEngine.Events;

namespace Assets.Scripts.view.board
{
    public class BoardSide : GameComponent
    {
        public UnityAction<PieceView> OnPieceCollision;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PieceView>())
            {
                PieceView pv = other.gameObject.GetComponent<PieceView>();
                if (pv.pieceDropped)
                {
                    OnPieceCollision.Invoke(other.gameObject.GetComponent<PieceView>());
                }
                
            }
        }
    }
}
