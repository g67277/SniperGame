using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

	public void resetScene() {
        //Play click sound
        gameObject.GetComponent<AudioSource>().Play();
        transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
