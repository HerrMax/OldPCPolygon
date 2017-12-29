using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsDisable;
    [SerializeField]
    GameObject playerModel;

    Camera mainCamera;

    void Start() {
        if (!isLocalPlayer) {
            playerModel.SetActive(true);
            for (int i = 0; i < componentsDisable.Length; i++) {
                componentsDisable[i].enabled = false;
            }
        }else {
            mainCamera = Camera.main;
            if(mainCamera != null) {
                mainCamera.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable() {
        if(mainCamera != null) {
            mainCamera.gameObject.SetActive(true);
        }
    }

}
