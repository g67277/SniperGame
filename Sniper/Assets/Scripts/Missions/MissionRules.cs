using UnityEngine;
using System.Collections;

public class MissionRules : MonoBehaviour {

    public int missionType;                             //1= Military Mission, 2= Regular Mission, 3= Special Mission, 4= Hostage
    public int phase1;
    public int phase2;
    public int phase3;
    public int phase4;
    public int phase5;

    //What phase should the level check for success
    [Header("What phase should the level check for success")]
    public bool phase3Check = false;
    public bool phase4Check = false;
    public bool phase5Check = false;

    public int secondary;

    bool failed = false;                                //Set this to true once mission failed so that it does not keep runing 

    public void checkRules(string identifier) {

        Debug.Log("What is being killed " + identifier);
        if (identifier.Contains("Main1")) {
            phase1--;
        } else if (identifier.Contains("Main2") && phase1 == 0) {
            phase2--;
        } else if (identifier.Contains("Main3") && phase1 == 0 && phase2 == 0) {
            phase3--;
            if (phase3Check) {
                if (phase3 == 0) {
                    GetComponent<GameController>().Success();
                }
            }
        }else if (identifier.Contains("Main4") && phase1 == 0 && phase2 == 0 && phase3 == 0) {
            phase4--;
            if (phase4Check) {
                if (phase4 == 0) {
                    GetComponent<GameController>().Success();
                }
            }
        } else if (identifier.Contains("Main5") && phase1 == 0 && phase2 == 0 && phase3 == 0 && phase4 == 0) {
            phase5--;
            if (phase5Check) {
                if (phase5 == 0) {
                    GetComponent<GameController>().Success();
                }
            }
        } else if (identifier == "Secondary") {
            secondary--;
        } else {
            if (!failed) {
                GetComponent<GameController>().Failure(missionType);
                failed = true;
            }
        }
    }
}
