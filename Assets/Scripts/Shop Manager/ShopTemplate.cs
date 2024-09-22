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

        if (item.Stats == null) return;
        for (int i = 0; i < item.Stats.Count; i++)
        {
            itemStatTexts.Add(Instantiate(itemStatPrefab));
            itemStatTexts[i].transform.SetParent(itemStatPanel);
            itemStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
    }    
}
