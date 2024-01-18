using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private int atkDamage;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
   void Update()
    {
        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack_1();
            }

            else if (Input.GetKeyDown(KeyCode.X))
            {
                Attack_2();
            }
            
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Attack_3();
            }
        }
        
    }

    void Attack_1()
    {
        animator.SetTrigger("Atk_1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach(Collider2D enemies in hitEnemies)
        {
           // enemies.GetComponent<Enemy>().TakeDamage(atkDamage);
        }
    }

    void Attack_2()
    {
        animator.SetTrigger("Atk_2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach (Collider2D enemies in hitEnemies)
        {
            //enemies.GetComponent<Enemy>().TakeDamage(atkDamage);
        }
    }

    void Attack_3()
    {
        animator.SetTrigger("Atk_3");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach (Collider2D enemies in hitEnemies)
        {
           // enemies.GetComponent<Enemy>().TakeDamage(atkDamage);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, Ground);
    }

}
