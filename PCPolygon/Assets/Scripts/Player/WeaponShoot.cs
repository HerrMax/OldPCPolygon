using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponShoot : NetworkBehaviour {

    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject weaponGraphic;
    [SerializeField] private string viewmodelLayerName = "Viewmodel";

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
        
        SetLayerRecursively(weaponGraphic, LayerMask.NameToLayer(viewmodelLayerName));
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
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
