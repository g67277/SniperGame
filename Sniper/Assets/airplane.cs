using UnityEngine;
using System.Collections;

public class airplane : MonoBehaviour {

    public GameObject zombie;
    public AnimationClip clip;
    void OnTriggerEnter(Collider other) {
        Debug.LogError("ERRORRRRRR!!!!");
        this.transform.Rotate(90f, 0f, 0f);

        Destroy(other.gameObject);
    }

    void Update () {

       
    }
}
