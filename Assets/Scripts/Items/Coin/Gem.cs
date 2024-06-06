using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    bool isCollided;
    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isCollided == false)
        {
            ScoreManager.instance.AddPoint(100);
            Destroy(gameObject);
            isCollided = true;
        }  
    }
}
