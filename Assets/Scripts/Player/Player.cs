using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public Rigidbody2D playerRb;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; }

    void Awake()
    {
        PlayerLevel = GetComponent<PlayerLevel>();
        characterStats = GetComponent<CharacterStats>();
        this.currentHealth = this.maxHealth;
        transform.position = GameManager.playerStartPosition;
    }

    void Start()
    {
        //UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    TakeDamage(5);
        //}
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame(this);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Ui da");
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

    public void Healing(int amount)
    {
        Debug.Log("HEALING");
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
