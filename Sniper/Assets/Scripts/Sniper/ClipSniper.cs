using UnityEngine;
using System.Collections;

public class ClipSniper : MonoBehaviour {

    public GameObject sniper;
    Camera scopeCamera;
    public GameObject spawnPoint;

    GunScript gunScript;

	// Use this for initialization
	void Start () {
        //gunScript = sniper.GetComponent<GunScript>();
        //Debug.Log("Is this assigned correctly, name: " + gunScript.name);
    }

    void OnTriggerEnter(Collider col) {
        clip(col);
    }

    public void clip(Collider col) {
        if (gameObject.transform.parent.childCount < 3) {
            
            if (col.gameObject.tag == "Scope") {
                GameObject scope = col.gameObject;
                scope.transform.parent = sniper.transform;
                scope.transform.localPosition = new Vector3(0.02f, 3.62f, .4344f);
                scope.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

                scopeCamera = scope.transform.GetChild(0).GetComponent<Camera>();
                scopeCamera.transform.position = spawnPoint.transform.position;
                scopeCamera.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

                sniper.GetComponent<GunScript>().isThereScope = true;
                sniper.GetComponent<GunScript>().scopeCamera = scopeCamera;

                if (scope.name == "Scope x11") {
                    sniper.GetComponent<GunScript>().newMinFOV = 1;
                } else if (scope.name == "Scope x7") {
                    sniper.GetComponent<GunScript>().newMinFOV = 5;
                } else if (scope.name == "Scope x5") {
                    sniper.GetComponent<GunScript>().newMinFOV = 16;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
