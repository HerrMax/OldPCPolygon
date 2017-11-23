using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private Behaviour[] disableComponents;
    private bool[] wasEnabled;

    [SyncVar]
    public int currentHealth;

    [SyncVar]
    private bool isDeadSync = false;
    public bool isDead {
        get { return isDeadSync; }
        protected set { isDeadSync = value; }
    }

	public void Setup () {
        wasEnabled = new bool[disableComponents.Length];
        for (int i = 0; i < wasEnabled.Length; i++) {
            wasEnabled[i] = disableComponents[i].enabled;
        }

        SetStats();
	}

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            RpcDamage(maxHealth);
        }

    }
	
    [ClientRpc]
    public void RpcDamage(int amount)
    {
        if (isDeadSync) return;
        if (isServer) return;

        currentHealth -= amount;

        Debug.Log(transform.name + " has " + currentHealth + " health.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        Debug.Log("Die1");

        for (int i = 0; i < disableComponents.Length; i++)
        {
            disableComponents[i].enabled = false;
        }

        Debug.Log("Die2");

        Collider playerCol = GetComponent<Collider>();
        if (playerCol != null)
        {
            playerCol.enabled = false;
        }

        Debug.Log(transform.name + " died.");

        StartCoroutine(Respawn(3f));
    }

    IEnumerator Respawn(float timeToRespawn)
    {
        yield return new WaitForSeconds(timeToRespawn);

        SetStats();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Debug.Log(transform.name + " has respawned!");
    }

    public void SetStats()
    {
        isDead = false;

        if (isLocalPlayer)
        {
            currentHealth = maxHealth;
        }

        for (int i = 0; i < disableComponents.Length; i++) {
            disableComponents[i].enabled = wasEnabled[i];
        }

        Collider playerCol = GetComponent<Collider>();
        if (playerCol != null) {
            playerCol.enabled = true;
        }
    }
}
