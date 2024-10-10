using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class PotionLog : MonoBehaviour, IConsumable
{
    // SETTING FOR GIVING ITEMS INTO PLAYER INVENTORY
    public GameObject player;
    public Player playerComponent;
    public CharacterStats playerStats;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player>();
        playerStats = player.GetComponent<CharacterStats>();
    }

    public void Consume()
    {
        Debug.Log("You drank HEALING potion");
    }

    public void Consume(List<BaseStat> stats)
    {
        Debug.Log("You drank STATS potion");
    }
}
