using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine.services;
using UnityEngine;

public class UserGameController : Singleton<UserGameController>
{
    private IControllerServices services;
    private bool controllerEnables;
    public bool gamePaused { get; private set; }
    public void StartController(IControllerServices services)
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            services.NotifyPause(gamePaused);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) services.NotifyMoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) services.NotifyMoveRight();
        if (Input.GetKeyDown(KeyCode.S)) services.NotifyRotateLeft();
        if (Input.GetKeyDown(KeyCode.D)) services.NotifyRotateRight();


        //press for down
        if (Input.GetKey(KeyCode.DownArrow)) services.NotifyMoveDown();
    }
}
