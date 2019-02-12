using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharkController : FishController {

    bool isChasing = false;

	protected override void HandlePlayerNearby()
    {
        //set chasing flag so we start chasing the player
        isChasing = true;

        //get animator component
        Animator anim = GetComponent<Animator>();

        //set the parameter
        anim.SetBool("sawPlayer", true);
    }

    protected override void FixedUpdate()
    {
        // if we are chasing, look at the player
        if(isChasing)
        {
            transform.LookAt(Camera.main.transform.position);

            // direction of the velocity
            Vector3 dir = Camera.main.transform.position - transform.position;

            //update the velocity vector = normalized direction * speed
            velocity = dir.normalized * speed;
        }

        // call the parent's method
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // call the parent's method
        base.OnTriggerEnter(other);

        if (other.CompareTag("Player"))
        {
            //restart scene
            SceneManager.LoadScene("Game");
        }
    }
}
