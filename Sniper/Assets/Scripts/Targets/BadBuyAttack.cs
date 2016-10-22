using UnityEngine;
using System.Collections;

public class BadBuyAttack : MonoBehaviour {

    GameObject player;
    Vector3 accuracy;

    [Header("Type of Bad Guy")]
    public bool Sniper = false;
    public bool Assult = false;
    public bool pistol = false;

    //Setup different weapon strengths
    public float bulletSpeedMultiplier = 1;
    public bool attack = false;
    public bool stopAttack = false;

    //Components
    [System.Serializable]
    public class components {
        [Header("Muzzleflash Holders")]
        public GameObject sideMuzzle;
        //Array of muzzleflash sprites
        public Sprite[] muzzleflashSideSprites;
        [Header("Holders")]
        public Transform bulletSpawnPoint;


        [Header("Bullet Strip Components")]
        public GameObject BulletPrefab;
        [Header("Light Front")]
        public Light
            lightFlash;
    }

    public components Components;

    public void startAttacking(bool attack, GameObject incomingPlayer = null) {
        if (attack) {
            player = incomingPlayer;
            StartCoroutine(Muzzleflash());
            float distance = Util.CalculateDistance(incomingPlayer.transform.position, transform.position);
            bulletSpeedMultiplier = distance / 2000;   //Calculates the speed of the bullet for each bad guy
        }else {
            stopAttack = true;
        }
    }

    //Show muzzleflash
    IEnumerator Muzzleflash() {

        while (!stopAttack) {
            //Disable raycast bullet for rpg and grenade launcher, since they dont use it
            Vector3 shootingAnimation = transform.position + new Vector3(2.7f, 1.3f, -.4f);
            GameObject go = Instantiate(Components.BulletPrefab, shootingAnimation, Components.bulletSpawnPoint.transform.rotation) as GameObject;
            //Add velocity to the non-physics bullet
            if (Sniper) {
                accuracy = player.transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
            } else if (Assult) {
                accuracy = player.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
            } else if (pistol) {
                accuracy = player.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            }
            go.GetComponent<SniperBullet>().currentVelocity = (Ballistics.bulletSpeed * bulletSpeedMultiplier) * (accuracy - go.transform.position).normalized;

            Components.sideMuzzle.GetComponent<SpriteRenderer>().sprite = Components.muzzleflashSideSprites
                [Random.Range(0, Components.muzzleflashSideSprites.Length)];
            //Show the muzzleflashes
            Components.sideMuzzle.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<AudioSource>().Play();
            //Show the light flash

            //Wait for set amount of time, default value 0.02
            yield return new WaitForSeconds(0.02f);

            Components.sideMuzzle.GetComponent<SpriteRenderer>().enabled = false;

            //Wait before taking another shot
            if (Sniper) {
                yield return new WaitForSeconds(1f);
            } else if (Assult) {
                yield return new WaitForSeconds(0.5f);
            } else if (pistol) {
                yield return new WaitForSeconds(0.7f);
            }
            
        }
    }
}
