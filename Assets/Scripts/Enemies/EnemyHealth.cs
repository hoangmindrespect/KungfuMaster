using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IEnemy
{
    public int maxHP { get; set; }
    public bool isSameDirection;
    public int ID { get; set; }
    public int Experience { get; set; }

    public virtual bool IsWillBeDie(int attdame)
    {
        return true;
    }
    public virtual void TackDamage(int attdame)
    {
        return;
    }

    public virtual void Destroy()
    {
        return;
    }
    public void ResetBool()
    {
        isSameDirection = false;
    }
}
