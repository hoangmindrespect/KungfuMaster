using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDHealth : EnemyHealth, IEnemy
{
    private int CD_HP = 5;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    private EffectManagement effectManagement;
    public Slider healthBar;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        effectManagement = GameObject.FindGameObjectWithTag("BattleEffect").GetComponent<EffectManagement>();
        player = GameObject.FindGameObjectWithTag("Player");
        Experience = 15;
        healthBar.value = CD_HP;
        maxHP = CD_HP;
        ID = 1; // ID of crowdeath is 1
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
        effectManagement.GenerateCoinDestroyCD(this.transform.position.x, this.transform.position.y);
        CombatEvents.EnemyDied(this);
        Destroy(enemy);
        ScoreManager.instance.AddPoint(50); // 50 is point value only for CrowDeath, another enemy has different point,
                                            // I can use delegate for this later to make sure Open/Closed Responsibility.
    }
}
