using Assets.Scripts.engine.services;
using UnityEngine;

public class UserGameController : Singleton<UserGameController>
{
    private IPlayerControllerServices playerServices;
    private bool controllerEnables;
    public bool gamePaused { get; private set; }
  
    public void StartController(IPlayerControllerServices playerServices)
    {
        this.playerServices = playerServices;
    }


    public void EnableController()
    {
        controllerEnables = true;
    }

    public void DisableController()
    {
        controllerEnables = false;
    }

    void Update()
    {
        if (!controllerEnables) return;

        //global
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            playerServices.NotifyPause(gamePaused);
        }

        //per player
        if (Input.GetKeyDown(KeyCode.LeftArrow)) playerServices.NotifyMoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) playerServices.NotifyMoveRight();
        if (Input.GetKeyDown(KeyCode.S)) playerServices.NotifyRotateLeft();
        if (Input.GetKeyDown(KeyCode.D)) playerServices.NotifyRotateRight();
        //press for down
        if (Input.GetKey(KeyCode.DownArrow)) playerServices.NotifyMoveDown();
    }
}
