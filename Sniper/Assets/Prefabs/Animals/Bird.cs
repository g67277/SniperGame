using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void hitBird(GameObject bird) {
        GameObject body = bird.transform.root.gameObject;
        ScoreManager.score = 50;
        bird.GetComponent<Rigidbody>().isKinematic = false;
        body.transform.Rotate(new Vector3(90f, 0f, 0f));
        body.GetComponent<PedestrianObject>().enabled = false;
        body.AddComponent<Rigidbody>().useGravity = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -30) {
            Destroy(gameObject);
        }
    }
}
