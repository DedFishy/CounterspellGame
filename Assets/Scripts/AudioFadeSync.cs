using UnityEngine;

public class AudioFadeSync : MonoBehaviour
{

    public FadeInOut fadeInOut;
    public AudioSource audioSource;

    private bool hasStartedFadeIn = false;
    private bool hasStartedFadeOut = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    float PlayPercentage() {
        return audioSource.time / audioSource.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying && !hasStartedFadeIn) {
            hasStartedFadeIn = true;
            fadeInOut.nextStage();
        } else if (!audioSource.isPlaying && hasStartedFadeIn && !hasStartedFadeOut) {
            hasStartedFadeOut = true;
            fadeInOut.nextStage();
        }
    }
}
