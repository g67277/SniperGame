using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public new Light light;
    public GameObject trail;

    [Header("Audio")]
    public AudioSource trailAudio;
    public AudioSource explosion;

    public GameObject player;
    bool attack = false;

	// Use this for initialization
	void Start () {
	
	}

    public void missleAttack() {
        light.enabled = true;
        trail.SetActive(true);
        attack = true;
        trailAudio.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (attack) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 50f * Time.deltaTime);
            transform.LookAt(2 * transform.position - player.transform.position);
            Vector3 offset = transform.position - player.transform.position;
            float sqrlen = offset.sqrMagnitude;
            Debug.Log("Distance: " + sqrlen);

            if (sqrlen < 15) {
                explosion.Play();
                attack = false;
                trailAudio.Stop();
            }
        }
    }
}
