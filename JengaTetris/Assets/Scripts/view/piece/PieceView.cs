using Assets.Scripts.engine.piece;
using Assets.Scripts.view.common;
using UnityEngine;

namespace Assets.Scripts.view.piece
{
    public class PieceView:GameComponent
    {
        public Piece piece { get; private set; }
        private Rigidbody2D piecePhysics;

        void Awake()
        {
            piecePhysics = GetComponent<Rigidbody2D>();
            RemovePhysics();
        }

        private void RemovePhysics() => piecePhysics.bodyType = RigidbodyType2D.Static;
        private void AddPhysics() => piecePhysics.bodyType = RigidbodyType2D.Dynamic;
        
    }
}
