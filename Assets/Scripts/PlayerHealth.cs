using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action OnPlayerDeath;
    private Rigidbody2D rb;
    private Animator Animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        Animator.SetTrigger("Death");
        rb.bodyType = RigidbodyType2D.Static;
        OnPlayerDeath.Invoke();
    }


}
