using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination;
    GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(Vector2.Distance(player.transform.position, transform.position) > 0.3f){
            player.transform.position = destination.transform.position;

        }
    }
}
