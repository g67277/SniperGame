using UnityEngine;
using System.Collections;

public class testingAnimatino : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(pointGun());
	}
	
    IEnumerator pointGun() {
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<Animator>().Play("Character_Handgun_Idle", -1, 0f);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
