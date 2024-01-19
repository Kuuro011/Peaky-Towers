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
    private int atk1Dmg = 5;
    private int atk2Dmg = 10;
    private int atk3Dmg = 15;
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
           enemies.GetComponent<Enemy_behaviour>().TakeDamage(atk1Dmg);
        }
    }
    
    void Attack_2()
    {
        animator.SetTrigger("Atk_2");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach (Collider2D enemies in hitEnemies)
        {
            enemies.GetComponent<Enemy_behaviour>().TakeDamage(atk2Dmg);
        }
    }

    void Attack_3()
    {
        animator.SetTrigger("Atk_3");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach (Collider2D enemies in hitEnemies)
        {
           enemies.GetComponent<Enemy_behaviour>().TakeDamage(atk3Dmg);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, Ground);
    }

}
