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
            DataHolder.rangeScore = totalScore;
            text.text = totalScore.ToString();
        }

        if (text.name == "AverageAccuracy") {
            double accuracy = DataHolder.totalAccuracy * 100;
            if (!double.IsNaN(accuracy)) {
                string accuracyString = accuracy.ToString("0");
                text.text = "Average Accuracy: " + accuracyString + "%";
            }
        }

        if (text.name == "HighScore") {
            text.text = "High Score: " + DataHolder.rangeHighScore;
        }
	}
}
