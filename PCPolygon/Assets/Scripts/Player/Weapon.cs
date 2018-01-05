using UnityEngine;

[System.Serializable]
public class Weapon {
    public string name = "Augarino";
    public float damage = 10f;
    public float range = 100f;
    public bool isAutomatic = true;
    public float rateOfFire = 11f;

    public int verticalRecoi = 1;
    public int horizontalRecoil = 1;
    public int additionalHorizontalRecoil = 1;

    public GameObject graphics;
}
