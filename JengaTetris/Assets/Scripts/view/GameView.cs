using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using UnityEngine;

public class GameView : MonoBehaviour, IGameServices
{

    void Awake()
    {
        SessionController.ME.StartSession(this);
    }

    public void NotifyStartSession()
    {
        throw new System.NotImplementedException();
    }

    public void NotifyNextPiece(Piece p)
    {
        throw new System.NotImplementedException();
    }

    public void NotifyEndGame(bool isWin)
    {
        throw new System.NotImplementedException();
    }
}
