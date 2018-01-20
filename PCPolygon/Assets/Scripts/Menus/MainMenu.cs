using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour {

    [SerializeField] NetworkManager networkManager;
    [SerializeField] InputField ipInput;
    [SerializeField] InputField portInput;

    public void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void HostGame()
    {
        SetPort();
        networkManager.StartHost();
    }

    public void JoinGame()
    {
        SetIP();
        SetPort();
        networkManager.StartClient();
    }

    void SetIP()
    {
        networkManager.networkAddress = ipInput.text;
    }
    void SetPort()
    {
        networkManager.networkPort = int.Parse(portInput.text);
    }
}
