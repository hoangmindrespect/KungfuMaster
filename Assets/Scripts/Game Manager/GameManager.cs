using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour // Used GameManager to handle Caretaker class.
                                         // When the player dies, save the current stats using the caretaker.
                                         // When the player reborns, restore the stats from the saved memento.
{
    public static Vector3 playerStartPosition;

    public GameObject player;
    public GameObject bringerOfDeath;

    // private bool is_bringer_created = false;

    // Setup to control Caretaker and used as a history
    public Player playerComponent;
    private CharacterStatsCaretaker caretaker = new CharacterStatsCaretaker();
    private void Start()
    {
        playerComponent = player.GetComponent<Player>();

        // Save the initial state of the player when the game starts

        //foreach (BaseStat baseStat in playerComponent.SaveStatsToMemento().PlayerBaseStats)
        //{
        //    Debug.Log("[PlayerStats] " + baseStat.StatName + " {save}: " + baseStat.FinalValue.ToString());
        //}

        caretaker.SaveState(playerComponent.SaveStatsToMemento());
    }

    // When the player levels up, save their current stats
    public void HandleLevelUp()
    {
        caretaker.SaveState(playerComponent.SaveStatsToMemento());
        Debug.Log("Player leveled up! Stats saved.");
    }

    private void Update()
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        // if (!is_bringer_created)
        // {
        //     if (player.transform.position.x > 280.0f)
        //     {
        //         if (Input.GetButtonDown("Boss"))
        //         {
        //             SceneManager.LoadScene("Boss1");
        //             is_bringer_created = true;
        //         }
        //     }
        // }

        if (playerComponent.IsDied)
        {
            // Player dies, restore the last saved stats (pop from stack)
            var previousStats = caretaker.GetSavedState();
            if (previousStats != null)
            {
                playerComponent.Reborn(previousStats);
                Debug.Log("Player died. Restoring stats from last save.");
                UIEventHandler.StatsChanged();
                UIEventHandler.PlayerLevelChanged();
            }

            playerComponent.IsDied = false;
        }
    }

    private void OnApplicationFocus(bool focus) // This used to reset money when player turn the game off
    {
        if (!focus)
        {
            if (PlayerPrefs.HasKey("money"))
                PlayerPrefs.DeleteKey("money");
        }
    }
}