using UnityEngine;
using System.Collections;

public class Ballistics : MonoBehaviour {

    //Drags
    public Transform targetObj;
    public Transform gunObj;

    //The bullet's initial speed in m/s
    public static float bulletSpeed = 50f;

    //The step size
    static float h;

    //For debugging
    //private LineRenderer lineRenderer;

    void Awake() {
        //Can use a less precise h to speed up calculations
        //Or a more precise to get a more accurate result
        //But lower is not always better because of rounding errors
        h = Time.fixedDeltaTime * 1f;

        //lineRenderer = GetComponent<LineRenderer>();
    }
    //Which angle do we need to hit the target?
//Returns 0, 1, or 2 angles depending on if we are within range
    void CalculateAngleToHitTarget(out float? theta1, out float? theta2) {
        //Initial speed
        float v = bulletSpeed;

        Vector3 targetVec = targetObj.position - gunObj.position;

        //Vertical distance
        float y = targetVec.y;

        //Reset y so we can get the horizontal distance x
        targetVec.y = 0f;

        //Horizontal distance
        float x = targetVec.magnitude;

        //Gravity
        float g = 9.81f;


        //Calculate the angles

        float vSqr = v * v;

        float underTheRoot = (vSqr * vSqr) - g * (g * x * x + 2 * y * vSqr);

        //Check if we are within range
        if (underTheRoot >= 0f) {
            float rightSide = Mathf.Sqrt(underTheRoot);

            float top1 = vSqr + rightSide;
            float top2 = vSqr - rightSide;

            float bottom = g * x;

            theta1 = Mathf.Atan2(top1, bottom) * Mathf.Rad2Deg;
            theta2 = Mathf.Atan2(top2, bottom) * Mathf.Rad2Deg;
        } else {
            theta1 = null;
            theta2 = null;
        }
    }
    
    //How long did it take to reach the target (splash in artillery terms)?
    public float CalculateTimeToHitTarget() {
        //Init values
        Vector3 currentVelocity = gunObj.transform.forward * bulletSpeed;
        Vector3 currentPosition = gunObj.transform.position;

        Vector3 newPosition = Vector3.zero;
        Vector3 newVelocity = Vector3.zero;

        //The total time it will take before we hit the target
        float time = 0f;

        //Limit to 30 seconds to avoid infinite loop if we never reach the target
        for (time = 0f; time < 30f; time += h) {
            Ballistics.CurrentIntegrationMethod(h, currentPosition, currentVelocity, out newPosition, out newVelocity);

            //If we are moving downwards and are below the target, then we have hit
            if (newPosition.y < currentPosition.y && newPosition.y < targetObj.position.y) {
                //Add 2 times to make sure we end up below the target when we display the path
                time += h * 2f;

                break;
            }

            currentPosition = newPosition;
            currentVelocity = newVelocity;
        }
        return time;
    }

    //Easier to change integration method once in this method
    public static void CurrentIntegrationMethod(
        float h,
        Vector3 currentPosition,
        Vector3 currentVelocity,
        out Vector3 newPosition,
        out Vector3 newVelocity) {
        IntegrationMethods.Heuns(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
    }


}
