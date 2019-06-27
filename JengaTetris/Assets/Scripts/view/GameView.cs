using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.board;
using Assets.Scripts.view.common;
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
       
    }

    public void NotifyEndGame(bool isWin)
    {
        UserGameController.ME.DisableController();
    }
    #endregion

    #region [CONTROLLERS]
    public void NotifyMoveLeft()
    {
        Debug.Log("NotifyMoveLeft");
    }

    public void NotifyMoveRight()
    {
        Debug.Log("NotifyMoveRight");
    }

    public void NotifyMoveDown()
    {
        Debug.Log("NotifyMoveDown");
    }

    public void NotifyRotateLeft()
    {
        Debug.Log("NotifyRotateLeft");
    }

    public void NotifyRotateRight()
    {
        Debug.Log("NotifyRotateRight");
    }

    public void NotifyPause()
    {
        Debug.Log("NotifyPause");
    }

    #endregion region
}
