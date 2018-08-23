using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;
    [SerializeField] private Weapon tertiaryWeapon;
    [SerializeField] private Weapon quaternaryWeapon;
    [SerializeField] private Weapon quinaryWeapon;

    [SerializeField] private Transform weaponHolder;

    [SerializeField] private string viewmodelLayerName = "Viewmodel";

    private Weapon currentWeapon;
    private WeaponGraphics currentGraphics;

    public GameObject currentTool;

    public void SwapWeapon(string item, int slot)
    {
        if (slot == 0)
        {
            primaryWeapon.name = item;
            EquipTool(primaryWeapon);
        }
        if (slot == 1)
        {
            secondaryWeapon.name = item;
            EquipTool(secondaryWeapon);
        }
        if (slot == 2)
        {
            tertiaryWeapon.name = item;
            EquipTool(tertiaryWeapon);
        }
        if (slot == 3)
        {
            quaternaryWeapon.name = item;
            EquipTool(quaternaryWeapon);
        }
        if (slot == 4)
        {
            quinaryWeapon.name = item;
            EquipTool(quinaryWeapon);
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipTool(Weapon weaponName)
    {
        currentWeapon = weaponName;

        if(weaponName.name == null)
        {
            CmdUnEquipTool(currentTool);
            CmdSpawnNullItem();
        }
        else
        {
            CmdUnEquipTool(currentTool);
            CmdSpawnWeapon(currentWeapon.name);
        }
    }

    [Command]
    void CmdSpawnWeapon(string weaponName)
    {

        GameObject weaponInstance = (GameObject)Instantiate(Resources.Load("Viewmodels/" + weaponName), weaponHolder.position, weaponHolder.rotation);
        currentTool = weaponInstance;

        weaponHolder.GetComponent<WeaponParent>().parentID = this.GetComponent<NetworkIdentity>();

        NetworkServer.Spawn(weaponInstance);

        weaponHolder.GetComponent<WeaponParent>().SetParent();

        currentTool = weaponInstance;

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();
        if (currentGraphics == null) Debug.LogError("NOGRAPHICS: " + weaponInstance.name);

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }

        //RpcSpawnWeapon(weaponName);
    }

    [ClientRpc]
    void RpcSpawnWeapon(string weaponName)
    {
        GameObject weaponInstance = (GameObject)Instantiate(Resources.Load("Viewmodels/" + weaponName), weaponHolder.position, weaponHolder.rotation);

        weaponHolder.GetComponent<WeaponParent>().parentID = weaponHolder.GetComponent<NetworkIdentity>();
        weaponInstance.transform.parent = weaponHolder;

        NetworkServer.Spawn(weaponInstance);

        weaponHolder.GetComponent<WeaponParent>().SetParent();

        


        currentTool = weaponInstance;

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();
        if (currentGraphics == null) Debug.LogError("NOGRAPHICS: " + weaponInstance.name);

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }
    }

    [Command]
    void CmdSpawnNullItem()
    {

        Debug.LogWarning("NO VIEWMODEL FOR " + currentWeapon.name);

        GameObject weaponInstance = (GameObject)Instantiate(Resources.Load("Viewmodels/Null"), weaponHolder.position, weaponHolder.rotation);

        NetworkServer.Spawn(weaponInstance);

        weaponInstance.GetComponent<WeaponParent>().parentID = weaponHolder.GetComponent<NetworkIdentity>();
        weaponInstance.transform.parent = weaponHolder;

        weaponHolder.GetComponent<WeaponParent>().SetParent();

        currentTool = weaponInstance;

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }

        //RpcSpawnNullItem();
    }

    [ClientRpc]
    void RpcSpawnNullItem()
    {
        Debug.LogWarning("NO VIEWMODEL FOR " + currentWeapon.name);

        GameObject weaponInstance = (GameObject)Instantiate(Resources.Load("Viewmodels/Null"), weaponHolder.position, weaponHolder.rotation);

        NetworkServer.Spawn(weaponInstance);

        weaponInstance.GetComponent<WeaponParent>().parentID = weaponHolder.GetComponent<NetworkIdentity>();
        weaponInstance.transform.parent = weaponHolder;

        currentTool = weaponInstance;

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }
    }

    [Command]
    void CmdUnEquipTool(GameObject currentTool)
    {
        RpcDestroyItem(currentTool);
    }

    [ClientRpc]
    void RpcDestroyItem(GameObject currentTool)
    {
        NetworkServer.Destroy(currentTool);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void SetWeaponStats(int slot, int damage, int range, bool isAuto, int ROF)
    {
        if(slot == 0)
        {
            primaryWeapon.damage = damage;
            primaryWeapon.range = range;
            primaryWeapon.isAutomatic = isAuto;
            primaryWeapon.rateOfFire = ROF;
        }
        if (slot == 1)
        {
            secondaryWeapon.damage = damage;
            secondaryWeapon.range = range;
            secondaryWeapon.isAutomatic = isAuto;
            secondaryWeapon.rateOfFire = ROF;
        }
        if (slot == 2)
        {
            tertiaryWeapon.damage = damage;
            tertiaryWeapon.range = range;
            tertiaryWeapon.isAutomatic = isAuto;
            tertiaryWeapon.rateOfFire = ROF;
        }
        if (slot == 3)
        {
            quaternaryWeapon.damage = damage;
            quaternaryWeapon.range = range;
            quaternaryWeapon.isAutomatic = isAuto;
            quaternaryWeapon.rateOfFire = ROF;
        }
        if (slot == 4)
        {
            quinaryWeapon.damage = damage;
            quinaryWeapon.range = range;
            quinaryWeapon.isAutomatic = isAuto;
            quinaryWeapon.rateOfFire = ROF;
        }
    }
}
