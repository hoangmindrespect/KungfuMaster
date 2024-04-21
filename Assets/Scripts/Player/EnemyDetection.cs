using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject fightPoint, kickPoint, swordPoint;
    public float radius;
    public LayerMask layerMask;
    private Animator animator;
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    public void Attack(){
        if(animator.GetBool("isNoneState")){
            OnAttack(1,2, fightPoint, kickPoint);
        }else if(animator.GetBool("isElementState")){
            OnAttack(4, 3, fightPoint, kickPoint);
        } else if(animator.GetBool("isWeaponState")){
            OnAttack(3, 3, swordPoint, kickPoint);
        }
    }

    private void OnAttack(int i, int j, GameObject point_1, GameObject point_2){
        if(!animator.GetBool("isWeaponState")){
            if(animator.GetBool("isFighting")){
                Collider2D[] enemies = Physics2D.OverlapCircleAll(fightPoint.transform.position, radius, layerMask);

                foreach(var enemy in enemies){
                    if(enemy.GetComponent<Health>().IsWillBeDie(1)){
                        audioManager.PlaySFX(audioManager.crowdeathDeath);
                    }else{
                        enemy.GetComponent<Animator>().SetBool("isDamaged", true);
                        enemy.GetComponent<Health>().TackDamage(1);
                    }
                }
            }else if(animator.GetBool("isKicking")){
                Collider2D[] enemies = Physics2D.OverlapCircleAll(kickPoint.transform.position, radius, layerMask);

                foreach(var enemy in enemies){
                    if(enemy.GetComponent<Health>().IsWillBeDie(2)){
                        audioManager.PlaySFX(audioManager.crowdeathDeath);
                    }else{
                        enemy.GetComponent<Animator>().SetBool("isDamaged", true);
                        enemy.GetComponent<Health>().TackDamage(2);
                    }
                }
            }
        }else{
            Collider2D[] enemies = Physics2D.OverlapCircleAll(swordPoint.transform.position, radius, layerMask);

            foreach(var enemy in enemies){
                if(enemy.GetComponent<Health>().IsWillBeDie(3)){
                    audioManager.PlaySFX(audioManager.crowdeathDeath);
                }else{
                    enemy.GetComponent<Animator>().SetBool("isDamaged", true);
                    enemy.GetComponent<Health>().TackDamage(3);
                }
            }
        }
    } 

    private void OnDrawGizmos() {
        // Gizmos.DrawWireSphere(swordPoint.transform.position, radius);
        // Gizmos.DrawWireSphere(kickPoint.transform.position, radius);
        // Gizmos.DrawWireSphere(fightPoint.transform.position, radius);
    }
}
