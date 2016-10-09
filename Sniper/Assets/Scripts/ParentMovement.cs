using UnityEngine;
using System.Collections;

public class ParentMovement : MonoBehaviour {

    public GameObject cameraRig;
    public GameObject pivotPoint;    //Point where we want the Object to rotate around
    public GameObject centerPoint;   //Center point of the rotating object
    public float speed;              //Speed of the orbiting object

    Quaternion rotation;
    Vector3 radius = new Vector3(5, 0, 0);
    float currentRotation = 0.0f;
    public bool clip = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(waitForEntry());
	}

    IEnumerator waitForEntry() {
        yield return new WaitForSeconds(1.2f);
        clip = true;
    }

    // Update is called once per frame
    void Update() {
        if (clip) {
            transform.RotateAround(pivotPoint.transform.position, Vector3.up, speed * Time.deltaTime);
            cameraRig.transform.position = centerPoint.transform.position;
            cameraRig.transform.rotation = centerPoint.transform.rotation;
        }
    }

    public void boatMovement() {
        
    }
}
