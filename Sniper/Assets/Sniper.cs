using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour {

    public Transform BulletSpawnPoint;
    public Transform BulletParent;
    public GameObject BulletPrefab;
    public Camera scopeCamera;

    public const float minFOV = 5f;
    public const float maxFOV = 200f;
    public SteamVR_TrackedObject rightController;


	// Update is called once per frame
	void Update () {

        var device = SteamVR_Controller.Input((int)rightController.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            device.TriggerHapticPulse(2000);
            

            GameObject go = Instantiate(BulletPrefab, BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation) as GameObject;

            //Parent it to get a less messy workspace
            BulletPrefab.transform.parent = BulletParent;

            //Add velocity to the non-physics bullet
            BulletPrefab.GetComponent<TutorialBullet>().currentVelocity = TutorialBallistics.bulletSpeed * -transform.forward;

            //GameObject go = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.transform.rotation) as GameObject;

            //go.GetComponent<Rigidbody>().AddForce(BulletSpawnPoint.transform.forward * 10000f);
            //go.GetComponent<Rigidbody>().velocity = 250f * BulletSpawnPoint.transform.forward;
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {

            float touchY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            float fov = scopeCamera.fieldOfView - touchY;
            if (fov < minFOV){
                scopeCamera.fieldOfView = minFOV;
            }else if (fov > maxFOV) {
                scopeCamera.fieldOfView = maxFOV;
            } else {
                scopeCamera.fieldOfView = fov;
            }

        }

    }

}
