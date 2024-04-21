using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    private Animator animator;
    private Rigidbody2D rb;
    public bool isSameDirection = false;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public bool IsWillBeDie(int attdame){
        if(maxHP - attdame < 0){
        {
            animator.SetBool("isDied", true);
            maxHP -= attdame;
            return true;
        }
        }else
            return false;
    }
    public void TackDamage(int attdame){
        maxHP -= attdame;
        if(rb.velocity.x * player.GetComponent<Rigidbody2D>().velocity.x > 0){
            isSameDirection = true;
            rb.velocity = new(rb.velocity.x * 2, 0.0f);
        }
        else{
            isSameDirection = false;
            rb.velocity = new(-rb.velocity.x, 0.0f);
        }
    }

    public void Destroy(){
        Destroy(enemy);
    }
    public void ResetBool(){
        isSameDirection = false;
    }
}
