using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeScript : MonoBehaviour
{

    public float distanceToFade;
    public Transform player;
    public Transform origin;
    public Image white;
    public float fadeSpeed = 0.005f;
    public gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(player.position, origin.position));
        if (Vector3.Distance(player.position,origin.position) >= distanceToFade)
        {
            Fade();
        }
        else
        {
            UnFade();
        }


    }

    void Fade()
    {
        white.color = white.color + new Color(0, 0, 0, fadeSpeed);
        if (white.color.a >= 1f)
        {
            gm.endLevel();
        }
    }

    void UnFade()
    {
        white.color = white.color - new Color(0, 0, 0, fadeSpeed);
    }
}
