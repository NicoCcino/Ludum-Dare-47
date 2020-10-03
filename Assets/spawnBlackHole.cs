using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBlackHole : MonoBehaviour
{
    public GameObject blackHole;
    public float timeBeforeSpawn;
    public float deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        deltaTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= timeBeforeSpawn)
        {
            blackHole.SetActive(true);
        }
    }
}
