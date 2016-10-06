using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

    public GameObject player;
    public GameObject rotor;
    public GameObject heliFront;

    bool moveHeliUp = false;
    bool moveToPlayer = false;

    int heliCeiling = 0;

    public void heliAttack() {
        rotor.GetComponent<Animator>().enabled = true;
        moveHeliUp = true;
    }
	
    void Start() {
        heliAttack();
    }
	// Update is called once per frame
	void Update () {
	    if (moveHeliUp) {
            moveUp();
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);
            heliFront.transform.LookAt(player.transform);

        }
    }

    void moveUp() {

        //while (heliCeiling < 6) {
     
            Vector3 heliHight = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        if (heliHight.y < 30) {
            transform.position = Vector3.MoveTowards(transform.position, heliHight, 5f * Time.deltaTime);
            Debug.Log(heliHight.y);
        }
         //   heliCeiling++;
       // }

        


    }

}
