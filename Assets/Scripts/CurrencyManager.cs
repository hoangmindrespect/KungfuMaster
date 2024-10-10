using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public TextMeshProUGUI moneyText;

    [SerializeField]
    private int money = 0;

    public int Money {  get { return money; } set { money = value; } } // set value for money first through property, then set value for PlayerPrefs

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("money"))
            PlayerPrefs.SetInt("money", 0);
        money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString() + "$";
    }

    public void AddMoney(int value)
    {
        money = PlayerPrefs.GetInt("money");
        money += value;
        moneyText.text = money.ToString() + "$";
        PlayerPrefs.SetInt("money", money);
    }

    public void SetMoney()
    {
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money.ToString() + "$";
    }
}
