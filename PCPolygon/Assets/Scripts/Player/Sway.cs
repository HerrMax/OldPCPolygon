using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {

    public float mouseX;
    public float mouseY;

    public bool menuOpen = false;

    public Quaternion rotationSpeed;

    public float speed;

    public void MenuOpen(bool isOpen)
    {
        menuOpen = isOpen;
    }

    void Update () {
        if(menuOpen == false)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }
        rotationSpeed = Quaternion.Euler(-mouseY, mouseX, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotationSpeed, speed * Time.deltaTime);
    }
}
