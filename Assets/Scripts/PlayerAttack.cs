using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask EnemyLayer;

    [SerializeField] private int atkDamage;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Player_Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

        foreach(Collider2D enemies in hitEnemies)
        {
            enemies.GetComponent<Enemy>().TakeDamage(atkDamage);
        }
    }
}
