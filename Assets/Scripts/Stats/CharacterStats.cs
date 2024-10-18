using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CharacterStats : MonoBehaviour // ~ Memento Class
{
    public List<BaseStat> stats;

    public CharacterStats(int attack, int defense)
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.ATK, attack, "Attack"),
            new BaseStat(BaseStat.BaseStatType.DEF, defense, "Defense")
        };
        foreach (BaseStat stat in stats)
        {
            //Debug.Log(stat.StatType + stat.FinalValue.ToString());
        }
    }
    public CharacterStats()
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.ATK, 10, "Attack"),
            new BaseStat(BaseStat.BaseStatType.DEF, 0, "Defense")
        };
        foreach (BaseStat stat in stats) 
        {
            //Debug.Log(stat.StatType + stat.FinalValue.ToString());
        }
        
    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
