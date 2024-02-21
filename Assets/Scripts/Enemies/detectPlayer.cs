using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("isAttack", true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        animator.SetBool("isAttack", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isAttack", false);
    }

}
