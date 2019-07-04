
using Assets.Scripts.engine.piece;

public interface IGameServices
{
    void NotifyStartSession();
    void NotifyNextPiece(short clientId,Piece p);
    void NotifyEndGame(short clientId,bool isWin);
}
