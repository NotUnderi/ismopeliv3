﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{

    public int bulletsPerMag = 31;
    public int bulletsLeft;

    public float fireRate = 0.1f;

    float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }
    private void Fire()
    {
        if (fireTimer < fireRate) return;
        Debug.Log("Fired");

    }
}
