using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField] Behaviour[] componentsToDisable;
    [SerializeField] Behaviour[] disableOnToggle;

    [SerializeField] string remoteLayer = "Remote";

    [SerializeField] GameObject playerUICanvas;
    [HideInInspector] public GameObject playerUIInstance;
    [SerializeField] private Text playerText;
    [SerializeField] private Pickup pickup;

    [SerializeField] string dontDrawLayer = "NoDraw";
    [SerializeField] GameObject playerGraphics;

    [SerializeField] [SyncVar] public int skinTone;
    [SerializeField] private Material[] skinTones;

    void Start () {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayer));

            playerUIInstance = Instantiate(playerUICanvas);
            playerUIInstance.name = transform.name + "'s UI";

            ToggleMenu toggleMenu = playerUIInstance.GetComponent<ToggleMenu>();

            pickup.pickupText = toggleMenu.pickupText;
            pickup.inventory = playerUIInstance.GetComponentInChildren<Inventory>();

            playerUIInstance.GetComponentInChildren<Inventory>().weaponManager = GetComponent<WeaponManager>();

            toggleMenu.disableOnToggle = this.disableOnToggle;
            toggleMenu.sway = this.GetComponentInChildren<Sway>();
            toggleMenu.weapon = this.GetComponent<WeaponShoot>();

            playerUIInstance.GetComponentInChildren<Inventory>().drop = GetComponent<Drop>();

            skinTone = Random.Range(0, skinTones.Length);
        }

        CmdSetSkinTone(skinTone);

        GetComponent<Player>().Setup();
    }

    [Command]
    void CmdSetSkinTone(int skinTone)
    {
        RpcSetSkinTone(skinTone);
    }

    void RpcSetSkinTone(int skinTone)
    {
        playerGraphics.GetComponent<Renderer>().material = skinTones[skinTone];
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayer);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if (isLocalPlayer)
        {
            GameManager.singleton.SetSceneCameraActive(true);
        }

        GameManager.DeregisterPlayer(transform.name);
    }

}
