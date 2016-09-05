using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

    Animator anim;


    //public Animator animator;
    //public AnimationClip die;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //PlayAnimation die = hit
        //AnimationPlayMode.Queue.Equals(1);

	}

    void OnCollisionEnter() {
        GetComponent<Animator>().Play("Die", 1);
        Debug.Log("Punk Got hit....");
    }

    public void Animate (GameObject gameObject) {
        GetComponent<Animator>().Play("Die", 1);
    }
	
	// Update is called once per frame
	void Update () {

       // float move = 
	
	}
}
