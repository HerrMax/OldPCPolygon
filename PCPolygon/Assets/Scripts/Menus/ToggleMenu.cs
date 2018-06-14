using System;
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

    private void Start()
    {
        toggleMenu(inventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey) && pauseMenu.GetComponent<CanvasGroup>().alpha == 0) toggleMenu(inventory);
        if (Input.GetKeyDown(pauseKey) && inventory.GetComponent<CanvasGroup>().alpha == 0) toggleMenu(pauseMenu);
    }


    /// <summary>
    /// Toggles a menu
    /// <para>Requires a CanvasGroup on the object</para>
    /// </summary>
    /// <param name="menuObject"></param>
    public void toggleMenu(GameObject menuObject)
    {
        CanvasGroup menu = menuObject.GetComponent<CanvasGroup>();
        pickupText.text = null;
        weapon.stopShooting();
        sway.MenuOpen(!menu.interactable);
        if (menuObject == inventory)
        {
            menuObject.transform.GetChild(0).gameObject.SetActive(!menu.interactable);
            menuObject.transform.GetChild(1).gameObject.SetActive(!menu.interactable);
            menuObject.transform.GetChild(2).gameObject.SetActive(true);
            menuObject.transform.GetChild(3).gameObject.SetActive(!menu.interactable);
            menuObject.transform.GetChild(4).gameObject.SetActive(!menu.interactable);
            menuObject.transform.GetChild(5).GetComponent<Image>().enabled = !menu.interactable;
            menuObject.transform.GetChild(5).transform.GetChild(0).gameObject.SetActive(true);
            menuObject.transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(!menu.interactable);
            menuObject.transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(!menu.interactable);
        }
        //menu.alpha = Math.Abs(menu.alpha - 1);
        menu.blocksRaycasts = !menu.blocksRaycasts;
        menu.interactable = !menu.interactable;
        crosshair.SetActive(!menu.interactable);
        for (int i = 0; i < disableOnToggle.Length; i++)
        {
            disableOnToggle[i].enabled = !menu.interactable;
        }
        
    }
}
