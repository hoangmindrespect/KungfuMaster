using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    Animator animator;
    Rigidbody2D playerRb;
    
    private AudioManager audioManager;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Vector2.Distance(player.transform.position, transform.position) > 0.3f){
            StartCoroutine(PortalIn());
        }
    }

    IEnumerator PortalIn()
    {
        playerRb.simulated = false;
        animator.SetBool("isEntryPortal", true);
        audioManager.PlaySFX(audioManager.EntryPortal);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isEntryPortal", false);
        animator.SetBool("isExistPortal", true);
        player.transform.position = new Vector2(destination.position.x + 1f, destination.position.y );
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isExistPortal", false);
        playerRb.simulated = true;
    }
}
