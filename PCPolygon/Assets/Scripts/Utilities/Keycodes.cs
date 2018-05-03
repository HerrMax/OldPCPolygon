using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WIP(ish)
public static class Keycodes
{
    static public KeyCode shoot;
    static public KeyCode aim;
    static public KeyCode forwards;
    static public KeyCode backwards;
    static public KeyCode left;
    static public KeyCode right;
    static public KeyCode jump;
    static public KeyCode sprint;
    static public KeyCode crouch;
    static public KeyCode inventory;
    static public KeyCode drop;
    static public KeyCode use;

    public static void GetKeycodes()
    {
        shoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootkey", "Mouse0"));
        aim = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("aimkey", "Mouse1"));
        forwards = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardskey", "W"));
        backwards = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardskey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkey", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpkey", "Space"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sprintkey", "LeftShift"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchkey", "LeftControl"));
        inventory = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("inventorykey", "Tab"));
        drop = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dropkey", "Mouse3"));
        use = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("usekey", "F"));
    }
}