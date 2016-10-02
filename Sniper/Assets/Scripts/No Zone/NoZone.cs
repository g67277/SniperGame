using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoZone : MonoBehaviour {

    new GameObject camera;
    public GameObject crawlerFront;
    public Text countDownDisplay;

    GameObject crawler;
    Vector3 crawlerPosition;
    Animator animator;
    AudioSource scream;

    // Use this for initialization
    void Start () {
        crawler = crawlerFront.transform.GetChild(0).gameObject;
        animator = crawler.GetComponent<Animator>();
        crawlerPosition = crawlerFront.transform.position;
        scream = crawler.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void startNoZone(Collider col) {
        //Set up the No zone elements 
        camera = col.gameObject;
        camera.GetComponent<Camera>().cullingMask = ~(1 << 10);
        countDownDisplay.enabled = true;
        StartCoroutine(countDown());
        gameObject.GetComponent<AudioSource>().Play();          //Play Nozone sound
    }

    IEnumerator countDown() {
        for (int i = 3; i >= 0; i--) {
            countDownDisplay.GetComponent<Text>().text = "Warning: \n Back Out \n" + i;
            if (i == 0) {
                countDownDisplay.enabled = false;
                crawler.SetActive(true);
                animator.Play("pounce", -1, 0f);                        //Play crawler animation
                scream.Play();                                          //Play crawler scream
                yield return new WaitForSeconds(.5f);
                crawlerFront.transform.position = Vector3.MoveTowards(crawlerFront.transform.position, camera.transform.position, 3f);
                crawlerFront.transform.LookAt(camera.transform);
                
            }else {
                yield return new WaitForSeconds(1f);
            }
        }
        
    }

    void killNoZone() {
        camera.GetComponent<Camera>().cullingMask = ~(1 << 9);
        StopAllCoroutines();
        countDownDisplay.enabled = false;
        crawlerFront.transform.position = crawlerPosition;
        crawler.SetActive(false);
        animator.Stop();                                        //Stop crawler animation
        scream.Stop();                                          //Stop crawler scream
        gameObject.GetComponent<AudioSource>().Stop();          //Stop Nozone sound
    }


    void OnTriggerEnter(Collider col) {
        if (col.tag == "MainCamera") {
            startNoZone(col);
        }
    }


    void OnTriggerExit() {
        //Reset the game without the No zone elements
        if (camera != null) {
            killNoZone();
        }
    }

}
