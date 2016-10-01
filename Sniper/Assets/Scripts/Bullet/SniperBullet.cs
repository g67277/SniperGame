using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SniperBullet : MonoBehaviour {

    [Header("Metal")]
    [Header("Bullet Impacts & Tags")]
    public Transform metalImpactStaticPrefab;
    public Transform metalImpactPrefab;
    [Header("Wood")]
    public Transform woodImpactStaticPrefab;
    public Transform woodImpactPrefab;
    [Header("Concrete")]
    public Transform concreteImpactStaticPrefab;
    public Transform concreteImpactPrefab;
    [Header("Dirt")]
    public Transform dirtImpactStaticPrefab;
    public Transform dirtImpactPrefab;

    [Header("Impact Tags")]
    //Default tags for bullet impacts
    public string metalImpactStaticTag = "Metal (Static)";
    public string metalImpactTag = "Metal";
    public string woodImpactStaticTag = "Wood (Static)";
    public string woodImpactTag = "Wood";
    public string concreteImpactStaticTag = "Concrete (Static)";
    public string concreteImpactTag = "Concrete";
    public string dirtImpactStaticTag = "Dirt (Static)";
    public string dirtImpactTag = "Dirt";

    public Vector3 currentPosition;
    public Vector3 currentVelocity;
    Vector3 fireDirection;

    //public Animator animator;
    PersonAnimator pAnimator;

    Vector3 newPosition = Vector3.zero;
    Vector3 newVelocity = Vector3.zero;


    void Awake() {
        currentPosition = transform.position;
    }

    void Update() {
        DestroyBullet();
    }

    void FixedUpdate() {
        MoveBullet();
    }

    //Did we hit a target
    void CheckHit() {
        fireDirection = (newPosition - currentPosition).normalized;
        float fireDistance = Vector3.Distance(newPosition, currentPosition);

        RaycastHit hit;

        if (Physics.Raycast(currentPosition, fireDirection, out hit, fireDistance)) {
            if (hit.collider.CompareTag("Target")  || hit.collider.CompareTag("MiniTarget")) {
                TargetHit(hit.collider.gameObject);
            }else if (hit.collider.CompareTag("Navigation")) {
                NavHit();
            } else if (hit.collider.CompareTag("GasTank")) {
                TankHit(hit.collider.gameObject);
            } else if (hit.collider.CompareTag("ExplosiveBarrel")) {
                BarrelHit(hit.collider.gameObject);
            } else if (hit.collider.CompareTag("Civilian")) {
                CivilianHit(hit.collider.gameObject);
            } else if (hit.collider.CompareTag("Bird")) {
                hit.collider.gameObject.transform.root.gameObject.GetComponent<Bird>().hitBird(hit.collider.gameObject);
                
            } else if (hit.transform.tag == metalImpactStaticTag) {
                (Instantiate(metalImpactStaticPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as Transform).parent = hit.collider.gameObject.transform;
            }else if (hit.transform.tag == metalImpactTag) {
                Instantiate(metalImpactPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }else if (hit.transform.tag == woodImpactStaticTag) {
                float distance = Vector3.Distance(hit.point, hit.collider.bounds.center);
                string part = hit.collider.name;
                if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<DummyTarget>() != null) {
                    hit.collider.gameObject.transform.parent.gameObject.GetComponent<DummyTarget>().calculateHit(distance, part);
                }
                (Instantiate(woodImpactStaticPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as Transform).parent = hit.collider.gameObject.transform;
                Destroy(gameObject);
            } else if (hit.transform.tag == woodImpactTag) {
                Instantiate(woodImpactPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            } else if (hit.transform.tag == concreteImpactStaticTag) {
                (Instantiate(concreteImpactStaticPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as Transform).parent = hit.collider.gameObject.transform;
            }else if (hit.transform.tag == concreteImpactTag) {
                Instantiate(concreteImpactPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }else if (hit.transform.tag == dirtImpactStaticTag) {
                (Instantiate(dirtImpactStaticPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as Transform).parent = hit.collider.gameObject.transform;
            }else if (hit.transform.tag == dirtImpactTag) {
                Instantiate(dirtImpactPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
        }
    }

    void TargetHit(GameObject target) {
        pAnimator = target.transform.root.gameObject.GetComponent<PersonAnimator>();

        Vector3 forceDirection = new Vector3(fireDirection.x, fireDirection.y, fireDirection.z);
        pAnimator.hitResult(target.name, target.tag);
        if (target.tag == "MiniTarget") {
            GameObject.Find("Camera (eye)").GetComponent<MissionManager>().missionSelection(target.name);
        }
        target.GetComponent<Rigidbody>().AddForce(forceDirection * 1000);
        Destroy(gameObject);
    }

    void BarrelHit(GameObject barrel) {
        barrel.GetComponent<ExplosiveBarrelScript>().explode = true;
    }

    void TankHit(GameObject tank) {
        tank.GetComponent<GasTankScript>().isHit = true;
    }

    void NavHit() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            SceneManager.LoadScene(1);
        } else {
            SceneManager.LoadScene(0);
        }
    }

    void CivilianHit(GameObject civilian) {

    }

    void MoveBullet() {
        //Use an integration method to calculate the new position of the bullet
        float h = Time.fixedDeltaTime;
        Ballistics.CurrentIntegrationMethod(h, currentPosition, currentVelocity, out newPosition, out newVelocity);

        //First we need these coordinates to check if we have hit something
        CheckHit();

        currentPosition = newPosition;
        currentVelocity = newVelocity;

        //Add the new position to the bullet
        transform.position = currentPosition;
    }

    void DestroyBullet() {
        if (transform.position.y < -30) {
            Destroy(gameObject);
        }
    }
}
