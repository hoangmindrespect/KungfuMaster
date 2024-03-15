using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator animator;
    public GameObject enemy;

    private void Start() {
        animator = enemy.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("isAttacking", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isAttacking", false);
    }
}
