﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject missionRules;                        //Retrives the mission rules
    public GameObject missionReport;                       //Calls the mission report script to display end of mission report
    public GameObject player;                              //Get the player to calculate distance from the target.

    //Mission Failure Objects
    public GameObject helicopter;
    public GameObject missile;
    public AudioSource siren;

    //Boat Specific missions
    public GameObject boat;

    public static bool finish = false;                     // Set by an outside class to invoke missionFinish              

    //Scoring
    double distanceMultiplier = 1;                   // Will be dynamic based on distance from player
    int score = 0;                                  // Holds score to be saved at the end of the game


    public void checkObjectHit(GameObject incomingObj, string id) {
        if (incomingObj.GetComponent<Light>() != null || incomingObj.GetComponentsInChildren<Light>() != null) {
            turnOffLight(incomingObj);
        }

        if (id.Contains("Main")) {
            missionRules.GetComponent<MissionRules>().checkRules(id);
        }
    }

    public void checkScore(string id, bool badGuy, Vector3 position, int points) {

        calculateDistanceMultiplier(Util.CalculateDistance(position, player.transform.position));        //Gets distance from the distance helper method
        if (badGuy) {
            points = points * (int)distanceMultiplier;
        }
        DataHolder.Score = DataHolder.Score + points;

        if (id.Contains("Main")) {
            missionRules.GetComponent<MissionRules>().checkRules(id);
        }
    }

    public void missionFailed() {
        GameObject.Find("boat").GetComponent<ParentMovement>().clip = false;
        GameObject.Find("MissionReport").GetComponent<MissionReport>().TransportToMissionReport();
    }

    void calculateDistanceMultiplier(float distance) {

        distance = distance / 100;
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


    void turnOffLight(GameObject light) {
        if (light.GetComponent<Light>() != null) {
            light.GetComponent<Light>().enabled = false;
        }else {
            Light[] lights = light.GetComponentsInChildren<Light>();
            if (lights != null) {
                foreach (Light childLight in lights) {
                    childLight.enabled = false;
                }
            }
        }
    }

    void Update() {
        if (finish) {
            Invoke("finishMission", 2.0f);
            finish = false;
        }
    }


    public void Success() {
        //mission complete code:
        DataHolder.checkMissionScore();
        if (boat != null) {
            boat.GetComponent<ParentMovement>().clip = false;
        }
        missionReport.GetComponent<MissionReport>().TransportToMissionReport();
    }

    public void Failure(int missionType) {
        switch (missionType) {
            case 1:
                type1Failure();                 //Military Mission
                break;
            case 2:
                type2Failure();                 //Regular Mission
                break;
            case 3:
                type3Failure();                 //Special Mission
                break;
        }
    }

    public void type1Failure() {
        if (siren != null) {
            siren.GetComponent<AudioSource>().Play();
        }
        if (helicopter != null) {
            helicopter.GetComponent<Helicopter>().heliAttack();
        }
    }

    public void type2Failure() {

    }

    public void type3Failure() {

    }

    public void finishMission() {
        if (boat != null) {
            boat.GetComponent<ParentMovement>().clip = false;
        }

        missionReport.GetComponent<MissionReport>().TransportToMissionReport();
    }

}