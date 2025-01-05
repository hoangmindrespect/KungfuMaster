using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WizardHealth : EnemyHealth, IEnemy
{
    private int BD_HP = 100;
    public GameObject enemy;
    private Animator animator;
    private GameObject player;
    private AudioManager audioManager;
    private EffectManagement effectManagement;
    public Slider healthBar;

    public void Awake()
    {
        effectManagement = GameObject.FindGameObjectWithTag("BattleEffect").GetComponent<EffectManagement>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        healthBar.value = BD_HP;
        maxHP = BD_HP;
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
        animator.Play("takehit", 0, 0.1f);
        healthBar.value = maxHP;
        if (maxHP < 0)
        {
            WizardMovement.isDied = true;
            animator.SetBool("isDied", true);
            animator.Play("Death", 0, 0.2f);
        }
    }

    public override void Destroy()
    {
        Destroy(enemy);
        ScoreManager.instance.AddPoint(150);
    }
}

