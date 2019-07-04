using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GamenetController : Singleton<GamenetController>
{
    private GamenetManager netManager;

    public GamenetEvent ON_SERVER_ADD_PLAYER;

    public bool isNetworkActive => netManager.isNetworkActive;
    public bool isEnabled;
    public void StartController(GamenetManager netManager)
    {
        ON_SERVER_ADD_PLAYER = new GamenetEvent();
        this.netManager = netManager;
        this.netManager.OnAddPlayer = OnServerAddPlayer;
    }

    public void EnableManager(bool isEnable)
    {
        isEnabled = isEnable;
        netManager.gameObject.SetActive(isEnable);
       
    }

    private void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        ON_SERVER_ADD_PLAYER.Invoke(conn, playerControllerId);
    }

    public void StartConnection()
    {
        if (!isEnabled) return;

        if (isNetworkActive) netManager.StartClient();
        else netManager.StartHost();
    }

}
