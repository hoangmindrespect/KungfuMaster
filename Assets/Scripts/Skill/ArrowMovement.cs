using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public float ArrowVelocity = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, -ArrowVelocity);
    }

    void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);

        float distance = Time.fixedDeltaTime * ArrowVelocity;
        transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);

        //DESTROY METEOR COLLIDE WITH GROUND
        if (asi.IsName("Arrow_collide") && asi.normalizedTime > 1)
        {
            Destroy(transform.gameObject);
        }
        else if (asi.IsName("Arrow_active") && asi.normalizedTime > 3)
        {
            //Destroy(transform.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        stopArrow();
        animator.SetBool("isCollide", true);
    }

    private void stopArrow()
    {
        ArrowVelocity = 0.0f;
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.gravityScale = 0.0f;
    }
}
