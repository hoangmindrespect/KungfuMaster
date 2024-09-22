using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Item
{
    public enum ItemTypes { Consumable, Weapon, Quest }
    public List<BaseStat> Stats { get; set; } // One item has many stats like attack or defense
    public string ObjectSlug { get; set; } // This seems like header.
                                           // This means type of certain object. For example, potion_log is type of potion, big sword is type of sword.
                                           // Moreover, this is defined to differentiate from others => one model one prefab. Avoid call one model but have the same prefabs.
    public string Description { get; set; }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public ItemTypes ItemType { get; set; } // This seems like category.
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    public bool ItemModifier { get; set; }
    public int Price { get; set; }

    public Item(List<BaseStat> _Stats, string _ObjectSlug)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
    }

    [JsonConstructor]
    public Item(List<BaseStat> _Stats, string _ObjectSlug, string _Description, ItemTypes _ItemType, string _ActionName, string _ItemName, bool _ItemModifier, int _Price)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
        this.Description = _Description;
        this.ItemType = _ItemType;
        this.ActionName = _ActionName;
        this.ItemName = _ItemName;
        this.ItemModifier = _ItemModifier;
        this.Price = _Price;
    }
}
