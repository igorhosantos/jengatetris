using UnityEngine;
using System.Collections;
using Assets.Scripts.view.common;

public class MenuView : GameComponent
{
    private Transform hud;
    private Transform menu;
    void Awake()
    {
        EnableMenu(false);
    }
    public void EnableMenu(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }
}
