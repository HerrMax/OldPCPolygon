using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField] Behaviour[] componentsToDisable;
    [SerializeField] Behaviour[] disableOnToggle;

    [SerializeField] string remoteLayer = "Remote";

    [SerializeField] GameObject playerUICanvas;
    [HideInInspector] public GameObject playerUIInstance;

    [SerializeField] string dontDrawLayer = "NoDraw";
    [SerializeField] GameObject playerGraphics;

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

            toggleMenu.disableOnToggle = this.disableOnToggle;
            toggleMenu.sway = this.GetComponentInChildren<Sway>();
            toggleMenu.weapon = this.GetComponent<WeaponShoot>();
        }

        GetComponent<Player>().Setup();
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
