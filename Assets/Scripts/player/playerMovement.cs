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
    public GameObject LargeMeteor;
    public GameObject MediumMeteor;
    public GameObject SmallMeteor;
    private AudioManager audioManager;
    private TrailRenderer trailRenderer;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool doubleJump = false;
    bool can_doubleJump = true;
    bool crouch = false;
    private bool canFight = true; // Biến để kiểm tra xem có thể thực hiện Fight() hay không
    private bool canKick = true; // Biến để kiểm tra xem có thể thực hiện Kick() hay không
    private bool isSurfing = false; // Disale tất cả các action khác
    private int isFacingRight = 1;
    private float lastFightTime; // Thời gian cuối cùng khi thực hiện Fight()
    private float lastKickTime; // Thời gian cuối cùng khi thực hiện Kick()
    public float cooldown = 0.5f; // Thời gian cooldown giữa các lần nhấn
    private float theLastLocation; // vị trí trước khi surf


    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update () {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);

        //NORMAL MOVING AND SURFING
        if(!isSurfing){
            if(Input.GetButtonDown("Crouch")){
                trailRenderer.enabled = true;
                animator.SetBool("isSurfing", true);
                isSurfing = true;
                theLastLocation = rb.transform.position.x;
                if(rb.velocity.x >= 0f){
                    isFacingRight = 1;
                }else{
                    isFacingRight = -1;
                }
            }
            
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if(Input.GetButtonDown("Jump")){
                if(can_doubleJump && !doubleJump && rb.velocity.y != 0f){
                    doubleJump = true;
                    can_doubleJump = false;
                }

                jump = true;
            }

            //ATTACK
            if(canFight && Input.GetButtonDown("Skill1")){
                Fight();
            }
            if(canKick && Input.GetButtonDown("Skill2")){
                Kick();
            }
            if(Input.GetButtonDown("Skill3")){
                Skill3();
            }
        }

        if(isSurfing){
            horizontalMove = 0.0f;
            if(Math.Abs(rb.transform.position.x - theLastLocation) > 15f){
                animator.SetBool("isSurfing", false);
                isSurfing = false;
                trailRenderer.enabled = false;
            }
        }

        ////CHANGE PLAYER STATE BY 1 OR 2 OR 3 [FOR DEVELOPERS]
        //if(Input.GetKeyDown(KeyCode.F1)){
        //    resetPlayerState();
        //    animator.SetBool("isNoneState", true);
        //}else if(Input.GetKeyDown(KeyCode.F2)){
        //    resetPlayerState();
        //    animator.SetBool("isElementState", true);
        //}else if(Input.GetKeyDown(KeyCode.F3)){
        //    resetPlayerState();
        //    animator.SetBool("isWeaponState", true);
        //}

        //RESET PARAMETER OF ANIMATOR
        setAnimator();
    }

    void FixedUpdate () {
        if(!isSurfing){
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, doubleJump);
            jump = false; 
            doubleJump = false;
            if(controller.getM_Ground() == true){
                can_doubleJump = true;
            }
        }else{
            rb.transform.position = new Vector2(rb.position.x + isFacingRight* 60f * Time.fixedDeltaTime, rb.position.y);
        }
    }

    public void resetPlayerState(){
        animator.SetBool("isNoneState", false);
        animator.SetBool("isElementState", false);
        animator.SetBool("isWeaponState", false);
    }
    public void setAnimator(){
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
        if ((asi.IsName("kick") || asi.IsName("kick_fire")|| asi.IsName("cut2_sword")) && asi.normalizedTime >= 1){
            animator.SetBool("isKicking", false);
		}
    }

    //S1
    private void Fight()
    {
        // Kiểm tra thời gian cooldown
        if (Time.time - lastFightTime > cooldown)
        {
            lastFightTime = Time.time; // Cập nhật thời gian cuối cùng khi thực hiện Fight()
            animator.SetBool("isFighting", true);
            audioManager.PlaySFX(audioManager.playerFight);
        }
    }

    private void Kick()
    {
        // Kiểm tra thời gian cooldown
        if (Time.time - lastKickTime > cooldown)
        {
            lastKickTime = Time.time; // Cập nhật thời gian cuối cùng khi thực hiện Kick()
            animator.SetBool("isKicking", true);
            audioManager.PlaySFX(audioManager.playerKick);
        }
    }
    private void Skill3(){
        //control S3 in element state
        if(animator.GetBool("isElementState")){
            Instantiate(LargeMeteor, new Vector3(transform.position.x -20f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(MediumMeteor, new Vector3(transform.position.x - 15f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(SmallMeteor, new Vector3(transform.position.x - 12f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(LargeMeteor, new Vector3(transform.position.x - 8f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(MediumMeteor, new Vector3(transform.position.x - 5f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(LargeMeteor, new Vector3(transform.position.x , transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(SmallMeteor, new Vector3(transform.position.x + 2f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(LargeMeteor, new Vector3(transform.position.x + 5f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(SmallMeteor, new Vector3(transform.position.x + 8f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(MediumMeteor, new Vector3(transform.position.x + 11f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(MediumMeteor, new Vector3(transform.position.x + 14f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(LargeMeteor, new Vector3(transform.position.x + 17f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(LargeMeteor, new Vector3(transform.position.x + 20f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(MediumMeteor, new Vector3(transform.position.x + 23f, transform.position.y + 10f, 0f), Quaternion.identity);
            Instantiate(SmallMeteor, new Vector3(transform.position.x + 26f, transform.position.y + 10f, 0f), Quaternion.identity);
        }
    }

}

