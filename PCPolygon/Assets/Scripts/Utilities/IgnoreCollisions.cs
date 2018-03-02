using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour {

    void OnCollisionEnter (Collision collider) {
		if(collider.gameObject.tag == "player")
        {
            //Physics.IgnoreCollision(collider.collider, this.gameObject.collider);
        }
	}
}
