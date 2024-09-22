using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }
    public int CurrentDamage { get; set; }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
    }

    public void PerformSpecialAttack()
    {

    }
}
