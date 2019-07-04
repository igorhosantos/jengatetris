using Assets.Scripts.engine.services;
using UnityEngine;

public class UserGameController : Singleton<UserGameController>
{
    private IPlayerControllerServices playerServices;
    private IControllerServices services;
    private bool controllerEnables;
    public bool gamePaused { get; private set; }
    private short clientId;

    public void StartPlayerController(IPlayerControllerServices playerServices, short clientId)
    {
        this.clientId = clientId;
        this.playerServices = playerServices;
    }

    public void StartGlobalController(IControllerServices services)
    {
        this.services = services;
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
            services.NotifyPause(gamePaused);
        }

        //per player
        if (Input.GetKeyDown(KeyCode.LeftArrow)) playerServices.NotifyMoveLeft(clientId);
        if (Input.GetKeyDown(KeyCode.RightArrow)) playerServices.NotifyMoveRight(clientId);
        if (Input.GetKeyDown(KeyCode.S)) playerServices.NotifyRotateLeft(clientId);
        if (Input.GetKeyDown(KeyCode.D)) playerServices.NotifyRotateRight(clientId);
        //press for down
        if (Input.GetKey(KeyCode.DownArrow)) playerServices.NotifyMoveDown(clientId);
    }
}
