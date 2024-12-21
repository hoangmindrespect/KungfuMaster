using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    private Animator animator;
    private float spiderPositionX;
    private float spiderPositionY;
    private bool isFaceRight;
    private bool isAttacked;

    public GameObject player;
    public GameObject poisonRightBullet;
    public GameObject poisonLeftBullet;
    void Start()
    {
        animator = GetComponent<Animator>();
        spiderPositionX = this.gameObject.transform.position.x;
        spiderPositionY = this.gameObject.transform.position.y;
        isFaceRight = false;
    }

    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void FixedUpdate()
    {
        if (player.transform.position.x < spiderPositionX)
        {
            if (isFaceRight)
            {
                isFaceRight = false;
                Flip();
            }
        }
        else
        {
            if (!isFaceRight)
            {
                isFaceRight = true;
                Flip();
            }
        }
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("attack") && !isAttacked)
        {
            if (!isFaceRight)
            {
                Instantiate(poisonLeftBullet, new Vector3(spiderPositionX, spiderPositionY - 1.5f, 0.0f), Quaternion.identity);
            }
            else
            {
                Flip();
                Instantiate(poisonRightBullet, new Vector3(spiderPositionX, spiderPositionY - 1.5f, 0.0f), Quaternion.identity);
            }
            isAttacked = true;
        }
        else if (asi.IsName("hide"))
        {
            isAttacked = false;
        }
    }
}
