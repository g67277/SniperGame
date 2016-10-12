using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

    public string id;
    public bool badObject = false;
    public bool isSlowBlink = false;

    //Items attached to the light that will be main or secondary targets
    public GameObject missileTurrent;

    GameObject gameController;
    Light objLight;

    private bool blinkOn = true;

    //*****DEBUGING ONLY******************
    public bool kill = false;
    //****REMOVE BEFORE RELEASE**********

    void Start() {
        gameController = GameObject.Find("GameController");
        if (GetComponent<Light>() != null) {
            objLight = GetComponent<Light>();
        }
    }

    public void killLight() {
        if (isSlowBlink) {
            isSlowBlink = false;
        }
        gameController.GetComponent<GameController>().checkObjectHit(gameObject, id, badObject);

        if (badObject) {
            if (missileTurrent != null) {
                missileTurrent.transform.Rotate(-30f, 0f, 0f);
            }
        }
    }

    void Update() {
        //*****DEBUGING ONLY******************
        if (kill) {
            killLight();
            kill = false;
        }
        //****REMOVE BEFORE RELEASE**********

        if (isSlowBlink) {
            slowBlink();
        }
    }
    public void slowBlink() {

        if (blinkOn) {
            objLight.intensity = Mathf.Lerp(objLight.intensity, 4f, Time.deltaTime * 2);
            if (objLight.intensity >= 2) {
                blinkOn = false;
            }
        } else {
            objLight.intensity = Mathf.Lerp(objLight.intensity, 0f, Time.deltaTime * 2);
            if (objLight.intensity < 0.1) {
                blinkOn = true;
            }
        }
    }
	
}
