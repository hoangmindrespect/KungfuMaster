using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get { return Level * 25; } }
    public CharacterStats characterStats;

    public GameObject gameManagerObject;
    public GameManager gameManagerComponent;

    // Use this for initialization
    void Awake()
    {
        CombatEvents.OnEnemyDeath += EnemyToExperience;
        Level = 1;
        characterStats = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerComponent = gameManagerObject.GetComponent<GameManager>();
    }

    public void EnemyToExperience(IEnemy enemy)
    {
        GrantExperience(enemy.Experience);
    }

    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;
        while (CurrentExperience >= RequiredExperience)
        {
            CurrentExperience -= RequiredExperience;
            Level++;
            IncreaseStats();
        }
        UIEventHandler.PlayerLevelChanged();
    }

    public void IncreaseStats()
    {
        List<BaseStat> stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.ATK, 4, "Attack"),
            new BaseStat(BaseStat.BaseStatType.DEF, 2, "Defense") };
        characterStats.AddStatBonus(stats);
        UIEventHandler.StatsChanged();

        gameManagerComponent.HandleLevelUp();
    }
}
