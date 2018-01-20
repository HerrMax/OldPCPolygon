using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour {

    public Behaviour[] disableOnToggle;
    public Sway sway;
    public WeaponShoot weapon;
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
