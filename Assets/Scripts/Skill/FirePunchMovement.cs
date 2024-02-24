using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePunchMovement : MonoBehaviour
{
    public CharacterController2D cc;
    private Vector2 startPos;
    private bool directionSkill;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        directionSkill = cc.isFaceRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(directionSkill){
            transform.position = new Vector3(transform.position.x + Time.deltaTime*15f, transform.position.y, 0f);
        }
        else{
            transform.position = new Vector3(transform.position.x - Time.deltaTime*15f, transform.position.y, 0f);
        }
    }
}
