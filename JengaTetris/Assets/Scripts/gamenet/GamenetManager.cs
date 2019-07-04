using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class GamenetManager : NetworkManager
{
    public UnityAction<NetworkConnection, short> OnAddPlayer;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        OnAddPlayer.Invoke(conn,playerControllerId);
    }
}
