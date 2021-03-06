﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;

    public GameSettings gameSettings;

    [SerializeField] private GameObject sceneCamera;

    private void Awake()
    {
        if (singleton != null) return;

        singleton = this;
        Keycodes.GetKeycodes();
    }

    public void SetSceneCameraActive (bool isActive)
    {
        if (sceneCamera == null) return;
        sceneCamera.SetActive(isActive);
    }

#region playerStuff

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = "Player " + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void DeregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer (string playerID)
    {
        return players[playerID];
    }
#endregion
}
