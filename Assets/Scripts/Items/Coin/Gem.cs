using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    bool isCollided;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            audioManager.PlaySFX(audioManager.collectingGem);
            ScoreManager.instance.AddPoint(100);
            Destroy(gameObject);
            isCollided = true;
        }  
    }
}
