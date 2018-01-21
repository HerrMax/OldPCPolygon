using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour {

    [HideInInspector] public Behaviour[] disableOnToggle;
    [HideInInspector] public Sway sway;
    [HideInInspector] public WeaponShoot weapon;
    [SerializeField] public Text pickupText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject inventory;
    [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey)) toggleMenu(inventory);
    }

    public void toggleMenu(GameObject menu)
    {
        weapon.stopShooting();
        sway.MenuOpen(!menu.activeSelf);
        menu.SetActive(!menu.activeSelf);
        crosshair.SetActive(!menu.activeSelf);
        //inventory.active = !inventory.active;
        //crosshair.active = !inventory.active;
        for (int i = 0; i < disableOnToggle.Length; i++)
        {
            disableOnToggle[i].enabled = !inventory.activeSelf;
        }
    }
}
