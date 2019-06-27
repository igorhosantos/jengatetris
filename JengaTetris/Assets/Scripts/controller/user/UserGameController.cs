using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine.services;
using UnityEngine;

public class UserGameController : Singleton<UserGameController>
{
    private IControllerServices services;
    private bool controllerEnables;

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

        if (Input.GetKeyDown(KeyCode.Escape)) services.NotifyPause();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) services.NotifyMoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) services.NotifyMoveRight();
        if (Input.GetKeyDown(KeyCode.DownArrow)) services.NotifyMoveDown();
        if (Input.GetKeyDown(KeyCode.S)) services.NotifyRotateLeft();
        if (Input.GetKeyDown(KeyCode.D)) services.NotifyRotateRight();
        
    }
}
