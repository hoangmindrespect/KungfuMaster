using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDeathInteraction : MonoBehaviour
{
    public GameObject enemy;
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
        //đảm bảo là CD chỉ nói gì đó 1 lần.
        if(!isEnterCollide){
            audioManager.PlaySFX(GetRandomCrowDeathSound());
            enemyAnimator.SetBool("isPlayerDetected", true);
            //Start
            isEnterCollide = true;
            isExitCollide = false;
        }
    }

    IEnumerable Delay(){
        yield return new WaitForSeconds(0.5f);
    }

    AudioClip GetRandomCrowDeathSound()
    {
        int randomIndex = Random.Range(1, 3); 
        switch (randomIndex) {
            case 1: return audioManager.crowdeathDetect1;
            case 2: return audioManager.crowdeathDetect2;
            case 3: return audioManager.crowdeathDetect3;
            default: return audioManager.crowdeathDetect1;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(!isExitCollide){
            enemyAnimator.SetBool("isPlayerDetected", false);
            enemyAnimator.SetBool("isRunning", false);
            isEnterCollide = false;
            isExitCollide = true;
        }
    }
    void FixedUpdate()
    {
        AnimatorStateInfo asi = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        float currentSpeed = enemyRigidBody.velocity.x;

        if(asi.IsName("run")){
            if(player.GetComponent<Animator>().GetBool("isMoving")){
                if((enemyRigidBody.position.x < playerRigidBody.position.x && currentSpeed < 0.2f) || (enemyRigidBody.position.x >= playerRigidBody.position.x && currentSpeed > 0.2f) ){
                    cm.Flip();
                }
            }
        }

        if(asi.IsName("discovery") && asi.normalizedTime > 1){
            enemyAnimator.SetBool("isRunning", true);
            enemyRigidBody.velocity = new Vector2(currentSpeed * 1.5f, 0.0f);
        }
    }
}
