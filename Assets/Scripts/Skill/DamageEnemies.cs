using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isPushedBack = false;
    private MinotaurMovement mm;
    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Animator>(out animator)) {
            animator.SetBool("isDamaged", true);
            animator.SetBool("isAttack", false);
        }
        if(other.TryGetComponent<Rigidbody2D>(out rb)) {
            Hurt(rb);
        }


        if(other.TryGetComponent<MinotaurMovement>(out mm)){
            if(mm.HP == 1){
                Destroy(other.gameObject);
            }else{
                mm.HP -= 1;
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent<Animator>(out animator)) { 
            animator.SetBool("isDamaged", false);
            animator.SetBool("isAttack", true);
        }
    }
    private IEnumerator HurtCoroutine(Rigidbody2D rb)
    {
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = new Vector2(0f, 0f);

        isPushedBack = true;
        rb.transform.position = new Vector3(rb.transform.position.x - 1.0f, rb.transform.position.y, 0f);
        yield return new WaitForSeconds(0.8f);

        rb.velocity = currentVelocity;
        isPushedBack = false;
    }

    void FixedUpdate()
    {
        if(isPushedBack && rb){
            rb.transform.position = new Vector3(rb.transform.position.x - 0.5f*Time.fixedDeltaTime, rb.transform.position.y, 0f);
        }
        
    }

    private void Hurt(Rigidbody2D rb)
    {
        StartCoroutine(HurtCoroutine(rb));
    }

}
