using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coords : MonoBehaviour {

    public Text coordsT;
    public Text rotT;
    public Transform cam;
	
	void Update () {
        coordsT.text = "Pos (X: " + Mathf.RoundToInt(this.transform.position.x) + 
            " Y: " + (Mathf.RoundToInt(this.transform.position.y) -1) + 
            " Z: " + Mathf.RoundToInt(this.transform.position.z) + ")";

        rotT.text = "Rot (X: " + Mathf.RoundToInt(cam.localRotation.eulerAngles.x) + 
            " Y: " + Mathf.RoundToInt(cam.rotation.eulerAngles.y * 10) / 10 + ")";
	}
}
