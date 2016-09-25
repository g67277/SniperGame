using UnityEngine;
using System.Collections;

public class MoveObjects : MonoBehaviour {

    // publically editable speed
    public float moveDelay = 0.0f;
    public float MoveTime = 0.5f;
    public bool moveOnStart = false;
    private bool logInitialMoveSequence = false;

    // store colours
    private Vector3[] vectors;

    // allow automatic moving on the start of the scene
    IEnumerator Start() {
        //yield return null; 
        yield return new WaitForSeconds(moveDelay);

        if (moveOnStart) {
            logInitialMoveSequence = true;
            Move();
        }
    }




    // check the alpha value of most opaque object
    float MaxAlpha() {
        float maxAlpha = 0.0f;
        Renderer[] rendererObjects = GetComponentsInChildren<Renderer>();
        foreach (Renderer item in rendererObjects) {
            maxAlpha = Mathf.Max(maxAlpha, item.material.color.a);
        }
        return maxAlpha;
    }

    // fade sequence
    IEnumerator MoveSequence(float MoveTime) {
        // log fading direction, then precalculate fading speed as a multiplier
        //bool moving = (MoveTime < 0.0f);
        float movingSpeed = 1.0f / MoveTime;


        // get current position max alpha
        Vector3 currentPosition = gameObject.transform.position;

        // iterate to change position vector
        while (moveOnStart) {
            gameObject.transform.position = new Vector3(currentPosition.x, currentPosition.y += Time.deltaTime, currentPosition.z);
            Debug.Log("What is Time.delta: " + Time.deltaTime);
            yield return null;
        }

        //while ((alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) {
        //    alphaValue += Time.deltaTime * fadingOutSpeed;

        //    for (int i = 0; i < rendererObjects.Length; i++) {
        //        Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
        //        newColor.a = Mathf.Min(newColor.a, alphaValue);
        //        newColor.a = Mathf.Clamp(newColor.a, 0.0f, 1.0f);
        //        rendererObjects[i].material.SetColor("_Color", newColor);
        //    }

        //    yield return null;
        //}

        //Debug.Log("fade sequence end : " + fadingOut);

    }


    void Move() {
        Move(MoveTime);
    }

    void Move(float newMoveTime) {
        StopAllCoroutines();
        StartCoroutine("MoveSequence", newMoveTime);
    }
}
