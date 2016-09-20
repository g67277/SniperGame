using UnityEngine;
using System.Collections;

public class DummyTarget : MonoBehaviour {


    public int distanceMultiplier = 1;
    private int score = 0;

	// Use this for initialization
	void Start () {
	
	}

    public void calculateHit(float distance, string part) {

        if (distance >= 0.0 && distance < 0.1) {
            score = 200;
        } else if (distance >= 0.1 && distance <= 0.14) {
            score = 50;
        } else if (distance >= 0.15 && distance <= 0.22) {
            score = 25;
        } else if (distance >= 0.23 && distance <= 0.31) {
            score = 10;
        }

        if (part == "Body") {
            if (distance >= 0.32 && distance <= 0.47) {
                score = 5;
            }
        }

        ScoreManager.score = score * distanceMultiplier;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

}
