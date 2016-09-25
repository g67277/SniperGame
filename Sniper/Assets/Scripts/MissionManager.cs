using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

    public Camera camera;
    public GameObject circle;
    public GameObject logo;
    public GameObject logoSpawn;
    GameObject view;

    public Vector3 ExitPosition;
    public Vector3 levelPosition;
    public int sceneIndex;

    bool isExpanding;
    // Use this for initialization
    void Start () {
	
	}

    public void missionSelection(string name) {


        if (name == "Home") {
            expandLogo();

        } else {
            expandLogo();
            Debug.Log("Mission name: " + name);
            if (name == "Mission1") {
                sceneIndex = 1;
            } else if (name == "mission2") {
                sceneIndex = 2;
            }
        }
        
    }

    public void expandLogo() {
        view = Instantiate(circle, logoSpawn.transform.position, logoSpawn.transform.rotation) as GameObject;
        view.transform.parent = gameObject.transform.parent.transform ;
        view.transform.localScale = new Vector3(0f, 0f, 0f);
        gameObject.GetComponent<TeleportVive>().enabled = false;
        isExpanding = true;
    }

    public void deflateLogo() {
        //camera.GetComponent<TeleportVive>().enabled = false;
        view = Instantiate(circle, logoSpawn.transform.position, logoSpawn.transform.rotation) as GameObject;
        view.transform.parent = gameObject.transform.parent.transform;
        view.transform.localScale = new Vector3(5f, 5f, 5f);
        isExpanding = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (view != null) {
            if (isExpanding) {
                if (view.transform.localScale.x <= 5) {
                    view.transform.localScale = new Vector3(view.transform.localScale.x + 0.05f, view.transform.localScale.y + 0.05f, view.transform.localScale.z + 0.05f);
                    if (view.transform.localScale.x > 1.5f && view.transform.localScale.x < 1.8f) {
                        gameObject.transform.root.transform.position = ExitPosition;
                    }
                }else if (view.transform.localScale.x > 4.9f) {
                    SceneManager.LoadScene(sceneIndex);
                    deflateLogo();
                }
            }else {
                if (view.transform.localScale.x >= 0.1f) {
                    view.transform.localScale = new Vector3(view.transform.localScale.x - 0.05f, view.transform.localScale.y - 0.05f, view.transform.localScale.z - 0.05f);
                    if (view.transform.localScale.x > 1.5f && view.transform.localScale.x < 1.8f) {
                        gameObject.transform.root.transform.position = levelPosition;
                    }
                }else {
                    Destroy(view);
                    //camera.GetComponent<TeleportVive>().enabled = true;
                }
            }
        }
    }
}
