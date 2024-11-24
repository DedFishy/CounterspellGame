using UnityEngine;

public class RocketLauncherWeapon : BaseWeapon
{

    public GameObject rocket;
    public GameObject player;
    public float cooldown = 1;
    public float lastFire = 0;

    void Start() {
        lastFire = 0;
    }

    public override Vector3 position 
    {
        get {
            return new Vector3(0.3f, 0.5f, 0.3f);
        }
    }

    public override Vector3 rotation 
    {
        get {
            return new Vector3(0, -90, 0);
        }
    }
    public override void Fire() {
        print("Firing rocket");
        if (lastFire > Time.time) {
            lastFire = Time.time - cooldown;
        }
        print(transform.gameObject.name);
        if (lastFire + cooldown > Time.time) {
            print("nvm");
            print(lastFire + " " + cooldown + " " + Time.time);
            return;
        }
        lastFire = Time.time;
        print(lastFire + " " + cooldown + " " + Time.time);
        if (player == null) player = GameObject.Find("Player"); // YEAH SURE WHY NOT
        GameObject newRocket = Instantiate(rocket);
        newRocket.transform.position = player.transform.position + player.transform.forward * 1;
        newRocket.transform.Rotate(player.transform.rotation.eulerAngles);
        newRocket.GetComponent<Rigidbody>().linearVelocity = player.transform.forward * 5;
    }
}
