using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public float score;
    public float highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public bool trackScore = false;
    public Transform player;
    public Transform origin;

    public GameObject highScoreObject;

    public float distanceFromOrigin = 0;



    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Level10")
        {
            Debug.Log("This is level 10");
            trackScore = true;
        }
        else
        {
            trackScore = false;
        }

        if (highScoreObject == null)
        {
            highScoreObject = GameObject.Find("HighScore");
        }

        score = 0;
        if (!trackScore)
        {
            scoreText.transform.parent.gameObject.SetActive(false);
        }
        if (trackScore)
        {
            highScore = highScoreObject.GetComponent<HighScore>().highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trackScore)
        {
            updateScore();
            updateScoreDisplay();
        }   
    }

    void updateScore()
    {
        if (player != null)
        {
            distanceFromOrigin = Vector3.Distance(player.position, origin.position);
            score += Time.deltaTime * distanceFromOrigin;
            if (score > highScore)
            {
                highScore = score;
                highScoreObject.GetComponent<HighScore>().highScore = highScore;
            }
        }

    }

    void updateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.Floor(score).ToString();
            highScoreText.text = "High Score: " + Mathf.Floor(highScoreObject.GetComponent<HighScore>().highScore).ToString();
        }

    }

    public void nextLevel()
    {
        Debug.Log("Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restartLevel()
    {
        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level10"))

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void endLevel()
    {
        SceneManager.LoadScene("endLevel");
    }

    public void endlessMode()
    {
        SceneManager.LoadScene("Level10");
    }


}
