using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WIP(ish)
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
    public KeyCode use;

    void Start()
    {
        shoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootkey", "Mouse0"));
        aim = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimkey", "Mouse1"));
        forwards = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardskey", "W"));
        backwards = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardskey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkey", "D"));
        inventory = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("inventorykey", "Tab"));
        drop = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dropkey", "Mouse3"));
        use = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("usekey", "F"));
    }
}