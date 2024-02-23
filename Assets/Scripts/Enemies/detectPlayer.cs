using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    public Animator animator;

    void OnTriggerStay2D(Collider2D other)
    {
        if(!animator.GetBool("isDamaged")){
            animator.SetBool("isAttack", true);
        }else{
            animator.SetBool("isAttack", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isAttack", false);
    }

}
