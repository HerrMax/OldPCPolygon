using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkInfo : NetworkBehaviour {

    public int playerCount;
    public int ping;
    public Text playerCountText;

    public override void OnStartClient()
    {
        UpdateInfo();
    }

    private void OnPlayerConnected(NetworkIdentity player)
    {
        UpdateInfo();
    }

    public void OnPlayerDisconnected(NetworkIdentity player)
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        playerCount = NetworkManager.singleton.numPlayers;
        ping = NetworkManager.singleton.client.GetRTT();

        playerCountText.text = "Players : " + playerCount + "/" + NetworkManager.singleton.maxConnections + "\nPing : " + ping + "ms";
    }
}
