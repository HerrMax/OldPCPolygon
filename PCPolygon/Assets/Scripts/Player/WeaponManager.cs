using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;
    [SerializeField] private Weapon tertiaryWeapon1;
    [SerializeField] private Weapon tertiaryWeapon2;
    [SerializeField] private Weapon tertiaryWeapon3;

    [SerializeField] private Transform weaponHolder;

    [SerializeField] private string viewmodelLayerName = "Viewmodel";

    private Weapon currentWeapon;

    private void Start()
    {
        EquipTool(primaryWeapon);
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    void EquipTool(Weapon weaponName)
    {
        currentWeapon = weaponName;

        GameObject weaponInstance = (GameObject)Instantiate(weaponName.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);
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
