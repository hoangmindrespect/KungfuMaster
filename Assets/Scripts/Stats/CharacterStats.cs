using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(int attack, int defense)
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.ATK, attack, "Attack"),
            new BaseStat(BaseStat.BaseStatType.DEF, defense, "Defense")
        };
    }
    public CharacterStats()
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.ATK, 10, "Attack"),
            new BaseStat(BaseStat.BaseStatType.DEF, 0, "Defense")
        };
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
