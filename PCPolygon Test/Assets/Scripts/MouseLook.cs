using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float sensitivity;
    public Transform charBody;
    public float maxY;
    public float minY;
    private float rotY;
    private float rotX;

    public bool mouseHideOnStart;

    void Start() {
        if(mouseHideOnStart == true) {
            Cursor.visible = false;
        }
    }

    void Update () {
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, minY, maxY);
        charBody.eulerAngles = new Vector3(0, rotX, 0);
        transform.localEulerAngles = new Vector3(-rotY, 0, 0);
    }

    public void camRecoil(float recoil) {
        transform.Rotate(-recoil, 0, 0);
    }

    void OnDisable() {
        if (mouseHideOnStart == true) {
            Cursor.visible = true;
        }
    }
}
