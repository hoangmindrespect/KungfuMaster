using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePunchMovement : MonoBehaviour
{
    public CharacterController2D cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 distance = transform.position;
        if(cc.isFaceRight){
            transform.position = new Vector3(transform.position.x + Time.deltaTime*15f, transform.position.y, 0f);
        }
        else{
            transform.position = new Vector3(transform.position.x - Time.deltaTime*15f, transform.position.y, 0f);
        }
    }
}
