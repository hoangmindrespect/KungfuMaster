using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotourAttack : MonoBehaviour
{
    private Animator animator;
    public GameObject enemy;
    private bool isAttacking = false;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        animator = enemy.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackWithSoundEffect());
        }
    }

    IEnumerator AttackWithSoundEffect()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        if (isAttacking)
        {
            isAttacking = false;
            audioManager.PlaySFX(audioManager.minotourAttack);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("isAttacking", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("isAttacking", false);
    }
}
