using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine.piece;
using UnityEngine;

public interface IGameServices
{
    void NotifyStartSession();
    void NotifyNextPiece(Piece p);
    void NotifyEndGame(bool isWin);
}
