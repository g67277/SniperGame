using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

    public string id;
    GameObject gameController;

    //*****DEBUGING ONLY******************
    public bool kill = false;
    void Update() {
        if (kill) {
            killLight();
            kill = false;
        }
    }
    //****REMOVE BEFORE RELEASE**********

    void Start() {
        gameController = GameObject.Find("GameController");
    }

    public void killLight() {
        gameController.GetComponent<GameController>().checkObjectHit(gameObject, id);
    }
	
}
