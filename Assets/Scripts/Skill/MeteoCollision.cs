using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoCollision : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TackDamage(damage);
            enemyHealth.Destroy();
        }
    }
}
