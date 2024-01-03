using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private float speed = 2;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
        
    }
}
