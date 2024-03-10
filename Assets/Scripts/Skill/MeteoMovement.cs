using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class MeteoMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(5.0f, 10.0f)] public float MeteorVelocity;
    private Animator animator;
    private Rigidbody2D rb;
    void Start() {
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(MeteorVelocity, -MeteorVelocity);
    }
    void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);

        float distance = Time.fixedDeltaTime * MeteorVelocity;
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);

        //DESTROY METEOR COLLIDE WITH GROUND
        if((asi.IsName("LargeBroke") || asi.IsName("MediumBroke") || asi.IsName("SmallBroke")) && asi.normalizedTime > 1){
            Destroy(transform.gameObject);
        }else if((asi.IsName("Large") || asi.IsName("Medium") || asi.IsName("Small")) && asi.normalizedTime > 3){
            Destroy(transform.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        stopMeteor();
        animator.SetBool("groundCheck", true);
    }

    //When meteor collide with ground
    private void stopMeteor(){
        MeteorVelocity = 0.0f;
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.gravityScale = 0.0f;
    }
}
