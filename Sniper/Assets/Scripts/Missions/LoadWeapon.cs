using UnityEngine;
using System.Collections;

public class LoadWeapon : MonoBehaviour {

    [Header("Sniper Selected")]
    public GameObject sniper1;
    public GameObject sniper2;
    public GameObject sniper3;

    [Header("Scope Selected")]
    public GameObject scope1;
    public GameObject scope2;
    public GameObject scope3;

    [Header("Coordinates to position the weapon")]
    public Vector3 position;
    public Vector3 rotation;

    GameObject loadedWeapon;
    GameObject loadedScope;

    // Use this for initialization
    void Start () {

        string weaponName = DataHolder.missionWeapon;
        string scopeName = DataHolder.missionScope;
        Debug.Log("Scope: " + DataHolder.missionScope);
        switch (weaponName) {
            case "Sniper1":
                loadedWeapon = Instantiate(sniper1, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.name = "Sniper1";
                break;
            case "Sniper2":
                loadedWeapon = Instantiate(sniper2, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.name = "Sniper2";
                break;
            case "Sniper3":
                loadedWeapon = Instantiate(sniper3, position, Quaternion.Euler(rotation)) as GameObject;
                loadedWeapon.name = "Sniper3";
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

        //Remove the weapon prefs
        DataHolder.deleteData();
        DataHolder.saveData(); //Testing
    }

    void addScope() {
        loadedWeapon.transform.GetChild(0).GetComponent<ClipSniper>().clip(loadedScope.GetComponent<Collider>());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
