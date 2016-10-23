using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissionStart : MonoBehaviour {

    public GameObject gameController;
    public GameObject cameraRig;
    public BadGuyAttack[] SniperBGA;

    Vector3 missionStartPosition;

    void Start() {
        missionStartPosition = gameController.GetComponent<MissionManager>().gameplayPosition;
    }

    public void startMission() {
        cameraRig.transform.position = missionStartPosition;
        if (gameController.GetComponent<GameController>().boat != null) {
            gameController.GetComponent<GameController>().boat.GetComponent<ParentMovement>().clip = true;
        }
        gameController.GetComponent<AnimateScene>().animateScene();
        foreach (BadGuyAttack bga in SniperBGA) {
            bga.sniperAttack();
        }
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("What is Hitting reset: " + col.name);
        if (col.transform.parent.name == "hand") {
            startMission();
        }
    }
}
