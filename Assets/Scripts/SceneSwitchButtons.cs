using UnityEngine;

using UnityEngine.SceneManagement;
public class SceneSwitchButtons : MonoBehaviour
{
    public void LoadWeaponScene() {
        SceneManager.LoadScene("WeaponTutorial", LoadSceneMode.Single);
    }

    public void LoadEndScene() {
        SceneManager.LoadScene("TheEnding", LoadSceneMode.Single);
    }
}
