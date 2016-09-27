using UnityEngine;
using System.Collections;

public class Weapons : ScriptableObject {


    Sniper sniper;
    GunScript gunScript;

    public void Start() {

    }

    public void setupWeapon(GameObject incomingSniper) {

        if (incomingSniper.name == "Sniper2") {
            gunScript = incomingSniper.GetComponent<GunScript>();
            gunScript.newMinFOV = 16;
            gunScript.bulletSpeedMultiplier = 6;
        } else if (incomingSniper.name == "Sniper4") {
            gunScript = incomingSniper.GetComponent<GunScript>();
            gunScript.bulletSpeedMultiplier = 10;
        } else if (incomingSniper.name == "Sniper6") {
            gunScript = incomingSniper.GetComponent<GunScript>();
            gunScript.bulletSpeedMultiplier = 3;
        }
    }
	

}
