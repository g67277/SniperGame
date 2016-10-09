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

    // Use this for initialization
    void Start () {

    }

    public void TransportToMissionReport() {
        cameraRig.transform.position = rigPosition;
        totalScore.text = totalScore.text + " Hello";
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
