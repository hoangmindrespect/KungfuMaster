using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public InventoryUIDetails inventoryDetailsPanel;
    public List<Item> playerItems = new List<Item>();

    //// Testing //
    //public Item sword;
    //public Item potionLog;
    ////  End    //

    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();
        GiveItem("sword");
        GiveItem("staff");
        GiveItem("potion_log");

        //// Testing //
        //List<BaseStat> swordStats = new List<BaseStat>();
        ////swordStats.Add(new BaseStat(6, "Power", "Your power level."));
        //swordStats.Add(new BaseStat(BaseStat.BaseStatType.Power, 6, "Power"));
        //sword = new Item(swordStats, "sword");
        //potionLog = new Item(new List<BaseStat>(), "potion_log", "Drink this to log something cool!", Item.ItemTypes.Consumable ,"Drink", "Log Potion", false);
        ////  End    //
    }

    //// Testing //
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        //playerWeaponController.EquipWeapon(sword);
    //        //consumableController.ConsumeItem(potionLog);
    //    }
    //}
    //// End     //

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        playerItems.Add(item);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void GiveItem(Item item)
    {
        playerItems.Add(item);
        UIEventHandler.ItemAddedToInventory(item);
    }

    //public void GiveItem(List<Item> items)
    //{
    //    playerItems.AddRange(items);
    //    UIEventHandler.ItemAddedToInventory(items);
    //}

    public void SetItemDetails(Item item, Button selectedButton)
    {
        inventoryDetailsPanel.SetItem(item, selectedButton);
    }

    public void EquipItem(Item itemToEquip)
    {
        //playerWeaponController.EquipWeapon(itemToEquip);
    }

    public void ConsumeItem(Item itemToConsume)
    {
        //consumableController.ConsumeItem(itemToConsume);
    }
}
