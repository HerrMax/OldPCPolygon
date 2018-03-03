using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour {

    public Transform dropPos;
    public Inventory inventory;

    //public string slugA;

    //This is temporary, it will eventually be replaced with the item slug.
    public GameObject objToDrop;


    /*void Update () {
        if(!isLocalPlayer) { return; }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdDropItem("augarino");
        }
    }*/

    public void DropItem(string slug)
    {
        CmdDropItem(slug);
    }

    [Command]
    void CmdDropItem(string slug)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(slug), dropPos);
        go.transform.parent = null;
        NetworkServer.Spawn(go);
        Debug.Log("Spawned " + go.transform.name);
    }

    /*[Command]
    void CmdDropItem(string slug)
    {
        RpcDropObj(slug);
    }

    [ClientRpc]
    void RpcDropObj(string slug)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(slug), dropPos);
        go.transform.parent = null;
        NetworkServer.Spawn(go);
        Debug.Log("Spawned " + go.transform.name);
    }*/
}
