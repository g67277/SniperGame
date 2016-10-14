using UnityEngine;
using System.Collections;

public class DataHolder : MonoBehaviour {

    public static double totalAccuracy;                 //Accuracy for the whole game and range
    public static int sessionBullets;                  //Total bullets fired during each mission
    public static int sessionHits;                     //Total hits during each mission
    public static int Score;                      //Final score for the range, overwrites the highscore when it surpasses it
    public static bool inMission;                      //Check if player is in a mission, to start saving the mission score
    public static int missionIndex;                    //Tells us which mission the player is playing
    public static int civiliansKilled;                 //Tells us how many civilians were killed in the mission

    //Items to be saved permenantly
    public static double[] sessionAccuracy;            //Accuracy for the current session, not saved for the range, but saved for the levels
    public static double longestHit;                   //Longest hit in meters 
    public static int totalBullets;                    //Total bullets fired since the start of the game (used to calculate accuracy)
    public static int totalHits;                       //Total hits on targets, rage or mission, since the start of the game (used to calculate accuracy)
    public static int[] missionScore;                  //Final score for each mission
    public static int[] missionStars;                  //Holds the star level for each mission
    public static int rangeHighScore;                  //Saves the high score of the range
    public static int cash;                            //Total cash earned
    public static int weapons;                         //Weapons unlocked, 1 = simple sniper, 2 second sniper, 3 best sniper
    public static string[] scops;                      //Adds scops that are unlocked, checked agains a static array to see which ones have been unlocked at the start of the game
    public static int mainMission;                     //The current mission they are at
    public static int subMission;                      //The current sub mission they are at

    //Items to be saved between scenes only
    public static string missionWeapon;                //Weapon selected for the mission
    public static string missionScope;                 //Scope selected for the mission
    public static float[] resetCoordinates;              //Coordinates to reset position when we want to reset the range or the mission
    public static bool isReset = false;                // Check if we're trying to reset the mission
    

	// Use this for initialization
	void Start () {

        loadData();
        if (missionScore == null) {                     // Initializing the mission Score array
            missionScore = new int[16];
        }

        if (sessionAccuracy == null) {
            sessionAccuracy = new double[16];
        }

    }

    // Update is called once per frame
    void Update() {
        //Calculate everything

        totalAccuracy = (double)totalHits / (double)totalBullets;
        if (!inMission) {
            if (Score > rangeHighScore) {
                rangeHighScore = Score;
            }
        }
    }

    public static void checkMissionScore() {

        sessionAccuracy[missionIndex] = ((double)sessionHits / (double)sessionBullets) * 100;
        Debug.Log("Session Accuracy: " + sessionAccuracy[missionIndex]);
        if (sessionAccuracy[missionIndex] > 79) {
            Score = Score + (int)Mathf.Ceil((float)sessionAccuracy[missionIndex]);
        }
        Debug.Log("Finishing Score: " + Score);
        if (Score > missionScore[missionIndex]) {
            missionScore[missionIndex] = Score;
        }
    }

    public static void deleteData() {
        missionWeapon = "";
        missionScope = "";

    }

    public void loadData() {
        GameData data = SaveLoadData.LoadData();
        if (data != null) {
            totalBullets = data.totalBullets;
            totalHits = data.totalHits;
            rangeHighScore = data.rangeHighscore;

            //Temporarly loaded after mission start
            missionWeapon = data.missionWeapon;
            missionScope = data.missionScope;
        }
    }

    public static void saveData() {
        SaveLoadData.SaveData();
    }

    void OnDestroy() {
        SaveLoadData.SaveData();
    }
}
