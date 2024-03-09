using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] public CharacterController2D controller;
    [SerializeField] public Animator animator;
    [SerializeField]  public Rigidbody2D rb;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update () {
        //Normal moving and control Axis
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        }else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }

        //Control attack
        if(Input.GetButtonDown("Fight")){
            Fight();
        }
        if(Input.GetButtonDown("Kick")){
            Kick();
        }
        setAnimator();
    }

    void FixedUpdate () {
        // Move our characte
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }



    private void setAnimator(){
         AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);

        //set isMoving
        if(horizontalMove != 0.0f){
            animator.SetBool("isMoving", true);
        }
        else{
            animator.SetBool("isMoving", false);
        }

        //set isJumping
        float vy = rb.velocity.y;
        if(vy >= 0.1f){
            animator.SetBool("isJumping", true);
        }else if(vy <= -0.1f){
            animator.SetBool("isLanding", true);
            animator.SetBool("isJumping", false);
        }else{
            animator.SetBool("isJumping", false);
            animator.SetBool("isLanding", false);
        }

        //control attack
        if ((asi.IsName("fight") || asi.IsName("cut_sword")) && asi.normalizedTime >= 1){
            animator.SetBool("isFighting", false);
		}
        if (asi.IsName("kick") && asi.normalizedTime >= 1){
            animator.SetBool("isKicking", false);
		}
    }

    private void Fight(){
        animator.SetBool("isFighting", true);
    }
    private void Kick(){
        animator.SetBool("isKicking", true);
    }

}

