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

    private void Start()
    {
        EquipTool(primaryWeapon);
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

        //This line handles viewmodel spawning
        GameObject weaponInstance = (GameObject)Instantiate(weaponName.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();
        if (currentGraphics == null) Debug.LogError("NOGRAPHICS: " + weaponInstance.name);

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
