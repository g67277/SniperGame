using UnityEngine;
using System.Collections;

public class PersonAnimator : MonoBehaviour {

    public int scoreValue = 1;
    Animator animator;
    
    public void stopAnimation() {
        animator = GetComponent<Animator>();
        animator.Stop();
    }

    public void hitResult(string name, string tag) {
        animator = GetComponent<Animator>();
        Debug.Log("*****Animator class hit string:" + name);
        Debug.Log("*****Animator class hit tag:" + tag);
        if (name == "Head_jnt" || name == "Spine_jnt") {
            animator.Stop();
            
            ScoreManager.score += scoreValue;
            gameObject.GetComponent<PedestrianObject>().enabled = false;
            //OnAIDeath();
        } else if(tag == "MiniTarget"){
            animator.Play("Death_02", -1, 0f);
            
            //yield return new WaitForSeconds(10f);
        } else {
            animator.Play("Run", -1, 0f);
        }

    }



    //Testing
    Collider[] rigColliders;
    Rigidbody[] rigRigidbodies;

    public void OnAIDeath() {

        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        //wait 2-3 seconds.
        foreach (Collider col in rigColliders) {
            col.enabled = false;
        }

        Invoke("StopBody", 2.0f);
    }

    void StopBody() {
        foreach (Rigidbody rb in rigRigidbodies) {
            rb.isKinematic = true;
        }
    }

}
