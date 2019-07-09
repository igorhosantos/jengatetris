
using Assets.Scripts.engine.piece;

public interface IGameServices
{
    void NotifyStartSession();
    void NotifyNextPiece(string cId,Piece p);
    void NotifyEndGame(string cId,bool isWin);
}
