using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsDisable;
    //[SerializeField]
    //GameObject playerModel;

    [SerializeField]
    string remoteLayer = "Remote";

    [SerializeField]
    string noDrawLayer = "NoDraw";
    [SerializeField]
    GameObject playerGraphics;

    Camera mainCamera;
    public GameObject playerUsernameBar;

    void Start() {
        if (!isLocalPlayer) {
            NotLocal();
        }else {
            Local();
        }
    }

    public override void OnStartClient() {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegPlayer(netID, player);
    }

    void OnDisable() {
        if(mainCamera != null) {
            mainCamera.gameObject.SetActive(true);
        }
        GameManager.UnregPlayer(transform.name);
    }

    void NotLocal() {
        //playerModel.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer(remoteLayer);
        for (int i = 0; i < componentsDisable.Length; i++) {
            componentsDisable[i].enabled = false;
        }
    }

    void Local() {
        mainCamera = Camera.main;
        if (mainCamera != null) {
            mainCamera.gameObject.SetActive(false);
        }

        SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(noDrawLayer));

        playerUsernameBar.SetActive(false);

        GetComponent<Player>().Setup();
    }

    void SetLayerRecursively(GameObject obj, int newLayer) {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
