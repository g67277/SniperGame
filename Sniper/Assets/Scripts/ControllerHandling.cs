using UnityEngine;
using System.Collections;

public class ControllerHandling : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    GameObject pickedUpObject;
    bool canPickup = false;

    Sniper sniper;
    Weapons weapons;

    Vector3 lastObjectPosition;
    Quaternion lastObjectRotation;

    GunScript gscript;

    // Update is called once per frame
    void Update () {

        // If there are less then 3 children, return the hands
        if (controller.transform.childCount < 3) {
            this.gameObject.transform.Find("hand").gameObject.SetActive(true);
        }

        var device = SteamVR_Controller.Input((int)controller.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip)) {
            if (controller.transform.childCount > 2) {
                Debug.Log("Testing: " + this.gameObject.transform.GetChild(2).tag);
                if (this.gameObject.transform.GetChild(2).tag == "Scope") {

                    this.gameObject.transform.GetChild(2).transform.position = this.gameObject.transform.GetChild(2).GetComponent<Scope>().lastScopePosition;
                    this.gameObject.transform.GetChild(2).transform.rotation = Quaternion.Euler(this.gameObject.transform.GetChild(2).GetComponent<Scope>().lastScopeRotation);
                    this.gameObject.transform.GetChild(2).transform.parent = null;
                } else {
                    this.gameObject.transform.GetChild(2).transform.position = lastObjectPosition;
                    this.gameObject.transform.GetChild(2).transform.rotation = lastObjectRotation;
                    this.gameObject.transform.GetChild(2).transform.parent = null;
                }
            } else if (canPickup) {
                objectPickup();
            }
        }

    }

    void objectPickup() {

        this.gameObject.transform.Find("hand").gameObject.SetActive(false);

        // Save object's current positions before picking it up
        if (pickedUpObject.tag == "Scope") {
            //Do nothing
        } else {
            lastObjectPosition = pickedUpObject.transform.position;
            lastObjectRotation = pickedUpObject.transform.rotation;
        }
        // Add the sniper to the parent controller
        pickedUpObject.transform.parent = controller.transform;

        // Pass the controller instance to the sniper
        sniper = pickedUpObject.GetComponent<Sniper>();
        gscript = pickedUpObject.GetComponent<GunScript>();
        // Manages the different characteristics of the weapons
        weapons = ScriptableObject.CreateInstance("Weapons") as Weapons;
        if (pickedUpObject.name == "Sniper4" || pickedUpObject.name == "Sniper2" || pickedUpObject.name == "Sniper6") {
            gscript.isPickedUp = true;
            gscript.controller = controller;
            weapons.setupWeapon(pickedUpObject);
        } 
        canPickup = false;

    }

    void OnTriggerEnter(Collider collider) {
        
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device != null) {

            if (collider.gameObject.tag == "Pickable" || collider.gameObject.tag == "Magazine" 
                || collider.gameObject.tag == "Magazine2" || collider.gameObject.tag == "Magazine3" 
                || collider.gameObject.tag == "Scope" && controller.transform.childCount < 3) {
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
