using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavButton : MonoBehaviour {

    public void startAction() {
        //Play click sound
        gameObject.GetComponent<AudioSource>().Play();
        transform.position = new Vector3(transform.position.x, transform.position.y - .02f, transform.position.z);

        if (gameObject.name == "Home") {
            DataHolder.missionIndex = 0;
            DataHolder.deleteData();
        }

        DataHolder.saveData();
        GameObject.Find("GameController").GetComponent<MissionManager>().missionSelection();
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("What is Hitting reset: " + col.name);
        if (col.transform.parent.name == "hand") {
            
            startAction();
        }
    }
}
