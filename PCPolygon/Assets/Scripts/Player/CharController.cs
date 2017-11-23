using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class CharController : MonoBehaviour {

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public bool grounded = false;

    public Rigidbody rigid;

    void Awake () {
        rigid = this.GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.useGravity = false;
	}
	
	void FixedUpdate () {
        if (grounded)
        {
            Vector3 targetVeolocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVeolocity = transform.TransformDirection(targetVeolocity);
            targetVeolocity *= speed;

            Vector3 velocity = rigid.velocity;
            Vector3 velocityChange = (targetVeolocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigid.AddForce(velocityChange, ForceMode.VelocityChange);

            if (canJump && Input.GetKeyDown(KeyCode.Space))
            {
                rigid.velocity = new Vector3(velocity.x, CalculateJumpVeritcalSpeed(), velocity.z);
            }
        }
        rigid.AddForce(new Vector3(0, -gravity * rigid.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVeritcalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
