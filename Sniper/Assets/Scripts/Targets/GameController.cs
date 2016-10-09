using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    GameObject missionRules;                        //Retrives the mission rules
    GameObject player;                              //Get the player to calculate distance from the target.
    Animator animator;

    //Constant names
    const string main1 = "Main1";                   //Define the target names
    const string main2 = "Main2";
    const string main3 = "Main3";
    const string secondary = "Secondary";

    string rootName;                                // Saves the root name to check score
    bool moving;                                    // To check if a system has the pedestrian object on it
    bool missionFail = false;                              

    //Scoring
    double distanceMultiplier = 1;                   // Will be dynamic based on distance from player
    int score = 0;                                  // Holds score to be saved at the end of the game

    void Start() {
        missionRules = GameObject.Find("MissionRules");     //Finds the object holding the 
        player = GameObject.Find("[CameraRig]");
        if (GetComponent<Animator>() != null) {
            animator = GetComponent<Animator>();            // Assignes the animator
        }       
        if (GetComponent<PedestrianObject>() != null) {
            moving = true;
        }else {
            moving = false;
        }
    }

    public void checkHit(GameObject incomingObj) {
        rootName = incomingObj.transform.root.name;                     //Use for scoring

        Vector3 offset = transform.position - player.transform.position; //Checking distance for multiplier
        float sqrlen = offset.sqrMagnitude;
        calculateDistanceMultiplier(sqrlen);

        if (incomingObj.name == "Head_jnt") {
            killed(incomingObj.tag, 50);
        }else if (incomingObj.name == "Spine_jnt") {
            killed(incomingObj.tag, 25);
        } else if (incomingObj.tag == "MiniTarget") {
            animator.Play("death", -1, 0f);                             //Might need to change**
        } else if (gameObject.GetComponent<Animator>() != null) {
            animator.Play("state2", -1, 0f);
        }


        // Remove light if the Game rules has light and we are in that mission
        if (DataHolder.missionIndex == 1) {
            if (rootName.Contains("Main2")) {
                turnOffLight(incomingObj);
                checkMissionRules();
            }
        }

        DataHolder.totalHits = DataHolder.totalHits + 1;
        DataHolder.sessionHits = DataHolder.sessionHits + 1;
    }

    public void checkMissionRules() {
        if (rootName.Contains("Main")) {
            if (!missionRules.GetComponent<MissionRules>().checkRules(rootName)) {
                missionFail = true;
                //Testing will change...
                GameObject.Find("Heli").GetComponent<Helicopter>().heliAttack();
                // Mission Failure Script
            }
        }
    }

    public void killed(string objTag, int points) {

        

        Debug.Log("Points for hit: " + points + " Multiplier: " + distanceMultiplier);
        points = points * (int)distanceMultiplier;
        animator.Stop();
        if (moving) {
            gameObject.GetComponent<PedestrianObject>().enabled = false;
        }
        if (objTag == "Civilian") {
            DataHolder.civiliansKilled = DataHolder.civiliansKilled + 1;
            DataHolder.Score = DataHolder.Score - 50;
        }else {
            DataHolder.Score = DataHolder.Score + points;
        }
        Debug.Log("Your Points are: " + DataHolder.Score);
        Invoke("StopBody", 3.0f);

        checkMissionRules();
    }

    void calculateDistanceMultiplier(float distance) {

        distance = distance / 100;
        Debug.Log("Distance check: " + distance);
        if (distance > 50 && distance < 101){
            distanceMultiplier = 2;
        }else if (distance > 100 && distance < 201) {
            distanceMultiplier = 3;
        } else if (distance > 200 && distance < 401) {
            distanceMultiplier = 4;
        } else if (distance > 400 && distance < 601) {
            distanceMultiplier = 5;
        }
    }


    











  

    //public void hitResult(string name, string tag) {
    //    animator = GetComponent<Animator>();
    //    if (name == "Head_jnt" || name == "Spine_jnt") {
    //        animator.Stop();
            
    //        gameObject.GetComponent<PedestrianObject>().enabled = false;
    //        //OnAIDeath();
    //    } else if(tag == "MiniTarget"){
    //        animator.Play("Death_02", -1, 0f);
    //    } else {
    //        animator.Play("Run", -1, 0f);
    //    }

    //}



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
        Debug.Log("stop body is hit");
    }


    void turnOffLight(GameObject light) {
        Light[] lights = light.GetComponentsInChildren<Light>();
        foreach (Light childLight in lights) {
            childLight.enabled = false;
        }
    }

}
