using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialBullet : MonoBehaviour {

    public Vector3 currentPosition;
    public Vector3 currentVelocity;

    //public Animator animator;
    PersonAnimator pAnimator;

    Vector3 newPosition = Vector3.zero;
    Vector3 newVelocity = Vector3.zero;


    void Awake() {
        currentPosition = transform.position;
    }

    void Update() {
        DestroyBullet();
    }

    void FixedUpdate() {
        MoveBullet();
    }



    //Did we hit a target
    void CheckHit() {
        Vector3 fireDirection = (newPosition - currentPosition).normalized;
        float fireDistance = Vector3.Distance(newPosition, currentPosition);

        RaycastHit hit;
        //int die = Animator.StringToHash("Die");

        if (Physics.Raycast(currentPosition, fireDirection, out hit, fireDistance)) {
            if (hit.collider.CompareTag("Target")) {
                
                Debug.Log("Hit target!");
                pAnimator = hit.collider.gameObject.transform.root.gameObject.GetComponent<PersonAnimator>();
                //pAnimator.animateHit(hit.collider);
                //animator = hit.collider.gameObject.GetComponent<Animator>();
                //animator.Play("Death_01", -1, 0f);

                Vector3 forceDirection = new Vector3(fireDirection.x, fireDirection.y, fireDirection.z);
                //Debug.Log("Hit point name: " + hit.collider.gameObject.name);
                pAnimator.hitResult(hit.collider.gameObject.name);
                //pAnimator.stopAnimation();
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * 1000);
                
                Destroy(gameObject);
            }else if (hit.collider.CompareTag("Navigation")) {
                if (SceneManager.GetActiveScene().buildIndex == 0) {
                    SceneManager.LoadScene(1);
                }else {
                    SceneManager.LoadScene(0);
                }

            }
        }
    }

    void MoveBullet() {
        //Use an integration method to calculate the new position of the bullet
        float h = Time.fixedDeltaTime;
        TutorialBallistics.CurrentIntegrationMethod(h, currentPosition, currentVelocity, out newPosition, out newVelocity);

        //First we need these coordinates to check if we have hit something
        CheckHit();

        currentPosition = newPosition;
        currentVelocity = newVelocity;

        //Add the new position to the bullet
        transform.position = currentPosition;
    }

    void DestroyBullet() {
        if (transform.position.y < -30f) {
            Destroy(gameObject);
        }
    }
}
