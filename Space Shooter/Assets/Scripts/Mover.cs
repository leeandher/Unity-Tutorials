using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    private Rigidbody rb;
    public float velocity;

    void Start()
    {
        rb = GetComponent<RigidBody>();
    }
}
