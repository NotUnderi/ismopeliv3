using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent enemy;
    Transform player;
    Vector3 heading;
    public PlayerDie playerDie;

    float enemyDamage = 15f;
    float damageRate = 2f;
    float lastHit;

    // Start is called before the first frame update
    public void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        heading = enemy.destination;
        player = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - lastHit >= damageRate)
        {
            if(enemy.hasPath && enemy.remainingDistance <= enemy.stoppingDistance)
            {
                playerDie.health -= enemyDamage;
                lastHit = Time.time;
            }
        }
        if (Vector3.Distance(player.position, heading) > 1.0f)
        {
            heading = player.position;
            enemy.destination = heading;
        }

       // Debug.Log("Remaining distance : " + enemy.remainingDistance.ToString() + " | Heading : " + heading.ToString() + " | Player position : " + player.position.ToString());
    }
}
