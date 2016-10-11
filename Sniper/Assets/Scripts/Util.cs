using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

    public static float CalculateDistance(Vector3 point1, Vector3 point2) {

        Vector3 offset = point1 - point2; //Checking distance for multiplier
        float sqrlen = offset.sqrMagnitude;
        return sqrlen;
    }
}
