using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameDataCarrier : MonoBehaviour
{

    public GameObject[] weapons;
    public GameObject currentWeapon;
    public GameObject weaponDisplay;
    public TMP_Text weaponTitle;
    public TMP_Text weaponDescription;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentWeapon = weapons[0];
        UpdateWeaponSelector();
    }

    void UpdateWeaponSelector() {
        print(currentWeapon.name);
        foreach (Transform child in weaponDisplay.transform) {
            print(child.name);
            if (child.name != currentWeapon.name) {
                child.gameObject.SetActive(false);
            } else {
                child.gameObject.SetActive(true);
                WeaponData weaponData = child.gameObject.GetComponent<WeaponData>();
                weaponTitle.text = weaponData.title;
                weaponDescription.text = weaponData.description;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }

    public GameObject getCurrentWeapon() {
        return currentWeapon;
    }

    
}
