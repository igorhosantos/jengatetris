using Assets.Scripts.engine.services;
using Assets.Scripts.view.board;
using Assets.Scripts.view.common;

public class GameView : GameComponent,IControllerServices
{
    private BoardView board;
    private MenuView menu;
    public override void Awake()
    {
        //start components
        board = FindAndGet<BoardView>(transform, "Board");
        menu = FindAndAdd<MenuView>("CanvasMenu");
        UserGameController.ME.StartGlobalController(this);
    }

    public void NotifyPause(bool gamePaused)
    {
        board.NotifyPause(gamePaused);
        menu.EnableMenu(gamePaused);
    }
}
