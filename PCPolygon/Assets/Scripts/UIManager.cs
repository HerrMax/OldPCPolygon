using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private Behaviour[] disableOnToggle;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private GameObject inventory;
    [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;

    void Start()
    {
        if(inventory == null)
        {
            inventory = GameObject.FindWithTag("InventoryPanel");
            crosshair = GameObject.FindWithTag("Crosshair");
            inventory.SetActive(false);
        }
    }

    void toggle(GameObject menuToToggle)
    {
        inventory.active = !inventory.active;
        crosshair.active = !inventory.active;
        for (int i = 0; i < disableOnToggle.Length; i++)
        {
            disableOnToggle[i].enabled = !inventory.active;
        }
    }

	void Update () {
        if (Input.GetKeyDown(inventoryKey)) toggle(inventory);
	}
}
