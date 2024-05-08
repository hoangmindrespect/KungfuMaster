using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    //public PlayerLevel PlayerLevel { get; set; }

    void Awake()
    {
        //PlayerLevel = GetComponent<PlayerLevel>();
        characterStats = GetComponent<CharacterStats>();
        this.currentHealth = this.maxHealth;
    }

    void Start()
    {
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    // Testing //
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(5);
        }
    }
    // End     //

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player dead. Reset health.");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
