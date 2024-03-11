using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MileStoneFunc : MonoBehaviour
{
    private Rigidbody2D enemyRigidBody2D;
    void OnTriggerEnter2D(Collider2D other)
    {
        enemyRigidBody2D = other.GetComponent<Rigidbody2D>();

        float  enemyVelocity = enemyRigidBody2D.velocity.x;
        Vector3 theScale = other.transform.localScale;
		theScale.x *= -1;
		other.transform.localScale = theScale;
        enemyRigidBody2D.velocity = new Vector2(-enemyVelocity, 0.0f);
    }
}
