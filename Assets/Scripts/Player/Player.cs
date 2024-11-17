using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;
using Unity.Properties;
using static Cinemachine.DocumentationSortingAttribute;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public Slider healthBar;
    public Rigidbody2D playerRb;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; } // used for get component from characterPanel
    public bool IsDied { get; set; }

    void Awake()
    {
        PlayerLevel = GetComponent<PlayerLevel>();
        characterStats = GetComponent<CharacterStats>();
        this.currentHealth = this.maxHealth;
        transform.position = GameManager.playerStartPosition;
        IsDied = false;
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame(this);
    }

    public void TakeDamage(int amount)
    {
        healthBar.value -= amount;
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Die()
    {
        IsDied = true;

        Debug.Log("Player dead. Reset health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    public void Healing(int amount)
    {
        Debug.Log("HEALING");
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    // Save the current stats to a memento
    public CharacterStatsMemento SaveStatsToMemento() // used to get characterStats component of player
    {
        // Calculate and log the FinalValue of each stat before saving
        foreach (BaseStat stat in characterStats.stats)
        {
            stat.GetCalculatedStatValue(); // Make sure FinalValue is up to date
            //Debug.Log($"[SaveStatsToMemento] Stat: {stat.StatName}, FinalValue: {stat.FinalValue}");
        }

        return new CharacterStatsMemento(PlayerLevel.Level, characterStats.stats);
    }

    // Restore the stats from the memento
    public void RestoreStatsFromMemento(CharacterStatsMemento memento)
    {
        PlayerLevel.Level = memento.Level;

        //foreach (BaseStat baseStat in memento.PlayerBaseStats)
        //{
        //    Debug.Log("[Player] {RestoreStatsFromMemento} " + baseStat.StatName + " {baseVale}: " + baseStat.BaseValue.ToString() + " {fianlVale}: " + baseStat.FinalValue.ToString());
        //}

        characterStats.stats = new List<BaseStat>(memento.PlayerBaseStats); // Restore a copy of base stats
    }

    public void Reborn(CharacterStatsMemento memento)
    {
        //foreach (BaseStat baseStat in memento.PlayerBaseStats)
        //{
        //    Debug.Log("[Player] {reborn} " + baseStat.StatName + " {baseVale}: " + baseStat.BaseValue.ToString() + " {fianlVale}: " + baseStat.FinalValue.ToString());
        //}

        // Restore stats from memento
        RestoreStatsFromMemento(memento);
    }
}
