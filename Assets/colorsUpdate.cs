using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorsUpdate : MonoBehaviour
{
    public Color newColor;
    public Color initialColor;
    public bool colorIncreasing = false;
    public float colorSpeed;

    public float deltaTime;
    public float timeToSwitch;
    private int i;


    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        newColor = new Color(0, 0, 0);
        initialColor = new Color(0, 0, 0);
        deltaTime = 0;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Mathf.Min(timeToSwitch, deltaTime+Time.deltaTime);

        if (transform.childCount > 0)
        {
            //if (newColor == initialColor)
            //{
            //    GetFirstColor();
            //}
            
            UpdateColors();
        }

        if (deltaTime >= timeToSwitch)
        {
            deltaTime = 0;
        }
    }

    void GetFirstColor()
    {
        float randValue = Mathf.FloorToInt(Random.Range(0f, 256f));

        // Set one to 255
        // Set one to 0
        // Last one is random
        float r = 0f;
        float g = 0f;
        float b = 0f;
        float randParam = Mathf.FloorToInt(Random.Range(0f, 3f));
        if (randParam == 0)
        {
            r = 255f;
            randParam = Mathf.FloorToInt(Random.Range(0f, 2f));
            if (randParam == 0)
            {
                g = randValue;
            }
            if (randParam == 1)
            {
                b = randValue;
            }
        }
        if (randParam == 1)
        {
            g = 255f;
            randParam = Mathf.FloorToInt(Random.Range(0f, 2f));
            if (randParam == 0)
            {
                r = randValue;
            }
            if (randParam == 1)
            {
                b = randValue;
            }
        }
        if (randParam == 2)
        {
            b = 255f;
            if (randParam == 0)
            {
                r = randValue;
            }
            if (randParam == 1)
            {
                g = randValue;
            }
        }

        newColor = new Color32((byte)r, (byte)g, (byte)b,1);
    }

    void GetNewColor()
    {
        float r = newColor.r;
        float g = newColor.g;
        float b = newColor.b;


        if ((i + 1) > colors.Length)
        {
            i = 0;
        }

        if ((i+1) >= colors.Length)
        {
            newColor = Color.Lerp(colors[0 + i], colors[0], deltaTime / timeToSwitch);
        }
        else
        {
            newColor = Color.Lerp(colors[0 + i], colors[1 + i], deltaTime / timeToSwitch);
        }

        if (deltaTime >= timeToSwitch){
            i++;
        }

        //if (r == 255f)
        //{
        //    if (b == 0f)
        //    {
        //        colorIncreasing = true;
        //    }
        //    if (g == 255f)
        //    {
        //        r--;
        //        colorIncreasing = false;
        //    }
        //    if (colorIncreasing)
        //    {
        //        g++;
        //    }
        //    if (!colorIncreasing)
        //    {
        //        b--;
        //    }
        //}
        //else if (g == 255f)
        //{
        //    if (r == 0f)
        //    {
        //        colorIncreasing = true;
        //    }
        //    if (b == 255f)
        //    {
        //        g--;
        //        colorIncreasing = false;
        //    }
        //    if (colorIncreasing)
        //    {
        //        b++;
        //    }
        //    if (!colorIncreasing)
        //    {
        //        r--;
        //    }
        //}
        //else if (b == 255f)
        //{
        //    if (g == 0f)
        //    {
        //        colorIncreasing = true;
        //    }
        //    if (r == 255f)
        //    {
        //        b--;
        //        colorIncreasing = false;
        //    }
        //    if (colorIncreasing)
        //    {
        //        r++;
        //    }
        //    if (!colorIncreasing)
        //    {
        //        g--;
        //    }
        //}

        // newColor = new Color32((byte)r, (byte)g, (byte)b, 1);
    }

    void UpdateColors()
    {
        GetNewColor();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform wall = transform.GetChild(i);
            wall.GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
            wall.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        }
    }
}
