using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaCheck : MonoBehaviour
{
    private Enemy_behaviour enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponent<Enemy_behaviour>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.TriggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();


        }
    }
}
