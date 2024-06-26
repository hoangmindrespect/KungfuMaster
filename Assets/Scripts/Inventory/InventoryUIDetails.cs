using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDetails : MonoBehaviour // Responsible for displaying more details of the item that we clicked.
{
    Item item;
    Button selectedItemButton, itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescriptionText, itemInteractButtonText;

    public TextMeshProUGUI statText;
    void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent<TextMeshProUGUI>();
        itemInteractButton = transform.GetComponentInChildren<Button>();
        itemInteractButtonText = itemInteractButton.GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void SetItem(Item item, Button selectedButton)
    {
        gameObject.SetActive(true);
        statText.text = "";
        if (item.Stats != null)
        {
            foreach (BaseStat stat in item.Stats)
            {
                statText.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }
        itemInteractButton.onClick.RemoveAllListeners();
        this.item = item;
        selectedItemButton = selectedButton;
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        itemInteractButtonText.text = item.ActionName;
        itemInteractButton.onClick.AddListener(OnItemInteract);
    }

    public void OnItemInteract() // When click the button of item in detail panel, the system will call the suitable method from InventoryController.
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);

            // Flow of equip one weapon:
            // [InventoryUIDetails] OnItemInteract() -> [InventoryController] EquipItem(Item itemToEquip)
            //                                       -> [PlayerWeaponController] EquipWeapon(Item itemToEquip) { UIEventHandler.ItemEquipped(itemToEquip);}
            //                                       -> [UIEventHandler] ItemEquipped(Item item)
            //                                       -> [CharacterPanel] UpdateEquippedWeapon(Item item) { for (int i = 0; i < item.Stats.Count; i++) weaponStatTexts[i].transform.SetParent(weaponStatPanel); }
        }
        item = null;
        gameObject.SetActive(false);
    }
}
