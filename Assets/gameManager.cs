using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public float score;
    public TextMeshProUGUI scoreText;
    public Transform player;
    public Transform origin;


    public float distanceFromOrigin = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
        updateScoreDisplay();
    }

    void updateScore()
    {
        distanceFromOrigin = Vector3.Distance(player.position, origin.position);
        score += Time.deltaTime * distanceFromOrigin;
    }

    void updateScoreDisplay()
    {
        scoreText.text = "Score: "+Mathf.Floor(score).ToString();
    }

    public void nextLevel()
    {
        Debug.Log("Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
