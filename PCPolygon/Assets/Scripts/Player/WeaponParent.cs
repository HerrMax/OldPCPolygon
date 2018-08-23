using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponParent : NetworkBehaviour {

    [SyncVar]
    public NetworkIdentity parentID;
    
    public void SetParent()
    {
        Debug.Log(parentID.netId);
        GameObject parentObject = NetworkServer.FindLocalObject(parentID.netId);
        transform.SetParent(parentObject.transform);
    }

}
