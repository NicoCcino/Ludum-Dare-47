using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroids : MonoBehaviour
{

    public float spawnDistance = 50f;
    public GameObject[] asteroidPrefabs;
    public Transform asteroidsTransform;
    public Transform playerTransform;
    public float speed;
    public float spawnTime;
    public float deltaTime = 0;
    private float initialSpawnTime = 10f;
    public bool increasingFrequency = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
        initialSpawnTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (increasingFrequency)
        {
            spawnTime = Mathf.Max(0.1f, spawnTime - (Time.deltaTime / 100f));
        }

        deltaTime += Time.deltaTime;
        if (deltaTime >= spawnTime)
        {
            SpawnAsteroid();
            deltaTime = 0;
        }
    }

    void SpawnAsteroid()
    {
        int randIndex;
        randIndex = Mathf.FloorToInt(Random.Range(0, asteroidPrefabs.Length));
        GameObject asteroidPrefab = asteroidPrefabs[randIndex];

        float x = 0;
        float z = 0;

        float rand = Random.value;
        if (rand <= 0.25f)
        {
            x = -spawnDistance;
            z = Random.Range(-spawnDistance, spawnDistance);
        }
        else if (rand <= 0.5f)
        {
            x = spawnDistance;
            z = Random.Range(-spawnDistance, spawnDistance);
        }
        else if (rand <= 0.75f)
        {
            z = -spawnDistance;
            x = Random.Range(-spawnDistance, spawnDistance);
        }
        else
        {
            z = spawnDistance;
            x = Random.Range(-spawnDistance, spawnDistance);
        }

        GameObject asteroid = Instantiate(asteroidPrefab, new Vector3(x, 0, z), Quaternion.identity, asteroidsTransform);
        asteroid.GetComponent<Rigidbody>().AddForce((playerTransform.position - asteroid.transform.position) * speed);
        asteroid.GetComponent<Rigidbody>().AddTorque((playerTransform.position - asteroid.transform.position) * speed /2);

        Destroy(asteroid, 15f);
    }
}
