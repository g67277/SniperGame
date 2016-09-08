using UnityEngine;
using System.Collections;

public class ControllerHandling : MonoBehaviour {

    public SteamVR_TrackedObject controller;
    GameObject pickupObject;
    bool canPickup = false;

    Sniper sniper;
    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update () {

        if (controller.transform.childCount < 3) {
            this.gameObject.transform.Find("hand").gameObject.SetActive(true);
        }

        if (canPickup) {
            var device = SteamVR_Controller.Input((int)controller.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip)) {
                this.gameObject.transform.Find("hand").gameObject.SetActive(false);
                //controller.transform.childCount;
                //controller.transform.DetachChildren;
                pickupObject.transform.parent = controller.transform;

                if (pickupObject.name == "Sniper") {
                    sniper = pickupObject.GetComponent<Sniper>();
                    sniper.isPickedUp = true;
                    sniper.controller = controller;
                }

                Debug.Log("The button worked for this controller index: " + pickupObject.name);
            }
        }

    }

    void OnTriggerEnter(Collider collider) {
        
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device != null) {
            pickupObject = collider.gameObject;
            canPickup = true;
        }
    }

    void OnTriggerExit() {
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device != null) {
            canPickup = false;
            pickupObject = null;
        }
    }



}
