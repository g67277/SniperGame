using UnityEngine;
using System.Collections;

public class MissionRules : MonoBehaviour {

    public int phase1;
    public int phase2;
    public int phase3;

    public bool checkRules(string objName) {
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
    
}
