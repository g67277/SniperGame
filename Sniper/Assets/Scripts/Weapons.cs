using UnityEngine;
using System.Collections;

public class Weapons : ScriptableObject {


    Sniper sniper;

    public void Start() {

    }

    public void setupWeapon(GameObject incomingSniper) {

        if (incomingSniper.name == "Sniper") {
            sniper = incomingSniper.GetComponent<Sniper>();
            sniper.newMinFOV = 1;
            sniper.bulletSpeedMultiplier = 16;
        }else if (incomingSniper.name == "Sniper2") {
            sniper = incomingSniper.GetComponent<Sniper>();
            sniper.newMinFOV = 16;
            sniper.bulletSpeedMultiplier = 3;
        }
    }
	

}
