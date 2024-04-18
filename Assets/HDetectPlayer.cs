using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HDetectPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private Animator enemyAnimator;
    private Rigidbody2D playerRigidBody;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemyAnimator.SetBool("isAttack", true);
    }

    void OnTriggerStay2D(Collider2D other){
        enemyAnimator.SetBool("isAttack", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            //enemyAnimator.SetBool("isAttack", false);
        }
    }
    void FixedUpdate()
    {
        AnimatorStateInfo asi = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        if(asi.IsName("attack") && asi.normalizedTime > 0.5f){
            enemyAnimator.SetBool("isAttack", false);
        }
    }
}
