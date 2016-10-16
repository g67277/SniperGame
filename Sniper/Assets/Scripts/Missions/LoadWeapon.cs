using UnityEngine;
using System.Collections;

public class LoadWeapon : MonoBehaviour {

    public int bulletsAllowed;

    [Header("Sniper Selected")]
    public GameObject sniper1;
    public GameObject sniper2;
    public GameObject sniper3;

    [Header("Scope Selected")]
    public GameObject scope1;
    public GameObject scope2;
    public GameObject scope3;

    [Header("Ammo prefab")]
    public GameObject sniper1Ammo;
    public GameObject sniper2Ammo;
    public GameObject sniper3Ammo;

    [Header("Coordinates to position the weapon")]
    public GameObject weaponParent;
    public Vector3 position;
    public Vector3 rotation;

    [Header("Coordinates for the ammo")]
    public Vector3 ammoPosition;
    public Vector3 ammoRotation;
    float staticX;

    GameObject loadedWeapon;
    GameObject loadedScope;
    GameObject loadedAmmo;


    // Use this for initialization
    void Start () {

        string weaponName = DataHolder.missionWeapon;
        string scopeName = DataHolder.missionScope;
        Debug.Log("Scope: " + DataHolder.missionScope);
        switch (weaponName) {
            case "Sniper1":
                loadedWeapon = Instantiate(sniper1, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.transform.parent = weaponParent.transform;
                loadedWeapon.transform.localPosition = position;
                loadedWeapon.name = "Sniper1";
                addAmmo(1);
                break;
            case "Sniper2":
                loadedWeapon = Instantiate(sniper2, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.transform.parent = weaponParent.transform;
                loadedWeapon.transform.localPosition = position;
                loadedWeapon.name = "Sniper2";
                addAmmo(2);
                break;
            case "Sniper3":
                Debug.Log("Incoming weapon position: " + position);
                loadedWeapon = Instantiate(sniper3, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.transform.parent = weaponParent.transform;
                loadedWeapon.transform.localPosition = position;
                loadedWeapon.name = "Sniper3";
                addAmmo(3);
                break;
            default:
                Debug.Log("Sniper is Not Valid");
                break;
        }
        if (scopeName != "" && weaponName == "Sniper3") {
            switch (scopeName) {
                case "Scope x5":
                    loadedScope = Instantiate(scope1, position, Quaternion.Euler(rotation)) as GameObject;
                    loadedScope.name = "Scope x5";
                    addScope();
                    break;
                case "Scope x7":
                    loadedScope = Instantiate(scope2, position, Quaternion.Euler(rotation)) as GameObject;
                    loadedScope.name = "Scope x7";
                    addScope();
                    break;
                case "Scope x11":
                    loadedScope = Instantiate(scope3, position, Quaternion.Euler(rotation)) as GameObject;
                    loadedScope.name = "Scope x11";
                    Debug.Log("Scope name: " + loadedScope.name);
                    addScope();
                    break;
                default:
                    Debug.Log("Scope is Not Valid");
                    break;
            }
        }

    }

    void addAmmo(int ammoType) {
        if (ammoType == 1) {
            int magazineCount = bulletsAllowed / 6;
            for (int i = 0; i < magazineCount; i++) {
                loadedAmmo = Instantiate(sniper1Ammo, ammoPosition, Quaternion.Euler(ammoRotation)) as GameObject;
                loadedAmmo.transform.parent = weaponParent.transform;
                loadedAmmo.transform.localPosition = ammoPosition;
                if (i > 0) {
                    ammoPosition = new Vector3(ammoPosition.x, ammoPosition.y, ammoPosition.z - 0.08f);
                    loadedAmmo.transform.localPosition = ammoPosition;
                }
            }
        }else if (ammoType == 2) {
            int magazineCount = bulletsAllowed;
            staticX = ammoPosition.x;
            for (int i = 0; i < magazineCount; i++) {
                loadedAmmo = Instantiate(sniper2Ammo, ammoPosition, Quaternion.Euler(ammoRotation)) as GameObject;
                loadedAmmo.transform.parent = weaponParent.transform;
                loadedAmmo.transform.localPosition = ammoPosition;
                if (i > 0 && i < 7) {
                    ammoPosition = new Vector3(ammoPosition.x, ammoPosition.y, ammoPosition.z - 0.05f);
                    loadedAmmo.transform.localPosition = ammoPosition;
                }else if (i > 6 && i < 14) {
                    if (i == 7) {
                        ammoPosition = new Vector3(staticX + 0.05f, ammoPosition.y, ammoPosition.z);
                    }else {
                        ammoPosition = new Vector3(staticX + 0.05f, ammoPosition.y, ammoPosition.z + 0.05f);
                    }
                    loadedAmmo.transform.localPosition = ammoPosition;
                } else if(i > 13) {
                    if (i == 14) {
                        ammoPosition = new Vector3(staticX + 0.1f, ammoPosition.y, ammoPosition.z);
                    } else {
                        ammoPosition = new Vector3(staticX + 0.1f, ammoPosition.y, ammoPosition.z - 0.05f);
                    }
                    loadedAmmo.transform.localPosition = ammoPosition;
                }
            }
        }else if (ammoType == 3) {
            int magazineCount = bulletsAllowed / 12;
            for (int i = 0; i < magazineCount; i++) {
                loadedAmmo = Instantiate(sniper3Ammo, ammoPosition, Quaternion.Euler(ammoRotation)) as GameObject;
                loadedAmmo.transform.parent = weaponParent.transform;
                loadedAmmo.transform.localPosition = ammoPosition;
                if (i > 0) {
                    ammoPosition = new Vector3(ammoPosition.x, ammoPosition.y + 0.03f, ammoPosition.z - 0.08f);
                    loadedAmmo.transform.localPosition = ammoPosition;
                }
            }
        }
    }

    void addScope() {
        loadedWeapon.transform.GetChild(0).GetComponent<ClipSniper>().clip(loadedScope.GetComponent<Collider>());
    }
}
