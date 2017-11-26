using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    private void Awake()
    {
        setDefaults();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");
    }

    public void setDefaults()
    {
        currentHealth = maxHealth;
    }
}
