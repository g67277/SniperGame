using UnityEngine;
using System.Collections;
using System;

//Note**: This class uses the DataHolder

public class Person : MonoBehaviour {

    public string id;                   //identifies which person this is for the mission rules
    public bool badGuy;                 //If he's a civilian or not
    GameObject gameController;          //Game controller
    GameObject playerHead;               //Player's position

    [Header("Give a bad guy two locations to run to")]
    public bool moveBetweenPoints = false;     //
    public Vector3 bgPosition1;         //Current location and return location
    public Vector3 bgPosition2;         //Position going to and from
    public float timeBetweenPositions;  //how long will the character take to cross the distance
    bool bgP1 = true;                   //Check if the character is in position1

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
        playerHead = GameObject.Find("Camera (eye)");
        position = transform.position;
        animator = GetComponent<Animator>();
        if (GetComponent<PedestrianObject>() != null) {
            moving = true;
        }
    }

    public void checkHit(GameObject incomingObj) {

        if (incomingObj.name == "Head_jnt") {
            personKilled(100);
        } else if (incomingObj.tag == "MiniTarget") {
            animator.Play("death", -1, 0f);
            DataHolder.missionIndex = Convert.ToInt32(id);
            gameController.GetComponent<MissionManager>().missionSelection();
        } else if (gameObject.GetComponent<Animator>() != null) {
            if (moveBetweenPoints) {
                StartCoroutine(badGuySwitchPosition());
            } else {
                transform.LookAt(playerHead.transform);
                //animator.Play("state2", -1, 0f);
                GetComponent<BadBuyAttack>().startAttacking(true, playerHead);
            }
        }

        DataHolder.totalHits = DataHolder.totalHits + 1;                //Saves all the hits through out the game
        DataHolder.sessionHits = DataHolder.sessionHits + 1;            //Saves all the hits from one level
    }

    void personKilled(int hitScore) {
        if (GetComponent<BadBuyAttack>() != null) {
            GetComponent<BadBuyAttack>().startAttacking(false);
        }
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

    IEnumerator badGuySwitchPosition() {
        Debug.Log("did this get hit?");
        GetComponent<BadBuyAttack>().startAttacking(false);
        float startTime = Time.time;
        animator.Play("state2", -1, 0f);
        
        if (bgP1) {
            transform.LookAt(bgPosition2);
            while (Time.time < startTime + timeBetweenPositions) {
                transform.position = Vector3.Lerp(bgPosition1, bgPosition2, (Time.time - startTime) / timeBetweenPositions);
                yield return null;
            }
            transform.position = bgPosition2;
            bgP1 = false;
        } else {
            transform.LookAt(bgPosition1);
            while (Time.time < startTime + timeBetweenPositions) {
                transform.position = Vector3.Lerp(bgPosition2, bgPosition1, (Time.time - startTime) / timeBetweenPositions);
                yield return null;
            }
            transform.position = bgPosition1;
            bgP1 = true;
        }
        animator.Play("state3", -1, 0f);
        transform.LookAt(playerHead.transform);
        GetComponent<BadBuyAttack>().startAttacking(true, playerHead);
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
