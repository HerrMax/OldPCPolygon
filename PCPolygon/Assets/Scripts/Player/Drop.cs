using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour {

    public Transform dropPos;
    public Inventory inventory;

    public void DropItem(string slug)
    {
        CmdDropItem(slug);
    }

    [Command]
    void CmdDropItem(string slug)
    {
        //GameObject go = (GameObject)Instantiate(Resources.Load(slug), dropPos);
        GameObject go = (GameObject)Instantiate(Resources.Load(slug), transform);
        go.transform.parent = null;
        //go.GetComponent<Rigidbody>().AddRelativeForce(0,0,200);
        NetworkServer.Spawn(go);
        //Debug.Log("Spawned " + go.transform.name);
    }
}
