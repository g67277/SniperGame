using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {


    [Header("Object Type")]
    public bool surveillanceCamera;
    public bool helicopter;
    public bool powerBox;
    public bool missileTurrent;

    [Header("Drag the Object in")]
    public GameObject selectedObject;
    public GameObject[] requiredChildren;
    public Light[] lightArray;
    public Light[] blinkingLights;

    [Header("Object Behavior")]
    public string id;
    public bool badObject = false;
    public bool isSlowBlink = false;
    public bool kill = false;

    //Required private variables
    GameObject gameController;
    private bool blinkOn = true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isSlowBlink) {
            slowBlink();
        }
    }

    public void killObject() {
        if (isSlowBlink) {
            isSlowBlink = false;
        }

        if (surveillanceCamera) {
            surveillanceCameraBehavior();
        }else if (helicopter) {
            helicopterBehavior();
        }else if (powerBox) {
            powerBoxBehavior();
        }else if (missileTurrent) {
            missileTurrentBehavior();
        }

        gameController.GetComponent<GameController>().checkObjectHit(gameObject, id, badObject);
    }

    void surveillanceCameraBehavior() {
        if (selectedObject != null) {
            selectedObject.transform.Rotate(-30f, 0f, 0f);
        }
    }

    void helicopterBehavior() {
        if (selectedObject != null) {

            Animator[] animators = selectedObject.GetComponentsInChildren<Animator>();
            if (animators != null) {
                foreach (Animator animator in animators) {
                    animator.enabled = false;
                }
            }
            requiredChildren[0].gameObject.SetActive(true);                //First child is always the smoke**
        }
    }

    void powerBoxBehavior() {

    }

    void missileTurrentBehavior() {
        if (selectedObject != null) {
            selectedObject.transform.Rotate(-30f, 0f, 0f);
        }
    }

    //Blinks any light in the blinking lights array
    public void slowBlink() {

        if (blinkOn) {
            foreach (Light objLight in blinkingLights) {
                objLight.intensity = Mathf.Lerp(objLight.intensity, 4f, Time.deltaTime * 2);
                if (objLight.intensity >= 2) {
                    blinkOn = false;
                }
            }
        } else {
            foreach (Light objLight in blinkingLights) {
                objLight.intensity = Mathf.Lerp(objLight.intensity, 0f, Time.deltaTime * 2);
                if (objLight.intensity < 0.1) {
                    blinkOn = true;
                }
            }
        }
    }
}
