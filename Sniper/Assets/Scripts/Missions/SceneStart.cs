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
            GameObject.Find("Camera (eye)").GetComponent<MissionManager>().afterReset();
        } else {
            GameObject.Find("Camera (eye)").GetComponent<MissionManager>().deflateLogo();
        }
    }
}
