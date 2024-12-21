using UnityEngine;
using UnityEngine.UI;

public class BringerHealth : EnemyHealth, IEnemy
{
    private int BD_HP = 100;
    public GameObject enemy;
    private GameObject player;
    private AudioManager audioManager;
    private EffectManagement effectManagement;
    public Slider healthBar;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        healthBar.value = maxHP;
    }

    public override void Destroy()
    {
        effectManagement.GenerateCoinDestroyCD(this.transform.position.x, this.transform.position.y);
        CombatEvents.EnemyDied(this);
        Destroy(enemy);
        ScoreManager.instance.AddPoint(150); // 50 is point value only for CrowDeath, another enemy has different point,
                                             // I can use delegate for this later to make sure Open/Closed Responsibility.
    }
}
