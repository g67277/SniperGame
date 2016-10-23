using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

    public GameObject hitDisplay;

    public static float CalculateDistance(Vector3 point1, Vector3 point2) {

        Vector3 offset = point1 - point2; //Checking distance for multiplier
        float sqrlen = offset.sqrMagnitude;
        return sqrlen;
    }

    public void displayHitScore(int hitScore, GameObject incomingObject, GameObject player) {
        Debug.Log("Display score method hit");
        GameObject scoreDisplay2 = Instantiate(hitDisplay, new Vector3(incomingObject.transform.position.x + 0.1f, incomingObject.transform.position.y + 3.3f, incomingObject.transform.position.z - 0.8f), incomingObject.transform.rotation) as GameObject;
        if (hitScore < 0) {
            scoreDisplay2.GetComponent<TextMesh>().text = hitScore.ToString();
        } else {
            scoreDisplay2.GetComponent<TextMesh>().text = "+" + hitScore.ToString();
        }
        scoreDisplay2.transform.LookAt(2 * scoreDisplay2.transform.position - player.transform.position);
    }
}
