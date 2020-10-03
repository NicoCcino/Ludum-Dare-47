using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnOnFirstBoost : MonoBehaviour
{
    public Transform boosts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boosts.childCount <= 0)
        {

        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            transform.position = boosts.GetChild(0).transform.position - new Vector3(0f, 0, 10f);
        }
    }
}
