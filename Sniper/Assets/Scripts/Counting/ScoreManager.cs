using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private int totalScore;
    Text text;


    void Awake() {
        text = GetComponent<Text>();
        score = 0;
        totalScore = 0;
    }
	
	
	// Update is called once per frame
	void Update () {

        
        if (text.name == "SessionScore") {
            totalScore += score;
            score = 0;
            PlayerPrefs.SetInt("Score", totalScore);
            text.text = totalScore.ToString();
        }

        if (text.name == "AverageAccuracy") {
            float accuracy = PlayerPrefs.GetFloat("TotalAccuracy") * 100;
            string accuracyString = accuracy.ToString("0");
            text.text = "Average Accuracy: " + accuracyString + "%";
        }

        if (text.name == "HighScore") {
            if (PlayerPrefs.GetInt("HighScore") < PlayerPrefs.GetInt("Score")) {
                PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            }

            text.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
	}
}
