using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public LayerMask layerMask;
    public GameObject attackPoint;
    public Transform healthbarTransform;  // Transform của thanh máu (slider)
    public float radius;
    [Range(1.0f, 5f)] public float speed;
    private int direction;
    [Range(1, 5)] public int damageCaused;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb.velocity = new Vector2(speed, 0.0f);
        direction = 1;
    }

    public void Flip()
    {
        if (direction > 0)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        direction = -direction;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
        rb.velocity = new Vector2(speed * direction, 0.0f);

        // Giữ thanh máu không bị lật bằng cách đảo ngược hướng của nó
        Vector3 healthbarScale = healthbarTransform.localScale;
        healthbarScale.x *= -1;  // Đảo ngược X của thanh máu
        healthbarTransform.localScale = healthbarScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Flip();

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (animator.GetBool("isDied"))
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("die") && asi.normalizedTime > 0.7f)
        {
            GetComponent<EnemyHealth>().Destroy();
        }
        else if (asi.IsName("damage"))
        {
            if (asi.normalizedTime > 1.0f)
            {
                animator.SetBool("isDamaged", false);
                if (GetComponent<EnemyHealth>().isSameDirection)
                {
                    rb.velocity = new(rb.velocity.x / 2, 0.0f);
                    GetComponent<EnemyHealth>().ResetBool();
                }
                else
                {
                    rb.velocity = new(-rb.velocity.x, 0.0f);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
    public void Attack()
    {
        Collider2D[] gameObj = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, layerMask);
        foreach (Collider2D colision in gameObj)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<Player>().TakeDamage(damageCaused);
            }
        }
    }
}
