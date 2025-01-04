using UnityEngine;
using System.Collections;

public class AttackControl : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Animator animator;
    private float attackCooldown = 0.5f;
    private bool canAttack = true;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            PerformRandomAttack();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void PerformRandomAttack()
    {
        int randomAttack = Random.Range(1, 3);
        if (randomAttack == 0)
        {
            animator.SetBool("isAttack1", true);
        }
        else
        {
            animator.SetBool("isAttack2", true);
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("isAttack1", false);
        animator.SetBool("isAttack2", false);
        canAttack = true;
    }
}
