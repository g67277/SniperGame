using UnityEngine;
using System.Collections;
using System;

//Note**: This class uses the DataHolder

public class Person : MonoBehaviour {

    public string id;                   //identifies which person this is for the mission rules
    public bool badGuy;                 //If he's a civilian or not
    GameObject gameController;          //Game controller

    Vector3 position;                   // Save the position to calculate the distance
    int score;                          // Save the score based on the part hit
    Animator animator;                  // Animator attached to the person
    bool moving = false;                // Checks if the Person has the Pedestrian Object attached

    //*****DEBUGING ONLY******************
    public bool kill = false;
    void Update() {
        if (kill) {
            personKilled(50);
            kill = false;
        }
    }
    //****REMOVE BEFORE RELEASE**********

    void Start() {
        gameController = GameObject.Find("GameController");
        position = transform.position;
        animator = GetComponent<Animator>();
        if (GetComponent<PedestrianObject>() != null) {
            moving = true;
        }
    }

    public void checkHit(GameObject incomingObj) {

        if (incomingObj.name == "Head_jnt") {
            personKilled(100);
        } else if (incomingObj.name == "Spine_jnt") {
            personKilled(50);
        } else if (incomingObj.tag == "MiniTarget") {
            animator.Play("death", -1, 0f);
            DataHolder.missionIndex = Convert.ToInt32(id);
            gameController.GetComponent<MissionManager>().missionSelection();
        } else if (gameObject.GetComponent<Animator>() != null) {
            animator.Play("state2", -1, 0f);
        }

        DataHolder.totalHits = DataHolder.totalHits + 1;                //Saves all the hits through out the game
        DataHolder.sessionHits = DataHolder.sessionHits + 1;            //Saves all the hits from one level
    }

    void personKilled(int hitScore) {
        score = hitScore;

        animator.Stop();
        if (moving) {
            gameObject.GetComponent<PedestrianObject>().enabled = false;
        }
        if (!badGuy) {
            DataHolder.civiliansKilled = DataHolder.civiliansKilled + 1;
            score = -100;
        }
        gameController.GetComponent<GameController>().checkScore(id, badGuy, gameObject, score);
        Invoke("StopBody", 6.0f);
        gameObject.tag = "killed";
    }

    // Removes colliders and rigidbodys to save CPU _____________________________________________________
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;

    public void OnAIDeath() {


        //wait 2-3 seconds.
        foreach (Collider col in rigColliders) {
            col.enabled = false;
        }
    }

    void StopBody() {

        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigRigidbodies) {
            rb.isKinematic = true;

        }
        OnAIDeath();
    }


}
