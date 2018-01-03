using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private Behaviour[] disableOnToggle;
    [SerializeField] private Sway sway;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private GameObject inventory;
    [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;

    void Start()
    {
        if(inventory == null)
        {
            inventory = GameObject.FindWithTag("InventoryPanel");
            crosshair = GameObject.FindWithTag("Crosshair");
            //inventory.SetActive(false);
        }
    }

    void toggle(GameObject menuToToggle)
    {
        sway.MenuOpen(inventory.activeSelf);
        inventory.SetActive(!inventory.activeSelf);
        crosshair.SetActive(!inventory.activeSelf);
        //inventory.active = !inventory.active;
        //crosshair.active = !inventory.active;
        for (int i = 0; i < disableOnToggle.Length; i++)
        {
            disableOnToggle[i].enabled = !inventory.activeSelf;
        }
    }

	void Update () {
        if (Input.GetKeyDown(inventoryKey)) toggle(inventory);
	}
}
