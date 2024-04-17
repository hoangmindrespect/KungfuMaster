using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private int maxHP = 50;
    [SerializeField] private int DPS = 2;
    private AudioManager audioManager;
    private Animator animator;
    private Rigidbody2D rb;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    public void TackDamage(){
        if(maxHP > 1){
            maxHP--;
            animator.SetBool("isDamaged", false);
            Debug.Log(maxHP);
        }else{
            Debug.Log(maxHP);
            rb.velocity = new Vector2(0.0f,0.0f);
            animator.SetBool("isDied", true);
            audioManager.PlaySFX(audioManager.crowdeathDeath);
        }
    }
}
