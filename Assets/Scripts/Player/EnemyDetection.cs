using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody2D;
    private Animator playerAnimator;
    private GameObject enemy;
    public GameObject Player;
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        enemyAnimator = other.GetComponent<Animator>();
        playerAnimator = Player.GetComponent<Animator>();
        enemyRigidBody2D = other.GetComponent<Rigidbody2D>();
        enemy = other.gameObject;
        
        if(playerAnimator.GetBool("isKicking") || playerAnimator.GetBool("isFighting")){
           enemyAnimator.SetBool("isDamaged", true);
        }
    }

    void FixedUpdate()
    {
        if(enemyAnimator != null){
            AnimatorStateInfo asi = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            if(asi.IsName("damage") && asi.normalizedTime > 1){
                enemyAnimator.SetBool("isDied", true);
                enemyRigidBody2D.velocity = new Vector2(0.0f,0.0f);
                audioManager.PlaySFX(audioManager.crowdeathDeath);
            }else if(asi.IsName("die") && asi.normalizedTime > 1){
                Destroy(enemy);
            }
        }
    }
    private void EnemyYell(){
        audioManager.PlaySFX(audioManager.crowdeathDeath);
    }
}
