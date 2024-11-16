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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 0.0f));
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if (isCollided == false && collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.collectingGem);
            CurrencyManager.instance.AddMoney(100);
            Destroy(gameObject);
            isCollided = true;
        }
    }
}
