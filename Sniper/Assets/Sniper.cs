using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour {

    public Transform BulletSpawnPoint;
   //ublic Transform BulletParent;
    public GameObject BulletPrefab;
    public Camera scopeCamera;

    public const float minFOV = 5f;
    public const float maxFOV = 200f;
    public SteamVR_TrackedObject controller;

    public bool isPickedUp = false;

    //Audio Vairables
    SniperSounds sounds;

    // Update is called once per frame
    void Update () {

        if (isPickedUp) {
            var device = SteamVR_Controller.Input((int)controller.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {

                sounds =  GetComponent<SniperSounds>();
                sounds.PlayShot();

                device.TriggerHapticPulse(2000);

                GameObject go = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.transform.rotation) as GameObject;
                //Add velocity to the non-physics bullet
                go.GetComponent<TutorialBullet>().currentVelocity = TutorialBallistics.bulletSpeed * BulletSpawnPoint.transform.forward;

            }

            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {

                float touchY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
                float fov = scopeCamera.fieldOfView - touchY;
                if (fov < minFOV) {
                    scopeCamera.fieldOfView = minFOV;
                } else if (fov > maxFOV) {
                    scopeCamera.fieldOfView = maxFOV;
                } else {
                    scopeCamera.fieldOfView = fov;
                }

            }
        }
    }

}
