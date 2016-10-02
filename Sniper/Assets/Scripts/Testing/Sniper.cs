using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour {

    public Transform BulletSpawnPoint;
   //ublic Transform BulletParent;
    public GameObject BulletPrefab;
    public Camera scopeCamera;

    public float newMinFOV = 1f;
    public float newMaxFOV = 66f;
    public float bulletSpeedMultiplier = 1;
    public SteamVR_TrackedObject controller;

    public bool isPickedUp = false;

    //Audio Vairables
    SniperSounds sounds;

    // Update is called once per frame
    void Update () {

        if (isPickedUp) {
            //Debug.Log("New Sniper!");

            var device = SteamVR_Controller.Input((int)controller.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {

                sounds =  GetComponent<SniperSounds>();
                sounds.PlayShot();

                device.TriggerHapticPulse(2000);

                GameObject go = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.transform.rotation) as GameObject;
                //Add velocity to the non-physics bullet
                go.GetComponent<SniperBullet>().currentVelocity = (Ballistics.bulletSpeed * bulletSpeedMultiplier) * BulletSpawnPoint.transform.forward;

            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {

                float touchY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
                Debug.Log("What is touchY: " + touchY);
                if (touchY > 0.5) {
                    float fov;
                    if (scopeCamera.fieldOfView < 7) {
                        fov = scopeCamera.fieldOfView - 1f;
                    } else {
                        fov = scopeCamera.fieldOfView - 10f;
                    }
                    if (fov >= newMinFOV) {
                        scopeCamera.fieldOfView = fov;
                    }
                }else if (touchY < -0.5) {
                    float fov;
                    if (scopeCamera.fieldOfView < 6) {
                        fov = scopeCamera.fieldOfView + 1f;
                    } else {
                        fov = scopeCamera.fieldOfView + 10f;
                    }

                    if (fov <= newMaxFOV) {
                        scopeCamera.fieldOfView = fov;
                    }

                }
            }
        }
    }

}
