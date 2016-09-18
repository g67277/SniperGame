using UnityEngine;
using System.Collections;

public class IntegrationMethods : MonoBehaviour {

    //Heun's method - one iteration
    //Will give a better result than Euler forward, but will not match Unity's physics engine
    //so the bullets also have to use Heuns method
    public static void Heuns(
        float h,
        Vector3 currentPosition,
        Vector3 currentVelocity,
        out Vector3 newPosition,
        out Vector3 newVelocity) {
        //Init acceleration
        //Gravity
        Vector3 acceleartionFactorEuler = Physics.gravity;
        Vector3 acceleartionFactorHeun = Physics.gravity;


        //Init velocity
        //Current velocity
        Vector3 velocityFactor = currentVelocity;
        //Wind velocity
        //velocityFactor += new Vector3(5f, 0f, 0f);
        //
        //Main algorithm


        Vector3 vel_E = currentVelocity + h * acceleartionFactorEuler;


        //Heuns method
        Vector3 pos_H = currentPosition + h * 0.5f * (velocityFactor + vel_E);

        //acceleartionFactorHeun += BulletPhysics.CalculateDrag(vel_E);

        Vector3 vel_H = currentVelocity + h * 0.5f * (acceleartionFactorEuler + acceleartionFactorHeun);


        newPosition = pos_H;
        newVelocity = vel_H;
    }
}
