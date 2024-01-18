using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("isHurt");
        Debug.Log("HIt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("isDead", true);
        Debug.Log("Dead");
    }
}
