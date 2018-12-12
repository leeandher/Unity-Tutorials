using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Timing settings
    public float startWait;
    public float spawnWait;
    public float waveWait;

    // Hazard settings
    public GameObject hazard;
    public int hazardCount;
    public Vector3 spawnValues;

    // GUI
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    private int score;

    // Game tracking
    private bool gameOver;
    private bool restart;
    
    // Initialize parameters
    private void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        UpdateScore();
        StartCoroutine( SpawnWaves() );
    }

    // Check for user input on the 'R' key for restart
    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Allows SpawnWaves to be called repeatedly via a Coroutine
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        // Never stop the waves
        while(true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    // Allow other events to trigger additions in score
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    // Update the text displaying the score
    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Run this function when the game ends
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
