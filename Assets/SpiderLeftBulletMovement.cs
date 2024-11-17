using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeftBulletMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("live-bullet"))
        {
            StartCoroutine(ResetIsAppearAfterDelay(2f));
        }
    }
    private IEnumerator ResetIsAppearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
