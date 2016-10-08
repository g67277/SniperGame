using UnityEngine;
using System.Collections;

public class IslandMissionRules : MonoBehaviour {

    static int phase1 = 4;
    static int phase2 = 1;
    static int phase3 = 4;



    public static bool checkRules(string objName) {
        if (objName.Contains("Main1")){
            phase1--;
            return true;
        }else if (objName.Contains("Main2") && phase1 == 0) {
            phase2--;
            return true;
        }else if (objName.Contains("Main3") && phase1 == 0 && phase2 == 0) {
            phase3--;
            return true;
        }
        return false;
    }

    public void testing() {
        Debug.Log("It worked!!!!");
    }
}
