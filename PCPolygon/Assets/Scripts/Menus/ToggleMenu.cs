using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour {

    public Behaviour[] disableOnToggle;
    [HideInInspector] public Sway sway;
    [HideInInspector] public WeaponShoot weapon;
    [SerializeField] public Text pickupText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject inventory;
    [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey) && pauseMenu.active == false) toggleMenu(inventory);
        if (Input.GetKeyDown(pauseKey) && inventory.active == false) toggleMenu(pauseMenu);
    }

    public void toggleMenu(GameObject menu)
    {
        weapon.stopShooting();
        sway.MenuOpen(!menu.activeSelf);
        menu.SetActive(!menu.activeSelf);
        crosshair.SetActive(!menu.activeSelf);
        for (int i = 0; i < disableOnToggle.Length; i++)
        {
            disableOnToggle[i].enabled = !menu.activeSelf;
        }
    }
}
