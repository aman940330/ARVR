using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    // fish speed
    public float speed;

    // fleeing speed
    public float fleeSpeed;

    // if we get closer to this position, the fish will run
    public float threshold;

    // velocity
    protected Vector3 velocity;

	// Use this for initialization
	void Start () {
        //velocity vector
        velocity = transform.forward * speed;

        //setup the player proximity check
        //random number added is so not ALL the fish execute this at the exact same time --> too much CPU usage at the same time
        //(optimization technique)
        InvokeRepeating("CheckPlayerProximity", Time.time, 1 + UnityEngine.Random.Range(-0.1f, 0.1f));

    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        
        // v = d / t => d = v * t
        Vector3 movement = Time.fixedDeltaTime * velocity;

        // make it move
        transform.position += movement;
	}

    protected virtual void OnTriggerEnter(Collider other)
    {
        // only if it's not a fish
        if (!other.CompareTag("Fish"))
        {
            // reverse direction
            Reverse();
        }
    }

    void Reverse()
    {
        // reverse direction
        transform.forward *= -1;
        velocity *= -1;
    }

    void CheckPlayerProximity()
    {
        //location of the player
        Vector3 playerPos = Camera.main.transform.position;

        //check distance
        if(Vector3.Distance(playerPos, transform.position) < threshold)
        {
            HandlePlayerNearby();
        }        
    }

    protected virtual void HandlePlayerNearby()
    {
        // generate a random angle
        Vector3 currEuler = transform.eulerAngles;

        currEuler.x = UnityEngine.Random.Range(-20, 20);
        currEuler.y = currEuler.y * UnityEngine.Random.Range(-30, 30);
        
        // rotate the fish to the new angle
        transform.eulerAngles = currEuler;

        // set new velocity (flee speed)
        velocity = transform.forward * fleeSpeed;
    }
}
