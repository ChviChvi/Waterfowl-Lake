using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyAI : MonoBehaviour
{
    //private IAstarAI ai;
    public float radius;
    
    //refference to target(takes in target)
    public Transform target;

    //speed controller
    public float speed = 250f;
    //stores waypoint distance, how close the enemy needs to be to a waypoint before it moves to the next one.
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;
    
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Collider2D c2d;

    private Vector3 Enemyplace;
    
    public float allowed_distance = 5f;
   
    void Start()
    {
        //finds the components of our object.
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        c2d = GetComponent<Collider2D>();

        //ai = GetComponent<IAstarAI>();

        //keeps invoking a method
        
        InvokeRepeating("UpdatePath",0f,0.2f);
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        

    }

    void Update()
    {
         Enemyplace = rb.position;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate() {

        
        if (path == null) {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        //gets the direction (gives position of current waypoint)
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        float distance2 = Vector2.Distance(rb.position, target.position);
        
        if(distance2 > allowed_distance)
        {
            rb.position = Enemyplace;
            return;
        }
        
        rb.AddForce(force);


        
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        print("distance: " + distance2);
        
         if (distance < nextWaypointDistance)
         {
             currentWaypoint++;
         }

        
        //changes which way the slime is looking
        // if (force.x >= 0.01f)
        // {
        //     enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        // } else if(force.x <= 0.01f)
        //
        // {
        //     enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        // }
        

    }
    
    private bool OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
