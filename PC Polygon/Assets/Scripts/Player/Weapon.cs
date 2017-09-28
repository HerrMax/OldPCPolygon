using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    public int range;
    public int damage;

    public KeyCode shootKey = KeyCode.Mouse0;

    void Start() {
        if (cam == null) this.enabled = false;
    }

    void Update() {
        if(Input.GetKeyDown(shootKey))
        Shoot();
    }

    [Client]
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask)) {
            print("hit");
            if (hit.collider.tag == "Player") {
                CmdTakeDamage(hit.collider.name, damage);
                print("hit2");
            }
        }
    }

    [Command]
    void CmdTakeDamage(string playerID, int _damage) {
        Debug.Log(playerID + " has been hit. " + _damage + " points of damager were dealt.");

        Player player = GameManager.GetPlayer(playerID);
        player.RpcDamage(_damage);
    }
}
