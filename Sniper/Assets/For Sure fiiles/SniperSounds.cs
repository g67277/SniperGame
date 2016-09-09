using UnityEngine;
using System.Collections;

public class SniperSounds : MonoBehaviour {

    AudioSource shot;

  
	// Use this for initialization
	void Start () {
        shot = GetComponent<AudioSource>();

    }

    public void PlayShot() {

        shot.Play();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
