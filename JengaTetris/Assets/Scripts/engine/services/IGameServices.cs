
using Assets.Scripts.engine.piece;

public interface IGameServices
{
    void NotifyStartSession();
    void NotifyNextPiece(Piece p);
    void NotifyEndGame(bool isWin);
}
