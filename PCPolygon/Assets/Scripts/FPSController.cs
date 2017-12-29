using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour {

    public float walkSpeed = 10.0f;
    public float runSpeed = 20.0f;
    public float currentSpeed;
    public float gravity = 10.0f;
    public float jumpForce = 8.0f;

    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode jump = KeyCode.Space;
    
    private CharacterController charCont;
    private Vector3 moveDir = Vector3.zero;

    void Start() {
        charCont = this.GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }

    void Update() {
        if (Input.GetKey(sprint)) {
            currentSpeed = runSpeed;
        }
        else {
            currentSpeed = walkSpeed;
        }

        if (charCont.isGrounded) {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= currentSpeed;

            if (Input.GetKeyDown(jump) && charCont.isGrounded)
            {
                moveDir.y = jumpForce;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        charCont.Move(moveDir * Time.deltaTime);
    }
}