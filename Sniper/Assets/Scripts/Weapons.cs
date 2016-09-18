using UnityEngine;
using System.Collections;

public class Weapons : ScriptableObject {


    Sniper sniper;
    GunScript gunScript;

    public void Start() {

    }

    public void setupWeapon(GameObject incomingSniper) {

        if (incomingSniper.name == "Sniper") {
            sniper = incomingSniper.GetComponent<Sniper>();
            sniper.newMinFOV = 20;
            sniper.bulletSpeedMultiplier = 5;
        }else if (incomingSniper.name == "Sniper4") {
            gunScript = incomingSniper.GetComponent<GunScript>();
            gunScript.newMinFOV = 1;
            gunScript.bulletSpeedMultiplier = 16;
        }
    }
	

}
