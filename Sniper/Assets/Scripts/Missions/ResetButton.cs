using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

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
        GameObject.Find("Camera (eye)").GetComponent<MissionManager>().resetMission();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
