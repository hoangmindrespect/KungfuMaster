using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement : MonoBehaviour, IEnemy
{
    public int ID {  get; set; }

    // Start is called before the first frame update
    public GameObject pointA;
    public GameObject pointB;
    public GameObject room;
    public Animator animator;
    private Rigidbody2D rb;
    public int HP = 2;
    private Transform currentGoal; // which point is enemy moving to?
    private float speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentGoal = pointB.transform;
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per framea
    void Update()
    {
        //cal the distance from the goal(A or B) to minotaur
        float distance = currentGoal.position.x - transform.position.x;

        if(currentGoal == pointB.transform && distance < 0){
            currentGoal = pointA.transform;
            rb.velocity = new Vector2(-speed, 0f);
            Flip();
        }else if(currentGoal == pointA.transform && distance > 0){
            currentGoal = pointB.transform;
            rb.velocity = new Vector2(speed, 0f);
            Flip();
        }
        print(HP);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>

    private void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}