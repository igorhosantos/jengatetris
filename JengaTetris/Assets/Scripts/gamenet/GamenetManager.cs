using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class GamenetManager : NetworkManager
{
    public UnityAction<NetworkConnection, int> OnServerAddClientPlayer;
    public UnityAction<NetworkConnection, int> OnClientConnectedPlayer;

    public int totalClients => NetworkServer.connections.Count;
    public NetworkConnection localClient { get; private set; }

    public bool isServerHost;
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.LogWarning("[Gamenet] OnServerAddPlayer");
        base.OnServerAddPlayer(conn, playerControllerId);
        localClient = conn;
        OnServerAddClientPlayer.Invoke(conn, conn.connectionId);
        isServerHost = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.LogWarning("[Gamenet] OnClientConnect");
        base.OnClientConnect(conn);
        
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
//        Debug.LogWarning("[Gamenet] OnStartClient : " + client.connection);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
//        Debug.LogWarning("[Gamenet] OnClientSceneChanged");
        base.OnClientSceneChanged(conn);
//        if (isServerHost) return;
//        OnClientConnectedPlayer.Invoke(conn, conn.connectionId);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.LogWarning("[Gamenet] OnServerConnect");
        base.OnServerConnect(conn);
        //OnAddPlayer.Invoke(conn, conn.connectionId);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.LogWarning("[Gamenet] OnServerReady");
        base.OnServerReady(conn);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.LogWarning("[Gamenet] OnMatchCreate");
        base.OnMatchCreate(success, extendedInfo, matchInfo);
    }


}
