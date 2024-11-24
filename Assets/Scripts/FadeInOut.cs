using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{

    private RawImage overlay;
    private Color currentColor;
    public Color startColor;
    public Color endColor;
    public float transitionTime;


    /*
    Stages:
    0: Do nothing
    1: Fade to end
    2: Fade to beginning
    3: Do nothing
    */
    private int stage = 0;
    private float startTime = 0;

    public string nextScene;

    protected bool hasFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        overlay = GetComponent<RawImage>();
        currentColor = overlay.color;
    }

    float GetAnimationProgress() {
        return (Time.time-startTime)/transitionTime;
    }

    // Update is called once per frame
    void Update()
    {
        float animProgress = GetAnimationProgress();
        if (animProgress > 1) {
            if (stage >= 2) {
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            }
            return;
        }
        if (stage == 1) {
            currentColor = Color.Lerp(startColor, endColor, animProgress);
        }
        else if (stage == 2) {
            currentColor = Color.Lerp(endColor, startColor, animProgress);
        }
        overlay.color = currentColor;

    }
    public void nextStage() {
        startTime = Time.time;
        stage++;
    }
}
