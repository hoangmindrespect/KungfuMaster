using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
        if (isCollided == false && collision.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.collectingGem);
            CurrencyManager.instance.AddMoney(100);
            Destroy(gameObject);
            isCollided = true;
        }
    }
}
