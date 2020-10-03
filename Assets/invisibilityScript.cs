﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invisibilityScript : MonoBehaviour
{
    public float invisibilityAmount = 0;
    public float replenishSpeed = 0.1f;
    public float depletionSpeed = 0.1f;
    public GameObject invisibilityBarRectTransform;
    public Color fullColor;
    public Color emptyColor;
    public GameObject player;
    public bool invisibilityActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        emptyColor = invisibilityBarRectTransform.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {

        if (!invisibilityActive)
        {
            invisibilityAmount = Mathf.Min(1f, invisibilityAmount + replenishSpeed * Time.deltaTime);
            invisibilityBarRectTransform.transform.localScale = new Vector3(invisibilityAmount, invisibilityBarRectTransform.transform.localScale.y, invisibilityBarRectTransform.transform.localScale.z);
        }
        else
        {
            invisibilityAmount = Mathf.Max(0f, invisibilityAmount - depletionSpeed * Time.deltaTime);
            invisibilityBarRectTransform.transform.localScale = new Vector3(invisibilityAmount, invisibilityBarRectTransform.transform.localScale.y, invisibilityBarRectTransform.transform.localScale.z);
        }


        if (invisibilityAmount == 1)
        {
            // change color
            invisibilityBarRectTransform.GetComponent<Image>().color = fullColor;
        }
        else
        {
            // change color
            invisibilityBarRectTransform.GetComponent<Image>().color = emptyColor;
        }
        if (invisibilityAmount == 0)
        {
            ToggleInvisibility();
        }

        if (Input.GetKeyDown("space") && invisibilityAmount == 1 && !invisibilityActive)
        {
            Debug.Log("Toggle invisibility");
            ToggleInvisibility();
        }
        if (Input.GetKeyDown("space") && invisibilityAmount != 1 && invisibilityActive)
        {
            Debug.Log("Toggle invisibility");
            ToggleInvisibility();
        }



    }

    public void ToggleInvisibility()
    {
        if (!invisibilityActive)
        {
            invisibilityActive = true;
            Color color = player.GetComponent<Renderer>().material.color;
            color.a = 0.1f; //  transparent
            player.GetComponent<Renderer>().material.color = color;
            player.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            invisibilityActive = false;
            Color color = player.GetComponent<Renderer>().material.color;
            color.a = 1f; //  opaque
            player.GetComponent<Renderer>().material.color = color;
            player.GetComponent<SphereCollider>().enabled = true;
        }
        
    }
}
