using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.board;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using UnityEngine;
using UnityEngine.Events;

public class GameView : GameComponent, IGameServices,IPlayerControllerServices
{
    [SerializeField]private Canvas menuCanvas;
    [SerializeField] private MenuView menu;
    [SerializeField] private HudView hud;
    private BoardView board;
    
    public override void Awake()
    {
        //start components
        board = LoadAndAdd<BoardView>(transform, "Stage/Board");
        board.OnPieceFall = OnPieceFall;
        board.OnPieceFailed = OnPieceFailed;
        board.OnPieceSucceed = OnPieceSucceed;

        CreateSession();
    }

    private void CreateSession()
    {
        //start engine and controllers
        SessionController.ME.StartSession(this);
        UserGameController.ME.StartController(this);

        OnPieceFall();
        OnPieceSucceed();
    }

    private void EnableMenu(bool gamePaused)=> menu.EnableMenu(gamePaused);

    #region [BOARD UPDATES]

    private void OnPieceFall()=> hud.UpdateFalls(SessionController.ME.falls);
    private void OnPieceSucceed() => hud.UpdateStacked(SessionController.ME.stackeds);
    private void OnPieceFailed()
    {
        hud.UpdateStacked(SessionController.ME.stackeds);
        hud.UpdateFalls(SessionController.ME.falls);
    }
    
    #endregion


    #region [PIECE EVENTS]
    private void OnMovementComplete(PieceView piece)=> board.OnMovementComplete(piece);
    #endregion[]

    #region [ENGINE FEEDS]
    public void NotifyStartSession()
    {
        UserGameController.ME.EnableController();
    }

    public void NotifyNextPiece(Piece p)
    {
        Debug.LogWarning("NotifyNextPiece");
        board.DropNewPiece(p);
        board.currentPiece.OnMovementComplete = OnMovementComplete;
    }

    public void NotifyEndGame(bool isWin)
    {
        UserGameController.ME.DisableController();
    }
    #endregion

    #region [CONTROLLERS]
    public void NotifyMoveLeft()
    {
        board.MovePieceLeft();
    }

    public void NotifyMoveRight()
    {
        board.MovePieceRight();
    }

    public void NotifyMoveDown()
    {
        board.MovePieceDown();
    }

    public void NotifyRotateLeft()
    {
        board.RotatePieceLeft();
    }

    public void NotifyRotateRight()
    {
        board.RotatePieceRight();
    }

    public void NotifyPause(bool gamePaused)
    {
        EnableMenu(gamePaused);
        board.PauseGame(gamePaused);
    }

    #endregion region
}
