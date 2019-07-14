using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.piece;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.view.board
{
    public class ClientContentView : GamenetClient, IGameServices, IPlayerControllerServices
    {
        [SerializeField] private Transform pieceContent;
        [SerializeField] private BoardBottom bottom;
        public PieceView currentPiece { get; private set; }
        private float speedDrop = 0.01f;
        private NetworkIdentity netIdentity;
        private List<PieceView> droppedPieces;
        private string clientId;
        
//    private HighLightView highLight;
        void Awake()
        {
            droppedPieces = new List<PieceView>();
            netIdentity = GetComponent<NetworkIdentity>();
        }

        void Start()
        {
            
            clientId = "client" + netIdentity.netId;
            transform.name = clientId;
            ClientManager.RegisterPlayer(clientId, this);

            
            if (isLocalPlayer)
            {
                SessionController.ME.StartSession(this, clientId);
                UserGameController.ME.StartPlayerController(this, clientId);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
            }

        }

        public void SaveLastPiece(PieceView p)
        {
            droppedPieces.Add(p);
        }

        [Client]
        public void DropNewPiece(string id, Piece piece)
        {
            Debug.LogWarning("DropNewPiece : " + id);

            currentPiece = CreatePiece(piece);
            currentPiece.OnMovementComplete = OnMovementComplete;
           
            CmdDropNewPiece(id, piece.id);
        }

        private PieceView CreatePiece(Piece piece)
        {
            PieceView p = Instantiate(PieceController.ME.RetrievePiece(piece), pieceContent.transform).AddComponent<PieceView>();
            p.Initiate(piece);

            return p;
        }

        #region [PIECE EVENTS]
        private void OnMovementComplete(PieceView piece)
        {
            SaveLastPiece(piece);
            Debug.LogWarning("OnMovementComplete");
            SessionController.ME.CheckNewPiece();
        }

        #endregion[]

        #region [ENGINE FEEDS]
        public void NotifyStartSession()
        {
            UserGameController.ME.EnableController();
        }

        public void NotifyNextPiece(string cId, Piece p)
        {
            Debug.LogWarning("NotifyNextPiece : " +  cId);
            DropNewPiece(cId, p);
           
        }

        public void NotifyEndGame(string cId, bool isWin)
        {
            UserGameController.ME.DisableController();
        }
        #endregion

        #region [CONTROLLERS]
        public void NotifyMoveLeft(string playerId)
        {
            currentPiece.transform.DOMoveX(currentPiece.currentPosition.x - 0.1f, 0);
        }

        public void NotifyMoveRight(string playerId)
        {
            currentPiece.transform.DOMoveX(currentPosition.x + 0.1f, 0);
        }

        public void NotifyMoveDown(string playerId)
        {
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y - 0.1f, currentPosition.z);
        }

        public void NotifyRotateLeft(string playerId)
        {
            if (currentPiece.applyRotation) return;
            LockRotate();
            currentPiece.transform.DORotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + 90), 0.2f).OnComplete(UnLockRotate);
        }

        public void NotifyRotateRight(string playerId)
        {
            if (currentPiece.applyRotation) return;
            LockRotate();
            currentPiece.transform.DORotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z - 90), 0.2f).OnComplete(UnLockRotate);
        }


        #endregion region


        #region [SERVER COMMANDS]

        [Command]
        private void CmdDropNewPiece(string id, int pieceId)
        {
            Debug.LogWarning("new piece dropped: " + id + " | " + pieceId +  " | " + id  +  " | " + isLocalPlayer);
            if(isLocalPlayer) return;
            
            currentPiece = ((ClientContentView) ClientManager.GetPlayer(id)).CreatePiece(PieceController.ME.RetrievePieceById(pieceId));
            currentPiece.OnMovementComplete = OnMovementComplete;
        }
    
        #endregion

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
            if (isPaused)
                droppedPieces.ForEach(x => x.Sleep());
            else
                droppedPieces.ForEach(x => x.WakeUp());
        }


        void Update()
        {
            if (currentPiece == null || currentPiece.pieceDropped || UserGameController.ME.gamePaused) return;
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y - speedDrop, currentPosition.z);
        }

        public Vector3 currentPosition => currentPiece.currentPosition;
        public Vector3 currentRotation => currentPiece.currentRotation;


        void OnDisable()
        {
            ClientManager.UnRegisterPlayer(clientId);
        }
    }
}
