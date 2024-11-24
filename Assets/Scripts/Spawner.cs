using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;
    private float lastSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(lastSpawn + spawnRate - Time.time);
        if (lastSpawn + spawnRate < Time.time) {
            lastSpawn = Time.time;
            Instantiate(enemy, transform.position, transform.rotation).SetActive(true);
        }
    }
}
