using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private int score;

    private void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine( SpawnWaves() );
    }

    // Allows SpawnWaves to be called repeatedly via a Coroutine
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        // Never stop the waves;
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
}
