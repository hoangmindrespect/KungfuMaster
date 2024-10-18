using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseStat;

public class CharacterStatsMemento // This  class will have methods to create a memento (save the state) and
                                   // restore from a memento (load the state when the player reborns).
{
    // {The underneath way will make a reference issue that all stats list of difference CharacterStatsMemento refer to one List<> stats. Then when adding a new characterStatsMemento to stack, all List<> stats will change to be the same with new added List<> stats}
    //public List<BaseStat> stats = new List<BaseStat>();
    //public CharacterStatsMemento(int level, List<BaseStat> stats)
    //{
    //    Level = level;
    //    this.stats = stats;
    //}

    public int Level { get; private set; }
    public List<BaseStat> PlayerBaseStats { get; private set; }
    // Constructor for creating a memento with a deep copy of base stats
    public CharacterStatsMemento(int level, List<BaseStat> baseStats)
    {
        this.Level = level;
        // Deep copy of base stats to avoid reference issues
        this.PlayerBaseStats = new List<BaseStat>();
        foreach (var stat in baseStats)
        {
            // Ensure we are copying the calculated FinalValue
            stat.GetCalculatedStatValue(); // Update the FinalValue

            // Now copy the stat
            this.PlayerBaseStats.Add(new BaseStat(stat.StatType, stat.FinalValue, stat.StatName) // Create a new copy of each stat
            {
                // FinalValue = stat.FinalValue // Ensure the FinalValue is copied - no need
            });

            //Debug.Log($"[CharacterStatsMemento] Copied Stat: {stat.StatName}, FinalValue: {stat.BaseValue}");
        }
    }
}
