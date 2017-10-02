using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour {

    public Vector3 originalPos;
    public Vector3 ironAimPos;
    public GameObject ironSights;
    public Vector3 scopeAimPos;
    public GameObject scope;
    public Vector3 redDotAimPos;
    public GameObject redDotSight;
    public Vector3 holoAimPos;
    public GameObject holosight;

    public GameObject weapon;
    public Camera cam;
    public float nextField, dampVelocity;

    public int normalFOV = 80, aimFOV;

    public MouseLook mouseLook;
    private float currentSensitivity, newSensitivity;

    public float duration = 0.3F;

    public KeyCode aim = KeyCode.Mouse1;

    public FPSController fpsCont;
    private float walkSpeed, walkAimSpeed, runSpeed, runAimSpeed;

    void Start() {
        aimFOV = normalFOV - 20;
        walkSpeed = fpsCont.walkSpeed;
        walkAimSpeed = walkSpeed / 2;
        runSpeed = fpsCont.runSpeed;
        runAimSpeed = runSpeed / 2;
        currentSensitivity = mouseLook.sensitivity;
        newSensitivity = currentSensitivity / 3;
    }

    void Update() {
        float newFOV = Mathf.SmoothDamp(cam.fieldOfView, nextField, ref dampVelocity, duration);
        cam.fieldOfView = newFOV;

        if (Input.GetKey(aim)) {
            fpsCont.walkSpeed = walkAimSpeed;
            fpsCont.runSpeed = runAimSpeed;
            mouseLook.sensitivity = newSensitivity;
            nextField = aimFOV;
            if (ironSights.active) {
                //print("ironySights");
                weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, ironAimPos, duration);
            } else if (redDotSight.active) {
                //print("redSights");
                weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, redDotAimPos, duration);
            } else if (scope.active) {
                //print("scope");
                weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, scopeAimPos, duration);
            }
            else if (holosight.active) {
                //print("holosight");
                weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, holoAimPos, duration);
            } else {
                //print("none");
                weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, ironAimPos, duration);
            }
        }
        else {
            fpsCont.walkSpeed = walkSpeed;
            fpsCont.runSpeed = runSpeed;
            mouseLook.sensitivity = currentSensitivity;
            nextField = normalFOV;
            weapon.transform.localPosition = Vector3.MoveTowards(weapon.transform.localPosition, originalPos, duration);
        }
    }
}
