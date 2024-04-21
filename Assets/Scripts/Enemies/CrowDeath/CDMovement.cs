using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDeathMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [Range(1.0f, 5f)] public float speed;
    private int direction;
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

    public void Flip(){
        direction = -direction;
        Vector3 theScale = transform.localScale;
        
		theScale.x *= -1;
		transform.localScale = theScale;
        rb.velocity = new Vector2(speed * direction, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 collisionVelocity = other.relativeVelocity;
        if (collisionVelocity.x > 0) 
        {
            if (direction < 0) 
            {
                Flip();
            }
        }
        else if (collisionVelocity.x < 0) 
        {
            if (direction > 0) 
            {
                Flip();
            }
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(animator.GetBool("isDied")){
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if(asi.IsName("die") && asi.normalizedTime > 0.7f){
            GetComponent<Health>().Destroy();
        }else if(asi.IsName("damage")){
            if(asi.normalizedTime > 1.0f){
                animator.SetBool("isDamaged", false);
                if(GetComponent<Health>().isSameDirection){
                    rb.velocity = new(rb.velocity.x / 2, 0.0f);
                    GetComponent<Health>().ResetBool();
                }else{
                    rb.velocity = new(-rb.velocity.x, 0.0f);
                }
            }
        }
    }
}
