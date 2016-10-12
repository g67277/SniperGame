using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

    public new Camera camera;
    public GameObject circle;
    public GameObject logo;
    public GameObject logoSpawn;
    GameObject view;

    public Vector3 ExitPosition;
    public Vector3 levelPosition;
    public Vector3 ResetPosition;
    public bool isReset = false;
    public int sceneIndex;

    bool isExpanding;

    //For levels that don't have the teleporter
    public bool hasTeleporter = true;
    // Use this for initialization
    void Start () {
	
	}

    public void missionSelection(string name) {

        if (name == "Home") {
            expandLogo();

        } else {
            findWeapon();
            StartCoroutine(waitTillDeath(1.5f));
            Debug.Log("Mission name: " + name);

            if (name == "Mission1") {
                sceneIndex = 1;
            } else if (name == "mission2") {
                sceneIndex = 2;
            }
        }
    }

    public void resetMission() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void afterReset() {
        float[] playerPosition = DataHolder.resetCoordinates;
        Vector3 position = new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
        GameObject.Find("[CameraRig]").transform.position = position;
        ;
    }

    IEnumerator waitTillDeath(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        expandLogo();
    }

    public void findWeapon() {
        GameObject root = gameObject.transform.root.gameObject;
        for (int i = 0; i < 2; i++) {
            GameObject controller = root.transform.GetChild(i).gameObject;
            for (int j = 0; j < controller.transform.childCount; j++) {
                GameObject cObj = controller.transform.GetChild(j).gameObject;
                if (cObj.tag == "Pickable") {
                    DataHolder.missionWeapon = cObj.name;
                    for (int s = 0; s < cObj.transform.childCount; s++) {
                        GameObject sObj = cObj.transform.GetChild(s).gameObject;
                        if (sObj.tag == "Scope") {
                            DataHolder.missionScope = sObj.name;
                        }
                    }
                }
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
        if (hasTeleporter) {
            camera.GetComponent<TeleportVive>().enabled = false;
        }
        view = Instantiate(circle, logoSpawn.transform.position, logoSpawn.transform.rotation) as GameObject;
        view.transform.parent = camera.transform.parent.transform;
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
                    DataHolder.saveData();
                    SceneManager.LoadScene(sceneIndex);
                    deflateLogo();
                }
            }else if (!isExpanding) {
                if (view.transform.localScale.x >= 0.1f) {
                    view.transform.localScale = new Vector3(view.transform.localScale.x - 0.05f, view.transform.localScale.y - 0.05f, view.transform.localScale.z - 0.05f);
                    if (view.transform.localScale.x > 1.5f && view.transform.localScale.x < 1.8f) {
                        gameObject.transform.root.transform.position = levelPosition;
                    }
                }else {
                    Destroy(view);
                    if (hasTeleporter) {
                        camera.GetComponent<TeleportVive>().enabled = true;
                    }
                }
            }
        }
    }
}
