using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {

    public bool inMission;

	// Use this for initialization
	void Start () {
        DataHolder.inMission = inMission;
        if (DataHolder.isReset) {
            GetComponent<MissionManager>().afterReset();
            DataHolder.isReset = false;
        } else {
            GetComponent<MissionManager>().deflateLogo();
        }
    }
}
