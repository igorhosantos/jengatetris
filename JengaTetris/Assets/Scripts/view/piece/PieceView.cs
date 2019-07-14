using System.Linq.Expressions;
using Assets.Scripts.engine.piece;
using Assets.Scripts.view.common;
using Assets.Scripts.view.board;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Assets.Scripts.view.piece
{
    public class PieceView : GameComponent
    {
        public Piece piece { get; private set; }
        private Rigidbody2D piecePhysics;
        private PolygonCollider2D collider;
        public UnityAction<PieceView> OnMovementComplete;
        public bool pieceFailed { get; private set; }
        public bool pieceDropped { get; private set; }
        public bool applyRotation { get; set; }

        private static readonly Vector3 dropPosition = new Vector3(0, 4.5f, 0);

        void Awake()
        {
            collider = GetComponent<PolygonCollider2D>();
            piecePhysics = GetComponent<Rigidbody2D>();
            RemovePhysics();
        }

        public PieceView Initiate(Piece p)
        {
            piece = p;
            transform.localPosition = dropPosition;
            return this;
        }

        public void RemovePhysics() => piecePhysics.bodyType = RigidbodyType2D.Kinematic;
        public void AddPhysics() => piecePhysics.bodyType = RigidbodyType2D.Dynamic;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (pieceDropped) return;

            if (other.gameObject.GetComponent<PieceView>() || other.gameObject.GetComponent<BoardBottom>())
            {
                pieceDropped = true;
                collider.isTrigger = false;
                AddPhysics();
                OnMovementComplete.Invoke(this);
            }
        }

        public void Sleep()
        {
            piecePhysics.Sleep();
        }

        public void WakeUp()
        {
            piecePhysics.WakeUp();
        }

        public void SetFailed()
        {
            pieceFailed = true;
        }

        public Vector3 currentPosition => transform.localPosition;
        public Vector3 currentRotation => transform.rotation.eulerAngles;
    }
}
