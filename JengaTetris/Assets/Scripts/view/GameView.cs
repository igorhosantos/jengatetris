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

public class GameView : GameComponent, IGameServices, IControllerServices
{
    private BoardView board;
    private MenuView menu;
    public override void Awake()
    {
        //start components
        board = LoadAndAdd<BoardView>(transform, "Stage/Board");
        menu = FindAndAdd<MenuView>("CanvasMenu");

        //start engine and controllers
        SessionController.ME.StartSession(this);
        UserGameController.ME.StartController(this);
    }

    private void EnableMenu(bool gamePaused)=> menu.EnableMenu(gamePaused);
    

    #region [PIECE EVENTS]
    private void OnMovementComplete(PieceView piece)
    {
        board.SaveLastPiece(piece);
        Debug.LogWarning("OnMovementComplete");
        SessionController.ME.CheckNewPiece();
    }

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
