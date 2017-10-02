using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Username : NetworkBehaviour {

    public Text usernameT;

	void Start () {
        usernameT.text = "Player " + GetComponent<NetworkIdentity>().netId;
	}
}
