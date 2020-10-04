using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnOnFirstBoost : MonoBehaviour
{
    public Transform boosts;
    public GameObject firstText;

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
            if (gameObject.GetComponent<MeshRenderer>().enabled == false)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                transform.position = boosts.GetChild(0).transform.position + new Vector3(1f, 0, 0f);
                firstText.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
