using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

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
        //NORMAL MOVING
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        }else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }

        //ATTACK
        if(Input.GetButtonDown("Fight")){
            Fight();
        }
        if(Input.GetButtonDown("Kick")){
            Kick();
        }

        //CHANGE PLAYER STATE BY 1 OR 2 OR 3 [FOR DEVELOPERS]
        if(Input.GetKeyDown(KeyCode.F1)){
            resetPlayerState();
            animator.SetBool("isNoneState", true);
        }else if(Input.GetKeyDown(KeyCode.F2)){
            resetPlayerState();
            animator.SetBool("isElementState", true);
        }else if(Input.GetKeyDown(KeyCode.F3)){
            resetPlayerState();
            animator.SetBool("isWeaponState", true);
        }

        //RESET PARAMETER OF ANIMATOR
        setAnimator();
    }

    void FixedUpdate () {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void resetPlayerState(){
        animator.SetBool("isNoneState", false);
        animator.SetBool("isElementState", false);
        animator.SetBool("isWeaponState", false);
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

        //control ATTACK here
        if ((asi.IsName("fight") || asi.IsName("cut_sword")|| asi.IsName("fight_fire")) && asi.normalizedTime >= 1){
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

