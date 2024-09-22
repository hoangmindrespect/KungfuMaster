using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; set; }
    private List<Item> Items { get; set; }
    
    // Get quantity of item database
    private int ItemQuantity { get; set; }

    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        BuildDatabase();

        ItemQuantity = Items.Count;
        //Debug.Log("Item quantity: " + ItemQuantity);
    }

    private void BuildDatabase()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
    }

    // Get item from unaccessable item list by this function with the compatible parameter
    public Item GetItem(string itemSlug)
    {
        foreach (Item item in Items)
        {
            if (item.ObjectSlug == itemSlug)
                return item;
        }
        Debug.LogWarning("Couldn't find item: " + itemSlug);
        return null;
    }

    public Item GetItem(int itemIndex)
    {
        Item item = Items[itemIndex];
        if (item == null)
            Debug.LogWarning("Couldn't find item[ " + itemIndex.ToString() + "]");
        return item;
    }

    public int GetItemQuantity()
    {
        return ItemQuantity;
    }
}
