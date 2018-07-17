using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    [SerializeField] private Slider healthbar;
    [SerializeField] private Text healthbarText;

    [SyncVar]
    private bool _isDead = false;
    public bool isDead {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField] GameObject[] disableGameObjectsOnDeath;

    [SerializeField] private GameObject deathParticles;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();

        //healthbar = GameObject.Find(gameObject.name + "'s UI").transform.GetChild(6).GetComponent<Slider>();
        //healthbar = GetComponent<PlayerSetup>().playerUIInstance.GetComponentInChildren<Slider>();
        //healthbar = GetComponent<PlayerSetup>().playerUIInstance.transform.GetChild(6).GetComponent<Slider>();

        healthbar = GetComponent<PlayerSetup>().playerUIInstance.transform.FindChild("HealthBar").GetComponent<Slider>();
        healthbarText = healthbar.GetComponentInChildren<Text>();
        UpdateHealth();
    }

    private void Update()
    {
        UpdateHealth();

        if (!isLocalPlayer) { return; }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isServer)
            {
                RpcTakeDamage(maxHealth);
            }
            else
            {
                CmdTakeDamage(maxHealth);
            }
        }
        //For testing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isServer)
            {
                RpcTakeDamage(20);
                Debug.Log("Player.cs Line 63-75");
            }
            else
            {
                CmdTakeDamage(20);
                Debug.Log("Player.cs Line 63-75");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PoisonPlayer(50,2);
        }
    }

    /// <summary>
    /// A method that damages the player over time
    /// </summary>
    /// <param name="time">The amount of time to damage the player for</param>
    /// <param name="DPS">The amount of damage to deal to the player every second</param>
    void PoisonPlayer(int time, int DPS)
    {
        StartCoroutine(SlowDamage(time, DPS));
    }

    IEnumerator SlowDamage(int time, int DPS)
    {
        float damage = DPS / 2;

        for (int i = 0; i <= 2*time; i++)
        {
            if(currentHealth > 0)
            {
                if (isServer)
                {
                    RpcTakeDamage(damage);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    CmdTakeDamage(damage);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    [Command]
    public void CmdTakeDamage(float amount)
    {
        RpcTakeDamage(maxHealth);
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        //healthbar.value = CalculateHealth();
        //healthbarText.text = " + " + currentHealth;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        GameObject instance = (GameObject)Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(instance, 5f);


        if (isLocalPlayer)
        {
            GameManager.singleton.SetSceneCameraActive(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }

        Debug.Log(transform.name + " is dead.");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.singleton.gameSettings.respawnTime);

        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.1f);

        SetDefaults();
        UpdateHealth();

        Debug.Log(transform.name + " has respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < wasEnabled.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;

        if (isLocalPlayer)
        {
            GameManager.singleton.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }
    }

    public void UpdateHealth()
    {
        healthbar.value = currentHealth / maxHealth;
        healthbarText.text = " + " + currentHealth;
    }
}
