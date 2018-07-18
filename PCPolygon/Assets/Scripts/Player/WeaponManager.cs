﻿using System.Collections;
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

    private void Start()
    {
        EquipTool(primaryWeapon);
    }

    public void SwapWeapon(string item, int slot)
    {
        UnequipTool(currentTool);
        if (slot == 1)
        {
            EquipTool(primaryWeapon);
        }
        if (slot == 2)
        {
            EquipTool(secondaryWeapon);
        }
        if (slot == 3)
        {
            EquipTool(tertiaryWeapon);
        }
        if (slot == 4)
        {
            EquipTool(quaternaryWeapon);
        }
        if (slot == 5)
        {
            EquipTool(quinaryWeapon);
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnequipTool(currentTool);
            EquipTool(primaryWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnequipTool(currentTool);
            EquipTool(secondaryWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnequipTool(currentTool);
            EquipTool(tertiaryWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnequipTool(currentTool);
            EquipTool(quaternaryWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UnequipTool(currentTool);
            EquipTool(quinaryWeapon);
        }*/
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
        GameObject weaponInstance = (GameObject)Instantiate(Resources.Load("Viewmodels/"+currentWeapon.name), weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);

        currentTool = weaponInstance;

        currentGraphics = weaponInstance.GetComponent<WeaponGraphics>();
        if (currentGraphics == null) Debug.LogError("NOGRAPHICS: " + weaponInstance.name);

        if (isLocalPlayer)
        {
            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(viewmodelLayerName));
        }
    }

    void UnequipTool(GameObject currentToola)
    {
        Destroy(currentTool);
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
