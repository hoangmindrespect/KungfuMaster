using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    int score = 0;
    int highscore = 0;

    // SETTING FOR GIVING ITEMS INTO PLAYER INVENTORY
    public Player player;
    public InventoryController inventoryController;

    bool isQualifiedSword = false;
    bool isQualifiedFire = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);

        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();

        inventoryController = player.GetComponent<InventoryController>();
    }

    public void AddPoint(int value)
    {
        score += value;
        scoreText.text = score.ToString() + " POINTS";
        if(highscore < score)
            PlayerPrefs.SetInt("highscore", score);

        if(score >= 250 && !isQualifiedSword)
        {
            isQualifiedSword=true;
            inventoryController.GiveItem("sword");
        }

        if(score >= 500 && !isQualifiedFire)
        {
            isQualifiedFire=true;
            inventoryController.GiveItem("fire");
        }
    }
}
