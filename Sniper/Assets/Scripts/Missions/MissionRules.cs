using UnityEngine;
using System.Collections;

public class MissionRules : MonoBehaviour {

    public GameObject gameController;

    public int missionType;                             //1= Military Mission, 2= Regular Mission, 3= Special Mission
    public int phase1;
    public int phase2;
    public int phase3;

    bool failed = false;                                //Set this to true once mission failed so that it does not keep runing 

    public void checkRules(string identifier) {

        Debug.Log("What is being killed " + identifier);
        if (identifier.Contains("Main1")) {
            phase1--;
        } else if (identifier.Contains("Main2") && phase1 == 0) {
            phase2--;
        } else if (identifier.Contains("Main3") && phase1 == 0 && phase2 == 0) {
            phase3--;
            if (phase3 == 0) {
                gameController.GetComponent<GameController>().Success();
            }
        }else {
            if (!failed) {
                gameController.GetComponent<GameController>().Failure(missionType);
                failed = true;
            }
        }
    }
}
