using UnityEngine;
using System.Collections;

public class WindDisplay : MonoBehaviour {

    public GameObject hairBase;
    public int windDirection;
    public int windSpeed;

    bool moveHair = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateHair();
    }

    void updateHair() {
        if (moveHair) {
            if (windDirection == 1) {
                //Quaternion rightWind = new Quaternion(-0.7f, 0.0f, 0.0f, -0.7f);
                //Vector3 leftWind = new Vector3(197.0f, 2.7f, -0.1f);
                //float test1 = leftWind.x;
                //float test2 = leftWind.z;
                float test3 = transform.position.x;
                float test4 = transform.position.z;

                if (transform.rotation.x <= 0) {
                    moveHair = false;
                } else {
                    transform.RotateAround(hairBase.transform.position, Vector3.up, 20 * Time.deltaTime);
                    //Debug.Log("Hair roation: " + transform.rotation.x);
                }
            } else if (windDirection == 2) {
                //Quaternion leftWind = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
                Vector3 rightWind = new Vector3(197.0f, 2.7f, 0.4f);
                transform.RotateAround(hairBase.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
        }

    }
}
