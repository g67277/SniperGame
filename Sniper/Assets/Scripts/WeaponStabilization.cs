using UnityEngine;
using System.Collections;
using System;

public class WeaponStabilization : MonoBehaviour {

    GameObject sniper;
    public GameObject controller2;

    public bool isEnabled = false;
    public bool isStabalizing = false;
    public bool test2 = false;
    bool gotSniper = false;

    int testing = 0;
    bool sniperCliped = false;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () {
        if (transform.childCount > 2 && !gotSniper) {
            sniper = transform.GetChild(2).gameObject;
            gotSniper = true;
        }

        if (sniper != null) {
            //sniper.transform.position = new Vector3(184.73f, 16.32f, 116.45f);
        }

        if (isEnabled) {
            //if (!sniperCliped) {
            //    sniper.transform.position = transform.position;
            //    sniper.transform.rotation = transform.rotation;
            //    sniperCliped = true;
            //}
            //Debug.Log("x: " + transform.rotation.x + " y: " + transform.rotation.y + " z: " + transform.rotation.z);

            //sniper.transform.position = transform.position + new Vector3(-0.02f, -0.02f, -0.02f);
            //sniper.transform.position = Vector3.SmoothDamp(sniper.transform.position, transform.position, ref velocity, 1f);
            //if (isStabalizing) {
            //    testingMotion();
            //} else if (test2) {
            //    sniper.transform.LookAt(controller2.transform);
            //}else {
            //    //sniper.transform.position = transform.position;
            //    //sniper.transform.rotation = transform.rotation;
            //}
        }
    }

    void testingMotion() {
        float x = (float)Math.Round(transform.position.x, 1);
        float y = (float)Math.Round(transform.position.y, 1);
        float z = (float)Math.Round(transform.position.z, 1);

        float Rx = (float)Math.Round(transform.rotation.x, 1);
        float Ry = (float)Math.Round(transform.rotation.y, 1);
        float Rz = (float)Math.Round(transform.rotation.z, 1);

        Debug.Log("Controller y: " + transform.rotation + " sniper y: " + Ry);
        //sniper.transform.position = new Vector3(x, y, z);
        sniper.transform.position = Vector3.SmoothDamp(new Vector3(x, y, z), transform.position, ref velocity, 0.1f);
        //sniper.transform.rotation = Quaternion.Euler(new Vector3(Rx, Ry, Rz));
        //sniper.transform.rotation = transform.rotation;
    }
}
