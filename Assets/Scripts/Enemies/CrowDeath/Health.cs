using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IEnemy
{
    [SerializeField] private int maxHP = 5;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    public bool isSameDirection = false;

    public int ID { get ; set ; }
    public int Experience { get; set; }

    public Slider healthBar;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Experience = 15;
    }

    private void Update()
    {
        if(healthBar != null)
            healthBar.value = (float) maxHP/100;
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
        CombatEvents.EnemyDied(this);
        Destroy(enemy);
        ScoreManager.instance.AddPoint(50); // 50 is point value only for CrowDeath, another enemy has different point,
                                            // I can use delegate for this later to make sure Open/Closed Responsibility.
    }
    public void ResetBool(){
        isSameDirection = false;
    }
}
