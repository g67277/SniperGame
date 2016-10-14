using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

    void Start() {
        Invoke("enableReset", 3.0f);
    }

    void enableReset() {
        GetComponent<BoxCollider>().enabled = true;
    }

	public void resetScene() {
        //Play click sound
        gameObject.GetComponent<AudioSource>().Play();
        transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);
        GameObject cameraRig = GameObject.Find("[CameraRig]");
        float[] playerPosition = new float[3];
        playerPosition[0] = cameraRig.transform.position.x;
        playerPosition[1] = cameraRig.transform.position.y;
        playerPosition[2] = cameraRig.transform.position.z;
        DataHolder.resetCoordinates = playerPosition;
        DataHolder.isReset = true;
        GameObject.Find("GameController").GetComponent<MissionManager>().resetMission();
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("What is Hitting reset: " + col.name);
        if (col.transform.parent.name == "hand") {
            resetScene();
        }
    }
}
