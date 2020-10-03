using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointInfo : MonoBehaviour
{
    public Vector3 positionOnCircle;
    public GameObject previousWall;
    public GameObject nextWall;
    public float offsetX;
    public float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0, 9999f);
        offsetZ = Random.Range(0, 9999f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
