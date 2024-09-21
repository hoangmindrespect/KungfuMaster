using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    //public string description;
    public int basePrice;

    public void Start()
    {
        int randomIndex = Random.Range(0, ItemDatabase.ItemQuantity);
        Item randomItem = ItemDatabase.Instance.GetItem(randomIndex);

        title = randomItem.ItemName;
        Debug.Log("random index: " + randomIndex);
        Debug.Log("random item: " + randomItem.ItemName);
    }
}
