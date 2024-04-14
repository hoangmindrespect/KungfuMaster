using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDeathMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [Range(1.0f, 5f)] public float speed;
    public int direction;
    public GameObject pointA;
    public GameObject pointB;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0.0f);
        direction = 1;
    }

    public void OnAttack(){
        
    }
    public void Flip(){
        direction = -direction;
        Vector3 theScale = transform.localScale;
        
		theScale.x *= -1;
		transform.localScale = theScale;
        rb.velocity = new Vector2(speed * direction, 0.0f);
    }
}
