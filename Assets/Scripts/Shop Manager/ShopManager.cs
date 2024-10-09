using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; set; }

    public RectTransform contents;

    ShopTemplate itemTemplate { get; set; }
    List<ShopTemplate> itemUIList = new List<ShopTemplate>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        itemTemplate = Resources.Load<ShopTemplate>("UI/Item_Template");
    }
    void Start()
    {
        Debug.Log("Item quantity: " + ItemDatabase.Instance.GetItemQuantity().ToString());
        AddRandomItemToShop();
    }

    public void ItemAdded(Item item)
    {
        ShopTemplate emptyItem = Instantiate(itemTemplate);
        emptyItem.SetItem(item);
        itemUIList.Add(emptyItem);
        emptyItem.transform.SetParent(contents);
    }

    public void AddRandomItemToShop()
    {
        List<int> itemIndexExist = new List<int>(); // this list use to make sure 10 items are different
        int randomIndex = -1;
        int itemQuantity = ItemDatabase.Instance.GetItemQuantity();
        try 
        {
            for (int index = 0; index < 10; index++) 
            {
                Debug.Log("LUCKY NUMBER START FROM: " + index.ToString());
                randomIndex = Random.Range(0, itemQuantity);

                while (itemIndexExist.Contains(randomIndex))
                {
                    Debug.Log("It's already exist this item index: " + randomIndex.ToString());
                    randomIndex = Random.Range(0, itemQuantity);
                }
                itemIndexExist.Add(randomIndex);

                Item randomItem = ItemDatabase.Instance.GetItem(randomIndex);
                Debug.Log("LUCKY item: " + randomItem.ObjectSlug);
                ItemAdded(randomItem);

                Debug.Log("random index: " + randomIndex.ToString());
                Debug.Log("random item: " + randomItem.ItemName);
            }
        }
        catch { }
    }
}
