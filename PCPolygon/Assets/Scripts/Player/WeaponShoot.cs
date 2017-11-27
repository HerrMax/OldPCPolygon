﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponShoot : NetworkBehaviour {

    public Weapon weapon;

    public KeyCode shoot = KeyCode.Mouse0;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Update()
    {
        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }
    }
    
    [Client]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log(this.transform.name + " shot " + hit.collider.name);
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
        }

    }

    [Command]
    void CmdPlayerShot(string playerID, float damage)
    {
        Debug.Log(playerID + " has been shot.");

        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage);
    }
}
