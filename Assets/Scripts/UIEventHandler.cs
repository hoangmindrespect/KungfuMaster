using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIEventHandler : MonoBehaviour
{

    public delegate void ItemEventHandler(Item item); // Deligate: a pointer to a method.
                                                      // This handles anything that happens with the item or with an item in the inventory for user interface.
                                                      // Define delegate for event like we define a new type of data for event.
    public static event ItemEventHandler OnItemAddedToInventory; // Static event is used to handle the subscribed method outside of class UIEventHandler. 
                                                                 // This event is the same type of the delegate we just created, with the parameter is Item.
                                                                 // Whenever calls this event, the system will call all of the methods that assigned/subscribed to this event.
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsEventHandler();
    public static event StatsEventHandler OnStatsChanged;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelChange;

    // Notice: Keep the same "Signature" for method which is going to be handling or calling the event.
    // Signature: a method or deligate's name and the type of it's parameters (not the the names of those parameters or not the data type of the method itself)
    public static void ItemAddedToInventory(Item item) // method ItemAddedToInventory has the same signature parameter type "Item" with the delelagte ItemEventHandler which its event is handled by this method.
    {
        if (OnItemAddedToInventory != null)
        {
            OnItemAddedToInventory(item);
        }
    }

    //public static void ItemAddedToInventory(List<Item> items)
    //{
    //    if (OnItemAddedToInventory != null)
    //    {
    //        foreach (Item item in items)
    //        {
    //            OnItemAddedToInventory(item);
    //        }
    //    }
    //}

    public static void ItemEquipped(Item item)
    {
        if (OnItemEquipped != null)
            OnItemEquipped(item);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        if (OnPlayerHealthChanged != null)
            OnPlayerHealthChanged(currentHealth, maxHealth);
    }

    public static void StatsChanged()
    {
        if (OnStatsChanged != null)
            OnStatsChanged();
    }

    public static void PlayerLevelChanged()
    {
        if (OnPlayerLevelChange != null)
            OnPlayerLevelChange();
    }
}
