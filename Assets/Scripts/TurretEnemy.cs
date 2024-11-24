using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public GameObject player;
    public GameObject turretTop;
    public PlayerController controller;
    public GameObject ammo;
    public float firingRate;
    public float lastFire;
    Rigidbody turretRigidbody;
    LayerMask targetLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetLayerMask = LayerMask.GetMask("Player", "Level");
        turretRigidbody = GetComponent<Rigidbody>();
        controller = player.GetComponent<PlayerController>();
    }

    bool IsPlayerInLineOfSight() {
        Vector3 rayDirection = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, targetLayerMask)) {
            print("Raycast hit: " + hit.transform.gameObject.name);

            return (hit.transform == player.transform);

        }
        print("Raycast failed");
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 25) {
            if (IsPlayerInLineOfSight()) {
                print("Player is in line-of-sight; moving");
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                turretTop.transform.LookAt(player.transform);
                transform.Rotate(0, 90, 0);
                //turretTop.transform.Rotate(0, 90, 0);
                if (lastFire + firingRate < Time.time) {
                    lastFire = Time.time;
                    GameObject newRocket = Instantiate(ammo, transform.position + -transform.right*3, transform.rotation);
                    newRocket.transform.Rotate(0, 90, 0);
                    newRocket.GetComponent<Rigidbody>().linearVelocity = newRocket.transform.forward * 2;
                }
            }
        }
        if (distanceToPlayer < 4) {
            controller.DealDamage(0.1f, "Electrocuted by Spot");
        }
    }
}
