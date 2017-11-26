using System.Collections;
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

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }

    }
}
