using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamenetController : Singleton<GamenetController>
{
    private GamenetManager netManager;

    public GamenetEvent ON_SERVER_ADD_PLAYER;
    public GamenetEvent ON_CLIENT_CONNECTED;

    public bool isNetworkActive => netManager.isNetworkActive;
    public bool isEnabled;
    public bool isServer => netManager.isServerHost;

    private int MAX_CLIENTS_PER_MATCH = 2;


    public void StartController(GamenetManager netManager)
    {
        ON_SERVER_ADD_PLAYER = new GamenetEvent();
        ON_CLIENT_CONNECTED = new GamenetEvent();
        this.netManager = netManager;
        this.netManager.OnServerAddClientPlayer = OnServerAddPlayer;
        this.netManager.OnClientConnectedPlayer = OnClientConnected;
    }

    public void EnableManager(bool isEnable)
    {
        isEnabled = isEnable;
        netManager.gameObject.SetActive(isEnable);
       
    }

    private void OnServerAddPlayer(NetworkConnection conn, int playerControllerId)
    {
        Debug.LogWarning("Gamenet Controller OnServerAddPlayer : " + netManager.totalClients);
//
//        if (netManager.totalClients == MAX_CLIENTS_PER_MATCH)
//        {
//            ON_SERVER_ADD_PLAYER.Invoke(conn, netManager.localClient.connectionId);
//        }
    }

    private void OnClientConnected(NetworkConnection conn, int playerControllerId)
    {
        Debug.LogWarning("Gamenet Controller OnClientConnected : " + conn.connectionId);
//        ON_CLIENT_CONNECTED.Invoke(conn, conn.connectionId);
    }

    public void StartHost()
    {
//        Debug.LogWarning("Gamenet StartHost");
        netManager.StartHost();
    }

    public void StartClient()
    {
//        Debug.LogWarning("Gamenet StartClient");
        netManager.StartClient();
    }
}
