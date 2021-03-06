﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Boundary Area
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    // Components
    private Rigidbody rb;
    private AudioSource audio;

    // Other game objects
    public GameObject shot;
    public Transform shotSpawn;

    // Configurable Variables
    public float speed;
    public float tilt;
    public Boundary boundary;
    public float fireRate;

    private float nextFire;

    private void Start()
    {
        // Get the rigidbody component
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            audio.Play();
        }
    }

    private void FixedUpdate()
    {
        // Accept the user input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Allow input to move the ship via velocity
        rb.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;

        //Ensure the ship cannot leave the play area
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        // Rotate the ship when turning
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
