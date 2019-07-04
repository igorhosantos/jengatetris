using System.Collections.Generic;
using Assets.Scripts.controller.piece;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.view.board
{
    public class BoardView:GameComponent, IGameServices, IPlayerControllerServices
    {
        private HighLightView highLight;
        private List<PieceView> droppedPieces;
        public PieceView currentPiece { get; private set; }
        
        private float speedDrop = 0.01f;

        private Dictionary<short,ClientContentView> clientViewList;
        
        void Awake()
        {
            clientViewList = new Dictionary<short, ClientContentView>();
            droppedPieces = new List<PieceView>();

            //Add network listener
            if (GamenetController.ME.isEnabled)
            {
                Debug.LogWarning("isNetworkActive");
                GamenetController.ME.ON_SERVER_ADD_PLAYER.AddListener(OnNewClient);
                GamenetController.ME.StartConnection();
            }
            else
            {
                OnOfflineClient(1);
            }
        }

        private void OnOfflineClient(short id)=> StartSessionConfig(id);
        private void OnNewClient(NetworkConnection conn,short id) => StartSessionConfig(id);
        
        private void StartSessionConfig(short id)
        {
            Debug.LogWarning("StartSessionConfig");
            clientViewList.Add(id, LoadAndAdd<ClientContentView>(transform, "Stage/ClientContent").Initiate(id));
            SessionController.ME.StartSession(this, id);
            UserGameController.ME.StartPlayerController(this, id);
        }

        public void SaveLastPiece(PieceView p)
        {
            droppedPieces.Add(p);
        }
        public PieceView DropNewPiece(short clientId,Piece piece)
        {
            PieceView p = InstantiateAndAdd<PieceView>(clientViewList[clientId].pieceContent.transform, PieceController.ME.RetrievePiece(piece)).Initiate(piece);
            currentPiece = p;
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

        public void NotifyNextPiece(short clientId,Piece p)
        {
            Debug.LogWarning("NotifyNextPiece");
            DropNewPiece(clientId,p);
            currentPiece.OnMovementComplete = OnMovementComplete;
        }

        public void NotifyEndGame(short clientId, bool isWin)
        {
            UserGameController.ME.DisableController();
        }
        #endregion

        #region [CONTROLLERS]
        public void NotifyMoveLeft(short playerId)
        {
            MovePieceLeft();
        }

        public void NotifyMoveRight(short playerId)
        {
            MovePieceRight();
        }

        public void NotifyMoveDown(short playerId)
        {
            MovePieceDown();
        }

        public void NotifyRotateLeft(short playerId)
        {
            RotatePieceLeft();
        }

        public void NotifyRotateRight(short playerId)
        {
            RotatePieceRight();
        }

        public void NotifyPause(bool gamePaused) => PauseGame(gamePaused);
        

        #endregion region
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
            if (currentPiece==null || currentPiece.pieceDropped || UserGameController.ME.gamePaused) return;
            currentPiece.transform.localPosition = new Vector3(currentPosition.x, currentPosition.y-speedDrop, currentPosition.z);
        }

        public Vector3 currentPosition => currentPiece.currentPosition;
        public Vector3 currentRotation => currentPiece.currentRotation;

    }
}
