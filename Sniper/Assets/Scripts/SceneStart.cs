using UnityEngine;
using System.Collections;

public class SceneStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Camera (eye)").GetComponent<MissionManager>().deflateLogo();
    }

    void Update() {

        double accuracy = (double)PlayerPrefs.GetInt("HitsNum") / (double)PlayerPrefs.GetInt("BulletsNum");
        PlayerPrefs.SetFloat("TotalAccuracy", (float)accuracy);
        
    }
}
