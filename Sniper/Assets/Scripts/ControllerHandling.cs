using UnityEngine;
using System.Collections;

public class ControllerHandling : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    GameObject pickedUpObject;
    bool canPickup = false;

    Sniper sniper;
    Weapons weapons;

    //test
    GunScript gscript;

    // Update is called once per frame
    void Update () {

        // If there are less then 3 children, return the hands
        if (controller.transform.childCount < 3) {
            this.gameObject.transform.Find("hand").gameObject.SetActive(true);
        }

        if (canPickup) {
            var device = SteamVR_Controller.Input((int)controller.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip)) {
                if (controller.transform.childCount > 2) {
                    this.gameObject.transform.GetChild(2).transform.position = pickedUpObject.transform.position;
                    this.gameObject.transform.GetChild(2).transform.rotation = pickedUpObject.transform.rotation;
                    this.gameObject.transform.GetChild(2).transform.parent = null;
                    objectPickup();
                } else {
                    objectPickup();
                }
            }
        }
    }

    void objectPickup() {

        this.gameObject.transform.Find("hand").gameObject.SetActive(false);
        // Add the sniper to the parent controller
        pickedUpObject.transform.parent = controller.transform;

        // Pass the controller instance to the sniper
        sniper = pickedUpObject.GetComponent<Sniper>();
        gscript = pickedUpObject.GetComponent<GunScript>();
        // Manages the different characteristics of the weapons
        weapons = ScriptableObject.CreateInstance("Weapons") as Weapons;
        if (pickedUpObject.name == "Sniper4") {
            gscript.isPickedUp = true;
            gscript.controller = controller;
            weapons.setupWeapon(pickedUpObject);
        } else if (pickedUpObject.name == "Sniper2") {
            sniper.isPickedUp = true;
            sniper.controller = controller;
            weapons.setupWeapon(pickedUpObject);
        } else if (pickedUpObject.name == "Sniper6") {
            gscript.isPickedUp = true;
            gscript.controller = controller;
            weapons.setupWeapon(pickedUpObject);
        }
        canPickup = false;

    }

    void OnTriggerEnter(Collider collider) {
        
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device != null) {
            Debug.Log("Testing: " + collider.gameObject.tag);

            if (collider.gameObject.tag == "Pickable" || collider.gameObject.tag == "Magazine" || collider.gameObject.tag == "Scope") {
                Debug.Log("Testing2: " + collider.gameObject.tag);
                pickedUpObject = collider.gameObject;
                canPickup = true;
            }
            
        }
    }

    void OnTriggerExit() {
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device != null) {
            canPickup = false;
            pickedUpObject = null;
        }
    }



}
