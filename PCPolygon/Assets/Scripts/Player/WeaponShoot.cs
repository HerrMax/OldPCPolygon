using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class WeaponShoot : NetworkBehaviour {

    public KeyCode shoot = KeyCode.Mouse0;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private Weapon currentWeapon;
    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon.rateOfFire <= 0 && Input.GetKeyDown(shoot))
        {
            Shoot();
        }
        else
        {
            if (Input.GetKeyDown(shoot))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.rateOfFire);
            }else if (Input.GetKeyUp(shoot))
            {
                CancelInvoke("Shoot");
            }
        }
    }
    
    [Client]
    void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, mask))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log(this.transform.name + " shot " + hit.collider.name);
                CmdPlayerShot(hit.collider.name, currentWeapon.damage);
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
