using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Animator Animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trao"))
        {
            Die();
        }
    }

    private void Die()
    {
        Animator.SetTrigger("Death");
    }
}
