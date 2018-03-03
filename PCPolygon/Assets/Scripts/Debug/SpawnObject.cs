using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnObject : NetworkBehaviour {

    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject objToSpawn;

    private void Update()
    {
        //if (!isLocalPlayer) { return; }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdSpawnObj();
        }
    }

    /*[Client]
    void SpawnObj(GameObject obj)
    {
        GameObject go = Instantiate(obj, spawnPos);
        NetworkServer.Spawn(go);
        
        go.transform.parent = null;
    }*/

    [Command]
    void CmdSpawnObj()
    {
        GameObject go = Instantiate(objToSpawn, spawnPos);
        go.transform.parent = null;
        NetworkServer.Spawn(go);
        Debug.Log("Spawned " + go.transform.name);
    }
}
