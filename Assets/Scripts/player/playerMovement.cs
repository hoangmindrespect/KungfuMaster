using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameObject FireAttack;
    public GameObject FirePunch;
    public GameObject FireBomb;
    public Animator animator;
    public Rigidbody2D rb;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    public float delay = 2.0f;
    private bool attackBlocked ;
    bool jump = false;
    bool crouch = false;
    GameObject preSkill;
    GameObject resSkill;
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
        StartCoroutine(DelayAttack_0(0.25f, FireAttack, 3.7f, -0.4f));
        StartCoroutine(DelayAttack_1(1.1f, null));
    }
     public void Fight2(){
        if (attackBlocked)
        return;
        animator.SetTrigger("attack_2");
        attackBlocked = true;
        StartCoroutine(DelayAttack_0(0.22f, FirePunch, -0.1f, -0.5f));
        StartCoroutine(DelayAttack_1(1.0f, FireBomb));
        StartCoroutine(DelayAttack_2(2f));
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
        animator.SetFloat("velocity_y", velocityY); 
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.5f);
        attackBlocked = false;
    }
    private IEnumerator DelayAttack_0(float timDelay, GameObject skill, float xpos, float ypos)
    {     
        yield return new WaitForSeconds(timDelay);
        Vector3 theScale = skill.transform.localScale;
        
        // Player is facing right
        if(controller.isFaceRight){
            print("Face right:");
            print(theScale.x);
            if(theScale.x < 0){
                theScale.x *= -1;
                skill.transform.localScale = theScale;
            }
            preSkill = Instantiate(skill, new Vector3(transform.position.x + xpos, transform.position.y + ypos, 0f), transform.rotation);
        }else{
            if(theScale.x > 0){
                theScale.x *= -1;
                skill.transform.localScale = theScale;
            }  
            print("Face Left");
            print(theScale.x); 
            preSkill = Instantiate(skill, new Vector3(transform.position.x - xpos, transform.position.y + ypos, 0f), transform.rotation);
        }
    }
    private IEnumerator DelayAttack_1(float timDelay, GameObject skill)
    {
        yield return new WaitForSeconds(timDelay);
        if(skill != null){
            resSkill = Instantiate(skill, new Vector3(preSkill.transform.position.x, preSkill.transform.position.y - 0.2f, 0f), transform.rotation);
        }
        else{
            attackBlocked = false;
        }
        Destroy(preSkill);
    }
    private IEnumerator DelayAttack_2(float timDelay)
    {
        yield return new WaitForSeconds(timDelay);
        Destroy(resSkill);
        attackBlocked = false;

    }
}

