using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostSpawner : MonoBehaviour
{
    public LoopPoints loopScript;
    public GameObject boostEnlargePrefab;
    public Transform boosts;

    public float timeSinceLastSpawn;
    public float timeBeforeSpawn = 5f;
    public float closer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBeforeSpawn)
        {
            SpawnEnlarge();
            timeSinceLastSpawn = 0;
        }
    }


    public void SpawnEnlarge()
    {
        SpawnBoost(boostEnlargePrefab);
    }

void SpawnBoost(GameObject prefab)
    {
        //Find position on circle
        float x = Random.Range(-loopScript.standardDistance, loopScript.standardDistance);
        float maxZ = Mathf.Sqrt(Mathf.Pow(loopScript.standardDistance, 2) - Mathf.Pow(x, 2));
        float z = Random.Range(0, maxZ);
        if (Random.value < 0.5)
        {
            z = -z;
        }
        Vector3 position = new Vector3(x, 0, z);

        // Remove shrinked + easiness factor (closer)
        if (position.x > 0)
        {
            position -= new Vector3(loopScript.shrinked+closer, 0, 0);
        }
        if (position.x < 0)
        {
            position += new Vector3(loopScript.shrinked + closer, 0, 0);
        }
        if (position.z > 0)
        {
            position -= new Vector3(0, 0, loopScript.shrinked + closer);
        }
        if (position.z < 0)
        {
            position += new Vector3(0, 0, loopScript.shrinked + closer);
        }



        GameObject.Instantiate(prefab, position, Quaternion.identity, boosts);
    }
}
