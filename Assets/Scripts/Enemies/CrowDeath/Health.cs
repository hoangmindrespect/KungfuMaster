using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    public bool isSameDirection = false;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool IsWillBeDie(int attdame){
        if(maxHP - attdame < 0){
        {
            enemy.GetComponent<Animator>().SetBool("isDied", true);
            maxHP -= attdame;
            return true;
        }
        }else
            return false;
    }
    public void TackDamage(int attdame){
        maxHP -= attdame;
        if(enemy.CompareTag("Enemy")){
            if(enemy.GetComponent<Rigidbody2D>().velocity.x * player.GetComponent<Rigidbody2D>().velocity.x > 0){
            isSameDirection = true;
            enemy.GetComponent<Rigidbody2D>().velocity = new(enemy.GetComponent<Rigidbody2D>().velocity.x * 2, 0.0f);
        }
        else{
            isSameDirection = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new(-enemy.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
        }
        }
    }

    public void Destroy(){
        Destroy(enemy);
    }
    public void ResetBool(){
        isSameDirection = false;
    }
}
