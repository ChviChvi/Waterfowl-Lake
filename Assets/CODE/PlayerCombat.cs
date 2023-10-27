using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform AttackPoint;
    public float attackRange = 0.8f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    
    public float attackRate = 6f;
    private float nextAttackTime = 0f;
    
    void Update()
    {
        
        //implement that player doesnt move during attacking
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            } 
            
        }
        //sword attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Sword();
                nextAttackTime = Time.time + 1f / attackRate;
            } 
            
        }
         
    }

    void Attack()
    {
        //attack animation
        animator.SetTrigger("Attack");
        
        //Detecs enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyProperties>().TakeDamage(attackDamage);
        }
    }
    void Sword()
    {
        //attack animation
        animator.SetTrigger("SwordAttack");
        
        //Detecs enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyProperties>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        //throw new NotImplementedException();
    }
}
