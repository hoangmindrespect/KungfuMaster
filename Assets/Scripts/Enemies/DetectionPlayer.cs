using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CrowDeathInteraction : MonoBehaviour
{
    [Header("----------Crow Death----------")]
    public GameObject enemy;

    [Header("----------Player----------")]
    public GameObject player;

    private CrowDeathMovement cm;
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody;
    private Rigidbody2D playerRigidBody;
    private bool isEnterCollide;
    private bool isExitCollide;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        cm = enemy.GetComponent<CrowDeathMovement>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isEnterCollide){
            audioManager.PlaySFX(audioManager.crowdeathDetect);
            enemyAnimator.SetBool("isPlayerDetected", true);
            isEnterCollide = true;
            isExitCollide = false;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(!isExitCollide){
            enemyAnimator.SetBool("isPlayerDetected", false);
            enemyAnimator.SetBool("isRunning", false);
            isEnterCollide = false;
            isExitCollide = true;
            cm.Flip();
        }
    }
    void FixedUpdate()
    {
        AnimatorStateInfo asi = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        float currentSpeed = enemyRigidBody.velocity.x;

        if(asi.IsName("run")){
            if((enemyRigidBody.position.x < playerRigidBody.position.x && currentSpeed < 0) || (enemyRigidBody.position.x >= playerRigidBody.position.x && currentSpeed > 0) ){
                cm.Flip();
            }
        }

        if(asi.IsName("discovery") && asi.normalizedTime > 1){
            enemyAnimator.SetBool("isRunning", true);
            enemyRigidBody.velocity = new Vector2(currentSpeed * 1.5f, 0.0f);
        }
    }
}
