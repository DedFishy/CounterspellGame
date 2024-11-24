using UnityEngine;

public class Rocket : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rocketBody;
    public Vector3 rotOffset;
    public float faceDampening;
    public float speed;
    public AudioSource boom;
    public AudioSource death;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        boom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        rocketBody.AddRelativeForce(new Vector3(speed, 0, 0));

        var lookPos = player.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(rotOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * faceDampening);
        //transform.LookAt(player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7/*Player*/) {
            other.gameObject.GetComponent<PlayerController>().DealDamage(20f, "Rocket to the face");
            Destroy(transform.gameObject);
        } else if (other.gameObject.layer == 8/*Enemies*/) {
            Destroy(other.gameObject);
            death.Play();
            Destroy(transform.gameObject);
            
        }
    }
}
