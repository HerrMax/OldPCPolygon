using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    private static Dictionary<string, Player> playerList = new Dictionary<string, Player>();

    public static void RegPlayer(string netID, Player player) {
        string playerID = "Player " + netID;
        playerList.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static Player GetPlayer (string playerID)
    {
        return playerList[playerID];
    }

    public static void UnregPlayer(string playerID) {
        playerList.Remove(playerID); 
    }
}
