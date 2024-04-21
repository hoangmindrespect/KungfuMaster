using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    public GameObject enemy;
    private AudioManager audioManager;
    private Animator animator;
    private Rigidbody2D rb;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
    }

    public void Destroy(){
        Destroy(enemy);
    }
}
