using UnityEngine;

public class SpotEnemy : MonoBehaviour
{

    public GameObject player;
    public PlayerController controller;
    Rigidbody spotRigidbody;
    Animator animator;
    LayerMask targetLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null) {
            player = GameObject.Find("Player");
        }
        animator = GetComponent<Animator>();
        targetLayerMask = LayerMask.GetMask("Player", "Level");
        spotRigidbody = GetComponent<Rigidbody>();
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
        bool isWalking = false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 25) {
            if (IsPlayerInLineOfSight()) {
                print("Player is in line-of-sight; moving");
                isWalking = true;
                transform.LookAt(player.transform);
                spotRigidbody.AddForce(transform.forward * 10);
            }
        }
        if (distanceToPlayer < 4) {
            controller.DealDamage(0.1f, "Electrocuted by Spot");
        }
        animator.SetBool("isWalking", isWalking);
    }
}
