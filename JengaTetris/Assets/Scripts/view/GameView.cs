using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.board;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using UnityEngine;

public class GameView : GameComponent, IGameServices, IControllerServices
{
    private BoardView board;
    public override void Awake()
    {
        //start components
        board = LoadAndAdd<BoardView>(transform, "Stage/Board");

        //start engine and controllers
        SessionController.ME.StartSession(this);
        UserGameController.ME.StartController(this);
    }

    #region [ENGINE FEEDS]
    public void NotifyStartSession()
    {
        UserGameController.ME.EnableController();
    }

    public void NotifyNextPiece(Piece p)
    {
        board.DropNewPiece(p);
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

    public void NotifyPause()
    {
        Debug.Log("NotifyPause");
    }

    #endregion region
}
