using UnityEngine;
using System.Collections;

public class SubtitleScrollScript : MonoBehaviour {

    public float scrollSpeed;

    void Start() {

    }

    void Update() {
        transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
    }
}