using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{

    public Animator animator;
    
    public int maxHealth = 100;
    private int currentHealth;

    public Rigidbody2D rb;
    private bool EnemyDead = false;
    private float corpsedecay = 10f;
    
    
    void Start()
    {
        currentHealth = maxHealth;

        
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (EnemyDead == true)
        {
            corpsedecay -= Time.deltaTime;
            print(corpsedecay);
            if (corpsedecay <= 0f)
            {
                Destroy(gameObject);
                // this.enabled = false;
            }
        }
    }

    void Die()
    {


        EnemyDead = true;
        Debug.Log("Enemy died!");
        //Die animation
        animator.SetBool("IsDead", true);


        //Disable the enemy
        
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Seeker>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        
       // this.enabled = false;
        
    }
    
}
