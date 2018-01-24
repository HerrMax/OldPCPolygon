using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Pickup : NetworkBehaviour {

    public float pickupRange;
    public Transform eyes;
    public KeyCode pickup = KeyCode.E;
    public Text pickupText;
    public Inventory inventory;
    public AudioSource aS;
    public AudioClip equipSound;
    public int itemID;

    void Update() {
        if (!isLocalPlayer)
        { return; }
        
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, pickupRange) && hit.transform.tag == "Item") {
            pickupText.text = hit.transform.GetComponent<ItemID>().itemName + " [" + pickup + "]";
            if (Input.GetKeyDown(pickup) /*&& inventory.items.Count < 20*/) {
                CmdPickup(hit.transform.gameObject);
            }
        }
        else
        {
            pickupText.text = null;
        }   
    }

    [Command]
    void CmdPickup(GameObject hit)
    {
        itemID = hit.transform.GetComponent<ItemID>().itemID;
        inventory.AddItem(itemID);
        //Destroy(hit.transform.gameObject);
        NetworkServer.Destroy(hit.transform.gameObject);
        aS.PlayOneShot(equipSound);
    }
}
