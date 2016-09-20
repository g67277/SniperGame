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
        totalScore += score;
        score = 0;
        text.text = totalScore.ToString();

	}
}
