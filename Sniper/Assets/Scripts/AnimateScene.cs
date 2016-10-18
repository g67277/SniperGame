using UnityEngine;
using System.Collections;

public class AnimateScene : MonoBehaviour {

    public GameObject[] animatedObjects;

    public void animateScene() {
        if (animatedObjects != null) {
            for (int i = 0; i < animatedObjects.Length; i++) {
                animatedObjects[i].GetComponent<Animator>().enabled = true;
            }
        }
        
    }
	
}
