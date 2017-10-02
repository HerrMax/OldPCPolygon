using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    public KeyCode flashlightToggle = KeyCode.F;
    private bool flashOn = false;
    private Light flashComp;

    void Start() {
        flashComp = this.GetComponent<Light>();
    }

    void Update () {
        if (Input.GetKeyDown(flashlightToggle)) {
            if(flashOn == true) {
                flashOn = false;
                flashComp.enabled = flashOn;
            }
            else {
                flashOn = true;
                flashComp.enabled = flashOn;
            }
        }	
	}
}
