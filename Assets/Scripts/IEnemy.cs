using UnityEngine;
using System.Collections;

public interface IEnemy
{
    int ID { get; set; }
    //Spawner Spawner { get; set; }
    int Experience { get; set; }
    void Destroy();
    void TackDamage(int amount);
    void PerformAttack();
}