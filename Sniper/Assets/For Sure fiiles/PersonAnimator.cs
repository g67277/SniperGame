using UnityEngine;
using System.Collections;

public class PersonAnimator : MonoBehaviour {

    Animator animator;

    public void animateHit(Collider collider) {

        if (collider.GetType() == typeof(BoxCollider)) {
            animator = GetComponent<Animator>();
            animator.Play("Death_01", -1, 0f);
        }else if (collider.GetType() == typeof(SphereCollider)) {
            animator = GetComponent<Animator>();
            animator.Play("Death_02", -1, 0f);
        }
        
    }


    public void stopAnimation() {
        animator = GetComponent<Animator>();
        animator.Stop();
    }

    public void hitResult(string name) {
        animator = GetComponent<Animator>();
        Debug.Log("*****Animator class hit string:" + name);
        if (name == "Head_jnt" || name == "Spine_jnt") {
            animator.Stop();
        }else {
            animator.Play("Run", -1, 0f);
        }

    }

}
