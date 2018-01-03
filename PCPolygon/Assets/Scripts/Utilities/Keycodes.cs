using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WIP
public class Keycodes
{
    public KeyCode shoot;
    public KeyCode aim;
    public KeyCode forwards;
    public KeyCode backwards;
    public KeyCode left;
    public KeyCode right;
    public KeyCode inventory;
    public KeyCode drop;

    void Start()
    {
        PlayerPrefs.GetString("shootkey", "Mouse0");
        PlayerPrefs.GetString("aimkey", "Mouse1");
        PlayerPrefs.GetString("forwardskey", "W");
        PlayerPrefs.GetString("backwardskey", "S");
        PlayerPrefs.GetString("leftkey", "A");
        PlayerPrefs.GetString("rightkey", "D");
        PlayerPrefs.GetString("inventorykey", "Tab");
        PlayerPrefs.GetString("dropkey", "Mouse3");
    }
}
