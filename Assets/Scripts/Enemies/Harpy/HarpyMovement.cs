using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpyMovement : MonoBehaviour
{

     private Animator animator;
     public  GameObject AI;
     private Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if(asi.IsName("die") && asi.normalizedTime > 0.8f){
            GetComponent<Health>().Destroy();
            Destroy(AI);
        }else if(asi.IsName("hurt")){
            if(asi.normalizedTime > 0.7f){
                animator.SetBool("isDamaged", false);
            }
        }
    }
}
