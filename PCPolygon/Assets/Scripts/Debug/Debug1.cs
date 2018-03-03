using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Debug1 : NetworkBehaviour
{
    public void Cake()
    {
        Debug.Log(name + " local player " + isLocalPlayer);
        Debug.Log(name + " local player authority " + localPlayerAuthority);
        Debug.Log(name + " player controller id " + playerControllerId);
        Debug.Log(name + " network id " + GetComponent<NetworkIdentity>().netId);
    }
}
