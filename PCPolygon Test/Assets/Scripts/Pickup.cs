using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

    public float pickupRange;
    public Transform eyes;
    public KeyCode pickup = KeyCode.E;
    public Text pickupText;
    public Text objectName;
    public AudioSource aS;
    public AudioClip equipSound;

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, pickupRange) && hit.transform.tag == "Item") {
            pickupText.text = "Press " + pickup + " to pickup " + hit.transform.name;
            if (Input.GetKeyDown(pickup)) {
                //write pickup code here, the destroy meme will be temporary
                Destroy(hit.transform.gameObject);
                aS.PlayOneShot(equipSound);
            }
        }
        else
        {
            pickupText.text = null;
        }

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit)) {
            objectName.text = hit.transform.name;
        }

        else {
            pickupText.text = "";
            objectName.text = "Null";
        }
    }
}
