using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public float damage;
    public float range;
    public float rateOfFire;

    public bool automatic;
    private float nextFire;

    public Camera fpsCam;
    public Transform shootPos;
    //public ParticleSystem muzzleEffect;
    public GameObject muzzleFlashGO;
    public GameObject bulletHitGO;
    public GameObject bulletHole;

    public KeyCode fire = KeyCode.Mouse0;
    public KeyCode aim = KeyCode.Mouse1;
    public KeyCode reload = KeyCode.R;

    public int ammo;
    public int magSize;

    public Text currentAmmoT;
    public int currentAmmo;
    public Text currentMagT;
    public int currentMag;
    public Text weaponNameT;
    public string weaponName;

    public AudioSource audS;
    public AudioClip gunShot;
    public AudioClip reloadNoise;

    public Recoil recoil;
    public float verticalRecoil;
    public float horizontalRecoil;

    void Start() {
        weaponNameT.text = weaponName;
        currentAmmo = ammo;
        currentMag = magSize;
        currentMagT.text = "" + currentMag;
        currentAmmoT.text = "/ " + currentAmmo;
    }

    void Update() {
        if (Input.GetKey(fire) && Time.time >= nextFire && automatic == true) {
            nextFire = Time.time + 1f / rateOfFire;
            shoot();
        }else if (Input.GetKeyDown(fire) && Time.time >= nextFire) {
            nextFire = Time.time + 1f / rateOfFire;
            shoot();
        }

        if (Input.GetKeyDown(reload)) {
            if (currentAmmo - (magSize - currentMag) >= 0)
            {
                currentAmmo -= (magSize - currentMag);
                currentMag = magSize;
                currentMagT.text = "" + currentMag;
                currentAmmoT.text = "/ " + currentAmmo;
                audS.PlayOneShot(reloadNoise);
                
            }
            else if (currentAmmo - (magSize - currentMag) < 0)
            {
                currentMag += currentAmmo;
                currentAmmo = 0;
                currentMagT.text = "" + currentMag;
                currentAmmoT.text = "/ " + currentAmmo;
                audS.PlayOneShot(reloadNoise);
                // For debugging. delete in final builds
                print("Out of ammo.");
                /* Need sound for this
                audS.PlayOneShot(outOfAmmoNoise);
                */
            }
        }
    }

    public void shoot() {
        if (currentMag > 0) {
            RaycastHit hit;
            currentMag -= 1;
            currentMagT.text = "" + currentMag;
            //audS.PlayOneShot(gunShot);

            if (Physics.Raycast(shootPos.transform.position, shootPos.transform.forward, out hit, range)) {
                //Debug.Log(hit.transform.name);

                Health health = hit.transform.GetComponent<Health>();
                if (health != null) {
                    health.takeDamage(damage);
                }

                GameObject impactGO = Instantiate(bulletHitGO, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject bulletHoleGO = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                bulletHoleGO.transform.parent = hit.collider.transform;
                Destroy(impactGO, 2.5f);
                Destroy(bulletHoleGO, 60.0f);
            }

            StartCoroutine(flash());
            recoil.camRecoil(verticalRecoil, horizontalRecoil);

        }

        else {
            print("RELOAD");
        }    
    }

    IEnumerator flash() {
        Vector3 euler = muzzleFlashGO.gameObject.transform.localEulerAngles;
        euler.x = Random.Range(0f, 360f);
        muzzleFlashGO.gameObject.transform.localEulerAngles = euler;
        muzzleFlashGO.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlashGO.SetActive(false);
    }
}
