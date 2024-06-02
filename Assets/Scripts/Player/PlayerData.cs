using UnityEngine;
using System.Collections;

using System;

[Serializable]
public class PlayerData
{
    public int level;
    public int currentHealth;
    public int maxHealth;
    public float[] playerPosition;
 
    public PlayerData(Player player){
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }

}
