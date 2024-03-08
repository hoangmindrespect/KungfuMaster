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
        if(Input.GetButtonDown("Attack2")){
            //Fight2();
        }
        if(Input.GetButtonDown("Attack3")){
           //Kick();
        }
        setAnimator();
    }

    void FixedUpdate () {
        // Move our characte
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }



    private void setAnimator(){

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

    }
    // private IEnumerator DelayAttack()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     attackBlocked = false;
    // }
    // private IEnumerator DelayAttack_0(float timDelay, GameObject skill, float xpos, float ypos)
    // {     
    //     yield return new WaitForSeconds(timDelay);
    //     Vector3 theScale = skill.transform.localScale;
        
    //     // Player is facing right
    //     if(controller.isFaceRight){
    //         if(theScale.x < 0){
    //             theScale.x *= -1;
    //             skill.transform.localScale = theScale;
    //         }
    //         preSkill = Instantiate(skill, new Vector3(transform.position.x + xpos, transform.position.y + ypos, 0f), transform.rotation);
    //     }else{
    //         if(theScale.x > 0){
    //             theScale.x *= -1;
    //             skill.transform.localScale = theScale;
    //         }  
    //         preSkill = Instantiate(skill, new Vector3(transform.position.x - xpos, transform.position.y + ypos, 0f), transform.rotation);
    //     }
    // }
    // private IEnumerator DelayAttack_1(float timDelay, GameObject skill)
    // {
    //     yield return new WaitForSeconds(timDelay);
    //     if(skill != null){
    //         resSkill = Instantiate(skill, new Vector3(preSkill.transform.position.x, preSkill.transform.position.y - 0.2f, 0f), transform.rotation);
    //     }
    //     else{
    //         attackBlocked = false;
    //     }
    //     Destroy(preSkill);
    //     cantMove = false;
    // }
    // private IEnumerator DelayAttack_2(float timDelay)
    // {
    //     yield return new WaitForSeconds(timDelay);
    //     Destroy(resSkill);
    //     attackBlocked = false;

    // }
}

