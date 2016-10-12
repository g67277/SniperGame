using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {

    public int missionIndex;
    public bool inMission;

	// Use this for initialization
	void Start () {
        DataHolder.missionIndex = missionIndex;
        DataHolder.inMission = inMission;
        if (DataHolder.isReset) {
            GetComponent<MissionManager>().afterReset();
        } else {
            GetComponent<MissionManager>().deflateLogo();
        }
    }
}
