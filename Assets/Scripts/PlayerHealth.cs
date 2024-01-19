using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator Animator;
    private int maxHealth = 100;
    private int health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();

        }
    }

    public void TakeDamaage(int damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            Die();
            Debug.Log("hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Die()
    {
        Animator.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        Invoke("GameOver", 3);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
