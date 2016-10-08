using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonAnimator : MonoBehaviour {

    GameObject missionRules;                        //Retrives the mission rules

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
    float distanceMultiplier = 1;                   // Will be dynamic based on distance from player
    int score = 0;                                  // Holds score to be saved at the end of the game

    void Start() {
        missionRules = GameObject.Find("MissionRules");   //Finds the object holding the 
        animator = GetComponent<Animator>();              // Assignes the animator
        if (GetComponent<PedestrianObject>() != null) {
            moving = true;
        }else {
            moving = false;
        }
    }

    public void checkHit(GameObject incomingObj) {
        rootName = incomingObj.transform.root.name;                     //Use for scoring
        
        if (incomingObj.name == "Head_jnt") {
            killed(incomingObj.tag, 50);
        }else if (incomingObj.name == "Spine_jnt") {
            killed(incomingObj.tag, 25);
        } else if (incomingObj.tag == "MiniTarget") {
            animator.Play("death", -1, 0f);                             //Might need to change**
        } else {
            animator.Play("state2", -1, 0f);
        }

        if (rootName.Contains("Main")){
            if (!missionRules.GetComponent<MissionRules>().checkRules(rootName)) {
                missionFail = true;
                //Testing will change...
                GameObject.Find("Heli").GetComponent<Helicopter>().heliAttack();
                // Mission Failure Script
            }
        }
    }



    public void killed(string objTag, int points) {
        animator.Stop();
        if (moving) {
            gameObject.GetComponent<PedestrianObject>().enabled = false;
        }
        if (objTag == "Civilian") {
            score = score - 50;
        }else {
            score = score + points;
        }

        Debug.Log("Your Points are: " + score);
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



    //Testing
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;

    public void OnAIDeath() {

        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        //wait 2-3 seconds.
        foreach (Collider col in rigColliders) {
            col.enabled = false;
        }

        Invoke("StopBody", 2.0f);
    }

    void StopBody() {
        foreach (Rigidbody rb in rigRigidbodies) {
            rb.isKinematic = true;
        }
    }

}
