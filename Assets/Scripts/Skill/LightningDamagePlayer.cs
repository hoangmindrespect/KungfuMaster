using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningDamagePlayer : MonoBehaviour
{
    public int damage;
    private Animator animator;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TackDamage(damage);
            enemyHealth.Destroy();
        }
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("appear") && asi.normalizedTime > 1.0f)
        {
            Destroy(transform.gameObject);
        }
    }
}
