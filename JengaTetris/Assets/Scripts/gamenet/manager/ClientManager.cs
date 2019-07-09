using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    private static Dictionary<string, GamenetClient> players = new Dictionary<string, GamenetClient>();

    public static void RegisterPlayer(string id, GamenetClient player)
    {
        players.Add(id, player);
    }

    public static GamenetClient GetPlayer(string id)
    {
        return players[id];
    }

    public static void UnRegisterPlayer(string id)
    {
        players.Remove(id);
    }

}
