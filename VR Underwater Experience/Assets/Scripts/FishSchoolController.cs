﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolController : MonoBehaviour {

    // number of fish
    public int numFish;

    // if we get closer to this position, create new fish
    public float activationDistance;

    // if we get further than this position, destroy all fish
    public float deactivationDistance;

    // fish prefab
    public GameObject fishPrefab;

    // keep track of whether the school is active or not
    bool isActivated = false;

    // keep all the fish
    List<GameObject> fishes;

    // Use this for initialization
    void Start () {
        //init our list of fish
        fishes = new List<GameObject>();

        //check player prox every second
        InvokeRepeating("CheckPlayerProximity", Time.time, 1 + UnityEngine.Random.Range(-0.1f, 0.1f));
    }

    void CheckPlayerProximity()
    {
        //location of the player
        Vector3 playerPos = Camera.main.transform.position;

        //check distance
        if (!isActivated && Vector3.Distance(playerPos, transform.position) < activationDistance)
        {
            isActivated = true;
            SpawnFish();
        }
        else if(isActivated && Vector3.Distance(playerPos, transform.position) > deactivationDistance)
        {
            isActivated = false;
            DestroyFish();
        }
    }

    void DestroyFish()
    {
        for (int i = 0; i < fishes.Count; i++)
        {
            Destroy(fishes[i]);    
        }
        fishes = new List<GameObject>();

    }

    void SpawnFish()
    {
        // number of fish
        int num = numFish + (int)UnityEngine.Random.Range(0, 5);

        // scale
        float scale = UnityEngine.Random.Range(0.2f, 0.5f);

        // rotation
        Vector3 euler = Vector3.zero;
        euler.x = UnityEngine.Random.Range(-20, 20);
        euler.y = UnityEngine.Random.Range(-180, 180);

        for(int i = 0; i < num; i++)
        {
            // random position, no further than 5 from the school of fish object
            float x = transform.position.x + UnityEngine.Random.Range(-5, 5);
            float y = transform.position.y + UnityEngine.Random.Range(-5, 5);
            float z = transform.position.z + UnityEngine.Random.Range(-5, 5);

            //create new fish
            GameObject newFish = Instantiate(fishPrefab, transform);

            //set position
            newFish.transform.position = new Vector3(x, y, z);

            //set rotation
            newFish.transform.eulerAngles = euler;

            //set scale
            newFish.transform.localScale = scale * Vector3.one;

            //add fish to the list
            fishes.Add(newFish);
        }

    }
}
