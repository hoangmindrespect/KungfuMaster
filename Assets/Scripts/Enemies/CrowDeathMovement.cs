using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowDeathMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [Range(1.0f, 5f)] public float speed;
    public GameObject pointA;
    public GameObject pointB;
    private bool isFaceRight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0.0f);
        isFaceRight = true;
    }

    void FixedUpdate()
    {
        
    }

    private void Flip(){

    }
}
