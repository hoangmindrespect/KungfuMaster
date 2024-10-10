using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ShopTemplate : MonoBehaviour
{
    [SerializeField]
    private Item item;

    //Equipped Weapon
    [SerializeField]
    private TextMeshProUGUI itemStatPrefab; // ~= weaponStatPrefab
    [SerializeField]
    private Transform itemStatPanel; // ~= weaponStatPanel
    [SerializeField]
    private TextMeshProUGUI itemNameText; // ~= weaponNameText
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private Image itemIcon; // ~= weaponIcon
    private List<TextMeshProUGUI> itemStatTexts = new List<TextMeshProUGUI>(); // ~= weaponStatTexts
    [SerializeField]
    private Button purchaseBtn;

    //Player money
    int money = 0;

    // SETTING FOR GIVING ITEMS INTO PLAYER INVENTORY
    public GameObject player;
    public InventoryController inventoryController;

    private void Start()
    {
        player = GameObject.Find("Player");
        inventoryController = player.GetComponent<InventoryController>();

        money = PlayerPrefs.GetInt("money");
        CheckPurchaseable();
    }
    private void Update()
    {
        money = PlayerPrefs.GetInt("money");
        CheckPurchaseable();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        SetupItemValues();
    }

    void SetupItemValues()
    {
        itemNameText.text = item.ItemName;
        priceText.text = item.Price.ToString();
        itemIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);

        if (item.Stats == null)
        {
            TextMeshProUGUI itemStatTexts = Instantiate(itemStatPrefab);
            itemStatTexts.transform.SetParent(itemStatPanel);
            itemStatTexts.text = item.Description;
            return;
        }
        for (int i = 0; i < item.Stats.Count; i++)
        {
            itemStatTexts.Add(Instantiate(itemStatPrefab));
            itemStatTexts[i].transform.SetParent(itemStatPanel);
            itemStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
    }

    public void CheckPurchaseable()
    {
        if (money >= item.Price) // if I have enough money
            purchaseBtn.interactable = true;
        else
            purchaseBtn.interactable = false;
    }

    public void PurchaseItem()
    {
        if (money >= item.Price)
        {
            money = money - item.Price;
            CurrencyManager.instance.Money = money;
            CurrencyManager.instance.SetMoney();
            CheckPurchaseable();
            //Unlock item.

            inventoryController.GiveItem(item);
        }
    }
}
