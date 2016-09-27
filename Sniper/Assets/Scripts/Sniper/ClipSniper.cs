using UnityEngine;
using System.Collections;

public class ClipSniper : MonoBehaviour {

    public GameObject sniper;
    Camera scopeCamera;
    public GameObject spawnPoint;

    GunScript gunScript;

	// Use this for initialization
	void Start () {
        gunScript = gameObject.transform.root.GetComponent<GunScript>();
	}

    void OnTriggerEnter(Collider col) {        
        if (gameObject.transform.parent.childCount < 3) {
            if (col.gameObject.tag == "Scope") {
                GameObject scope = col.gameObject;
                scope.transform.parent = sniper.transform;
                scope.transform.localPosition = new Vector3(0.02f, 3.62f, .4344f);
                scope.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

                scopeCamera = scope.transform.GetChild(0).GetComponent<Camera>();
                scopeCamera.transform.position = spawnPoint.transform.position;
                scopeCamera.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

                gunScript.isThereScope = true;
                gunScript.scopeCamera = scopeCamera;

                if (scope.name == "Scope x11") {
                    gunScript.newMinFOV = 1;
                } else if (scope.name == "Scope x7") {
                    gunScript.newMinFOV = 5;
                } else if (scope.name == "Scope x5") {
                    gunScript.newMinFOV = 16;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
