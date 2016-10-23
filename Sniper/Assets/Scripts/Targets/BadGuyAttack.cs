using UnityEngine;
using System.Collections;

public class BadGuyAttack : MonoBehaviour {

    GameObject player;
    Vector3 accuracy;

    [Header("Type of Bad Guy")]
    public bool Sniper = false;
    public bool Assult = false;
    public bool pistol = false;

    //Setup different weapon strengths
    float bulletSpeedMultiplier = 1;
    bool attack = false;
    bool stopAttack = false;
    float reloadTime;

    //Components
    [System.Serializable]
    public class components {
        [Header("Muzzleflash Holders")]
        public GameObject sideMuzzle;
        public GameObject bulletSpawn;
        //Array of muzzleflash sprites
        public Sprite[] muzzleflashSideSprites;

        [Header("Bullet Strip Components")]
        public GameObject BulletPrefab;
    }

    public void sniperAttack() {
            player = GameObject.Find("Camera (eye)");
            float randomNumber = Random.Range(25f, 40f);
            Invoke("setupSniper", randomNumber);
    }

    public components Components;

    public void startAttacking(bool attack, GameObject incomingPlayer = null) {
        if (attack) {
            stopAttack = false;
            player = incomingPlayer;
            float distance = Util.CalculateDistance(incomingPlayer.transform.position, transform.position);
            bulletSpeedMultiplier = distance / 2000;   //Calculates the speed of the bullet for each bad guy
            StartCoroutine(Muzzleflash());
        } else {
            stopAttack = true;
        }
    }


    //Show muzzleflash
    IEnumerator Muzzleflash() {

        while (!stopAttack) {
            setupBGClass();

            //Disable raycast bullet for rpg and grenade launcher, since they dont use it
            //Vector3 shootingAnimation = transform.position + new Vector3(2.7f, 1.3f, -.4f);
            GameObject go = Instantiate(Components.BulletPrefab, Components.bulletSpawn.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

            //Add velocity to the non-physics bullet
            go.GetComponent<SniperBullet>().currentVelocity = (Ballistics.bulletSpeed * bulletSpeedMultiplier) * (accuracy - go.transform.localPosition).normalized;

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
            yield return new WaitForSeconds(reloadTime);
        }
    }


    void setupSniper() {
        transform.LookAt(player.transform);
        startAttacking(true, player);
    }

    void setupBGClass() {
        if (Sniper) {
            reloadTime = 2f;
            accuracy = player.transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
        }else if (Assult) {
            reloadTime = 0.5f;
            accuracy = player.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        } else if (pistol) {
            reloadTime = 0.8f;
            accuracy = player.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        }
    }
}
