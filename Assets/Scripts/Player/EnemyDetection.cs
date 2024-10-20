using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject fightPoint, kickPoint, swordPoint;
    public float radius_Fight;
    public float radius_Kick;
    public float radius_Sword;
    public LayerMask layerMask;
    private Animator animator;
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (animator.GetBool("isNoneState"))
        {
            OnAttack(1, 2, fightPoint, kickPoint, radius_Fight, radius_Kick);
        }
        else if (animator.GetBool("isElementState"))
        {
            OnAttack(4, 3, fightPoint, kickPoint, radius_Fight, radius_Kick);
        }
        else if (animator.GetBool("isWeaponState"))
        {
            OnAttack(3, 3, swordPoint, kickPoint, radius_Sword, radius_Sword);
        }
    }

    private void OnAttack(int i, int j, GameObject point_1, GameObject point_2, float radius_1, float radius_2)
    {
        if (animator.GetBool("isFighting"))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(point_1.transform.position, radius_1, layerMask);

            foreach (var enemy in enemies)
            {
                if (enemy.GetComponent<EnemyHealth>().IsWillBeDie(i))
                {
                    audioManager.PlaySFX(audioManager.crowdeathDeath);
                }
                else
                {
                    enemy.GetComponent<Animator>().SetBool("isDamaged", true);
                    enemy.GetComponent<EnemyHealth>().TackDamage(j);
                }
            }
        }
        else if (animator.GetBool("isKicking"))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(point_2.transform.position, radius_2, layerMask);

            foreach (var enemy in enemies)
            {
                if (enemy.GetComponent<EnemyHealth>().IsWillBeDie(i))
                {
                    audioManager.PlaySFX(audioManager.crowdeathDeath);
                }
                else
                {
                    enemy.GetComponent<Animator>().SetBool("isDamaged", true);
                    enemy.GetComponent<EnemyHealth>().TackDamage(j);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(swordPoint.transform.position, radius_Sword);
        // Gizmos.DrawWireSphere(kickPoint.transform.position, radius_Kick);
        // Gizmos.DrawWireSphere(fightPoint.transform.position, radius_Fight);
    }
}
