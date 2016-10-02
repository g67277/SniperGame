using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (DataHolder.isReset) {
            GameObject.Find("Camera (eye)").GetComponent<MissionManager>().afterReset();
        } else {
            GameObject.Find("Camera (eye)").GetComponent<MissionManager>().deflateLogo();
        }
    }
}
