using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

    public GameObject player;
    public GameObject rotor;
    public Light heliLight;
    public GameObject missile;

    public AudioSource heliSound;

    bool moveHeliUp = false;
    bool moveToPlayer = false;

    public void heliAttack() {
        rotor.GetComponent<Animator>().enabled = true;
        moveHeliUp = true;
        moveToPlayer = true;
        heliSound.Play();
    }
	
    void Start() {
        //heliAttack();
    }
	// Update is called once per frame
	void Update () {
	    if (moveToPlayer) {
            if (moveHeliUp) {
                moveUp();
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);
            transform.LookAt(2 * transform.position - player.transform.position);
            Vector3 offset = transform.position - player.transform.position;
            float sqrLen = offset.sqrMagnitude;

            if (sqrLen < 7634) {
                moveToPlayer = false;
                missile.GetComponent<Missile>().player = player;
                missile.GetComponent<Missile>().missleAttack();
            }
        }
    }

    void moveUp() {
     
        Vector3 heliHight = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        if (heliHight.y < 30) {
            transform.position = Vector3.MoveTowards(transform.position, heliHight, 5f * Time.deltaTime);
        }else {
            moveHeliUp = false;
        }
    }

}
