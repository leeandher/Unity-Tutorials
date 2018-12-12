using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.LogError("Cannot find 'GameController' script!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") { return; }
        // Note: Objects are marked to be destroyed, not actually destroyed
        // therefore order does not matter
        Destroy(other.gameObject);
        Destroy(gameObject);

        // Cause an explosion
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
    }
}
