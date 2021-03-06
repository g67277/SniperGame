﻿using UnityEngine;
using System.Collections;

public class TutorialFireBullets : MonoBehaviour {
    public GameObject bulletObj;
    public Transform bulletParent;
    public Transform spawnPoint;

    void Start() {
        StartCoroutine(FireBullet());
    }

    public IEnumerator FireBullet() {
        while (true) {
            //Create a new bullet
            GameObject newBullet = Instantiate(bulletObj, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

            //Parent it to get a less messy workspace
            newBullet.transform.parent = bulletParent;

            //Add velocity to the non-physics bullet
            newBullet.GetComponent<TutorialBullet>().currentVelocity = TutorialBallistics.bulletSpeed * transform.forward;


            //Add velocity to the bullet with a rigidbody
            //newBullet.GetComponent<Rigidbody>().velocity = TutorialBallistics.bulletSpeed * transform.forward;

            yield return new WaitForSeconds(2f);
        }
    }
}