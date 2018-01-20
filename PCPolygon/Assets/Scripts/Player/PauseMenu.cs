﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private bool pauseActive;
    [SerializeField] NetworkManager networkManager;

	void Start ()
    {
        networkManager = NetworkManager.singleton; 
	}

    public void leaveGame()
    {
        networkManager.StopClient();
        if (!Network.isClient) networkManager.StopHost();
    }
}
