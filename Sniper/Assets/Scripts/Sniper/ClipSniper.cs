using UnityEngine;
using System.Collections;

public class ClipSniper : MonoBehaviour {

    public GameObject sniper;
    public Camera scopeCamera;
    public GameObject spawnPoint;

    GunScript gunScript;

	// Use this for initialization
	void Start () {
	    
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Scope") {
            GameObject scope = col.gameObject;
            scope.transform.parent = sniper.transform;
            scope.transform.localPosition = new Vector3(0.02f, 3.62f, .4344f);
            scope.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

            scopeCamera.transform.position = spawnPoint.transform.position;
            scopeCamera.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);

            gameObject.transform.parent.GetComponent<GunScript>().isThereScope = true;
            gameObject.transform.parent.GetComponent<GunScript>().scopeCamera = scopeCamera;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
