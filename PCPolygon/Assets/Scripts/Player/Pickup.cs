using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

    public float pickupRange;
    public Transform eyes;
    public KeyCode pickup = KeyCode.E;
    public Text pickupText;
    public Inventory inventory;
    public AudioSource aS;
    public AudioClip equipSound;
    public int itemID;

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, pickupRange) && hit.transform.tag == "Item") {
            pickupText.text = hit.transform.name + "[" + pickup + "]";
            if (Input.GetKeyDown(pickup)) {
                itemID = hit.transform.GetComponent<ItemID>().itemID;
                inventory.AddItem(itemID);
                Destroy(hit.transform.gameObject);
                aS.PlayOneShot(equipSound);
            }
        }
        else
        {
            pickupText.text = null;
        }   
    }
}
