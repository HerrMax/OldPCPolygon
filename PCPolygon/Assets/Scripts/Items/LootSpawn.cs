using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootSpawn : NetworkBehaviour {

    public GameObject itemToSpawn;

    void Start()
    {
        if (isServer)
        {
            CmdSpawnObject();
        }
    }

    [Command]
    void CmdSpawnObject()
    {
        GameObject go = Instantiate(itemToSpawn, this.transform);
        NetworkServer.Spawn(go);
        Debug.Log("Spawned " + itemToSpawn.transform.name);
    }
}
