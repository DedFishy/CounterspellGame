using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PlayerController : MonoBehaviour
{

    InputAction moveAction;
    InputAction lookAction;
    InputAction fireAction;
    Rigidbody playerBody;

    public float speed;
    public float lookMultiplier;
    public GameObject playerCamera;
    public Volume postProcessingVolume;
    public GameObject deathIndicator;
    public GameObject deathReasonIndicator;
    public GameObject weapon;
    public BaseWeapon weaponController;
    public GameObject theEnding;
    public AudioSource crunch;

    private bool isDead = false;

    private float health = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        fireAction = InputSystem.actions.FindAction("Fire");
        playerBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        GameObject gameDataCarrierObject = GameObject.Find("GameDataCarrier");
        if (gameDataCarrierObject != null) {
            weapon = gameDataCarrierObject.GetComponent<GameDataCarrier>().getCurrentWeapon();
        }

        weaponController = weapon.GetComponent<BaseWeapon>();

        weapon = Instantiate(weapon, weaponController.position, Quaternion.Euler(weaponController.rotation));
        weapon.transform.parent = playerCamera.transform;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {
            Vector2 moveValue = moveAction.ReadValue<Vector2>() * speed;
            playerBody.linearVelocity = transform.forward * moveValue.y + transform.right * moveValue.x - transform.up * 2;

            Vector2 lookDelta = lookAction.ReadValue<Vector2>() * lookMultiplier;
            playerCamera.transform.Rotate(new Vector3(-lookDelta.y, 0, 0));
            transform.Rotate(new Vector3(0, lookDelta.x, 0));

            if (Vector3.Distance(transform.position, theEnding.transform.position) < 3) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                float fireValue = fireAction.ReadValue<float>();
                if (fireValue > 0.5) {
                    weaponController.Fire();
                }
            }
            


        } else {
            transform.eulerAngles = new Vector3(-90, transform.eulerAngles.y, transform.eulerAngles.z);
            deathIndicator.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        /*if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;*/
    }

    public void DealDamage(float damage, string deathMessage) {
        if (isDead) return;
        health -= damage;
        print(health);
        if (health <= 0) {
            isDead = true;
            deathReasonIndicator.GetComponent<TMP_Text>().text = deathMessage;
            crunch.Play();
        }

        Vignette vignette;
        if(postProcessingVolume.profile.TryGet<Vignette>( out vignette ) )
        {
            vignette.intensity.Override(1-health/100f);
            
            print("Set vignette intensity");
            print(1-health/100f);
        }
    }
}
