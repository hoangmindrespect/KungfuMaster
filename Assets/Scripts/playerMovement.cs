using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    public float delay = 0.417f;
    private bool attackBlocked;
    bool jump = false;
    bool crouch = false;
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Attack1")){
            Fight1();
        }
        if(Input.GetButtonDown("Attack2")){
            Fight2();
        }
        if(Input.GetButtonDown("Attack3")){
            Kick();
        }
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        }else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }
    }

    void FixedUpdate () {
        // Move our characte
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        Jump();
    }

    public void Fight1(){
        if (attackBlocked)
        return;
        animator.SetTrigger("attack_1");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
     public void Fight2(){
        if (attackBlocked)
        return;
        animator.SetTrigger("attack_2");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    public void Kick(){
        if (attackBlocked)
        return;
        animator.SetTrigger("attack_3");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }
    public void Jump(){
        float velocityY = rb.velocity.y;
        print(velocityY);

        animator.SetFloat("velocity_y", velocityY); 
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}

