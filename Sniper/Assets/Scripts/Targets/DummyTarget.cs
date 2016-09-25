using UnityEngine;
using System.Collections;

public class DummyTarget : MonoBehaviour {


    public int distanceMultiplier = 1;
    public GameObject scoreDisplay;
    private int score = 0;
	// Use this for initialization
	void Start () {
	
	}

    public void calculateHit(float distance, string part) {

        if (distance >= 0.0 && distance < 0.1) {
            displayHitScore(200);
        } else if (distance >= 0.1 && distance <= 0.15) {
            displayHitScore(50);
        } else if (distance >= 0.15 && distance <= 0.23) {
            displayHitScore(15);
        } else if (distance >= 0.23 && distance <= 0.32) {
            displayHitScore(10);
        }

        if (part == "Body") {
            if (distance >= 0.32 && distance <= 0.47) {
                displayHitScore(5);
            }
        }

        ScoreManager.score = score * distanceMultiplier;
    }
	
    void displayHitScore(int hitScore) {
        score = hitScore;

        GameObject scoreDisplay2 = Instantiate(scoreDisplay, new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y + 3.3f, gameObject.transform.position.z - 0.8f), gameObject.transform.rotation) as GameObject;
        scoreDisplay2.GetComponent<TextMesh>().text = "+" + hitScore.ToString();
        saveHit();
    }

    void saveHit() {
        //Save bullet count
        int hits = PlayerPrefs.GetInt("HitsNum") + 1;
        PlayerPrefs.SetInt("HitsNum", hits);
    }
   

    // Update is called once per frame
    void Update () {
        
    }

}
