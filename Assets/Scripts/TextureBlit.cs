using UnityEngine;

public class TextureBlit : MonoBehaviour
{
    public Camera gameCamera;
    public RenderTexture renderTex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameCamera.Render();
        Graphics.Blit(renderTex, null as RenderTexture);
    }
    
}
