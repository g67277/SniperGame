using UnityEngine;
using System.Collections;

public class Movements : MonoBehaviour {

    Vector2 touchPadPos;
    public SteamVR_TrackedObject controller;
    public GameObject cameraRig;
    //public Camera camera;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        var device = SteamVR_Controller.Input((int)controller.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
            touchPadPos = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            //touchPadPos = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

           if (touchPadPos.y > 0) {
               // Debug.Log("vector is: " + camera.transform.forward);

                //cameraRig.transform.position = camera.transform.forward / 500;
            }

        }
    }
}
