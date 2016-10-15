using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionReport : MonoBehaviour {

    public GameObject cameraRig;
    public Vector3 rigPosition;

    [Header("Scoring Text")]
    public Text totalScore;
    public Text LevelScore;
    public Text BulletsFired;
    public Text Accuracy;
    public Text AccuracyScore;
    public Text cashEarned;
    public Text TotalCash;
    public Text SecondaryScore;
    public Text civiliansKilled;
    public Text civilianScore;
    public Text missionStatus;
    public Text longestHit;
    public Text highScore;

    // Use this for initialization
    void Start () {

    }

    public void TransportToMissionReport() {
        cameraRig.transform.position = rigPosition;
        updateScores();
    }

    void updateScores() {

        if (DataHolder.missionSuccess) {
            missionStatus.text = "Mission Successful";
        } else {
            missionStatus.text = "Mission Failed";
        }
        LevelScore.text = DataHolder.Score.ToString();
        BulletsFired.text = BulletsFired.text + " " + DataHolder.sessionBullets;
        Accuracy.text = Accuracy.text + " " + DataHolder.sessionAccuracy[DataHolder.missionIndex] + "%";
        AccuracyScore.text = AccuracyScore.text + " + " + (int)DataHolder.sessionAccuracy[DataHolder.missionIndex];
        SecondaryScore.text = DataHolder.secondaryScore.ToString();
        civiliansKilled.text = civiliansKilled.text + " " + DataHolder.civiliansKilled;
        civilianScore.text = "-" + (DataHolder.civiliansKilled * 100);
        longestHit.text = DataHolder.longestHit + " Meters";
        totalScore.text = totalScore.text + " " + DataHolder.finalScore;
        highScore.text = DataHolder.missionScore[DataHolder.missionIndex].ToString();
        cashEarned.text = cashEarned.text + " " + (int)(DataHolder.finalScore * 0.3);
        TotalCash.text = TotalCash.text + " " + DataHolder.cash;

        //Clears temporary data
        clearTempData();
    }

    public void clearTempData() {
        DataHolder.sessionBullets = 0;
        DataHolder.sessionHits = 0;
    }
}
