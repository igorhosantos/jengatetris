using Assets.Scripts.controller.session;
using Assets.Scripts.engine.piece;
using Assets.Scripts.engine.services;
using Assets.Scripts.view.board;
using Assets.Scripts.view.common;
using Assets.Scripts.view.piece;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameView : GameComponent, IGameServices,IPlayerControllerServices
{
    [SerializeField]private Canvas menuCanvas;
    [SerializeField] private MenuView menu;
    [SerializeField] private HudView hud;
    [SerializeField] private GameStatusView gameStatus;

    private BoardView board;
    
    public override void Awake()
    {
        //start components
        board = LoadAndGet<BoardView>(transform, "Stage/Board");
        board.OnPieceFall = OnPieceFall;
        board.OnPieceFailed = OnPieceFailed;
        board.OnPieceSucceed = OnPieceSucceed;

        gameStatus.OnStartGame = CreateSession;
        gameStatus.UpdateState(GameStatusView.GAMESTATE.ON_START);
    }

    private void CreateSession()
    {
        //start engine and controllers
        SessionController.ME.StartSession(this);
        UserGameController.ME.StartController(this);

        OnPieceFall();
        OnPieceSucceed();
    }

    private void BacktoMenu()=> SceneManager.LoadScene(Startup.SCENE_MENU, LoadSceneMode.Single);
    private void EnableMenu(bool gamePaused)=> menu.EnableMenu(gamePaused);

    #region [BOARD UPDATES]

    private void OnPieceFall() => hud.UpdateFalls(SessionController.ME.falls);
    private void OnPieceSucceed()=> hud.UpdateStacked(SessionController.ME.stackeds);
    
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
        board.DropNewPiece(p);
        board.currentPiece.OnMovementComplete = OnMovementComplete;
    }

    public void NotifyEndGame(bool isWin)
    {
        board.EndGame();
        UserGameController.ME.DisableController();
        gameStatus.UpdateState(isWin ? GameStatusView.GAMESTATE.ON_WIN : GameStatusView.GAMESTATE.ON_LOSE);
        Invoke(nameof(BacktoMenu),2f);


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
