using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonHealth : EnemyHealth, IEnemy
{
    private int S_HP = 10;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    public Slider healthBar;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Experience = 15;
        healthBar.value = S_HP;
        maxHP = S_HP;
        ID = 2; // ID of skeleton is 2
    }

    public override bool IsWillBeDie(int attdame)
    {
        if (maxHP - attdame < 0)
        {
            {
                enemy.GetComponent<Animator>().SetBool("isDied", true);
                maxHP -= attdame;
                return true;
            }
        }
        else
            return false;
    }
    public override void TackDamage(int attdame)
    {
        maxHP -= attdame;
        healthBar.value = maxHP;
        if (enemy.CompareTag("Enemy"))
        {
            if (enemy.GetComponent<Rigidbody2D>().velocity.x * player.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                isSameDirection = true;
                enemy.GetComponent<Rigidbody2D>().velocity = new(enemy.GetComponent<Rigidbody2D>().velocity.x * 2, 0.0f);
            }
            else
            {
                isSameDirection = false;
                enemy.GetComponent<Rigidbody2D>().velocity = new(-enemy.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            }
        }
    }

    public override void Destroy()
    {
        CombatEvents.EnemyDied(this);
        Destroy(enemy);
        ScoreManager.instance.AddPoint(100); // 50 is point value only for CrowDeath, another enemy has different point,
                                             // I can use delegate for this later to make sure Open/Closed Responsibility.
    }
}
